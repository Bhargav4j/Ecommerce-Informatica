# EcommerceInformatica - AWS ECS Fargate Deployment Guide

This guide provides comprehensive instructions for deploying the EcommerceInformatica ASP.NET Core 8.0 application to AWS ECS Fargate.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Local Development Setup](#local-development-setup)
3. [Docker Setup](#docker-setup)
4. [AWS ECS Fargate Prerequisites](#aws-ecs-fargate-prerequisites)
5. [Building and Pushing Docker Images](#building-and-pushing-docker-images)
6. [ECS Fargate Deployment](#ecs-fargate-deployment)
7. [Configuration Management](#configuration-management)
8. [Monitoring and Logging](#monitoring-and-logging)
9. [Troubleshooting](#troubleshooting)
10. [Security Considerations](#security-considerations)

---

## Prerequisites

### Required Tools

- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Docker Desktop** - [Download](https://www.docker.com/products/docker-desktop)
- **AWS CLI v2** - [Installation Guide](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html)
- **Git** - For version control

### AWS Account Requirements

- Active AWS account with appropriate permissions
- IAM user with permissions for:
  - ECS (Elastic Container Service)
  - ECR (Elastic Container Registry)
  - VPC and networking resources
  - CloudWatch Logs
  - Application Load Balancer (optional)
  - RDS or external database access

### Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: PostgreSQL (via Entity Framework Core)
- **Logging**: Serilog
- **Container Runtime**: Docker
- **Orchestration**: AWS ECS Fargate
- **Container Registry**: AWS ECR or Docker Hub

---

## Local Development Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd EcommerceInfContain1201
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure Database Connection

Update `src/EcommerceInformatica.Web/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ecommerce_informatica;Username=postgres;Password=your_password"
  }
}
```

### 4. Run Database Migrations

```bash
cd src/EcommerceInformatica.Web
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run --project src/EcommerceInformatica.Web
```

The application will be available at `http://localhost:5000` or `https://localhost:5001`.

---

## Docker Setup

### Build Docker Image Locally

```bash
docker build -t ecommerceinformatica-web:latest -f Dockerfile .
```

### Run Container Locally

```bash
docker run -d \
  -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e DB_HOST=host.docker.internal \
  -e DB_PORT=5432 \
  -e DB_NAME=ecommerce_informatica \
  -e DB_USER=postgres \
  -e DB_PASSWORD=your_password \
  --name ecommerce-app \
  ecommerceinformatica-web:latest
```

### Verify Container Health

```bash
curl http://localhost:8080/health
```

Expected response: `Healthy`

### Using Docker Compose

```bash
docker-compose up -d
```

This will build and start the application container with the configuration from `docker-compose.yml`.

---

## AWS ECS Fargate Prerequisites

### 1. Configure AWS CLI

```bash
aws configure
```

Provide your AWS Access Key ID, Secret Access Key, default region, and output format.

### 2. Create VPC and Networking Resources

ECS Fargate requires a VPC with at least two subnets in different Availability Zones.

#### Option A: Use Default VPC

```bash
# Get default VPC ID
aws ec2 describe-vpcs --filters "Name=isDefault,Values=true" --query "Vpcs[0].VpcId" --output text

# Get default subnets
aws ec2 describe-subnets --filters "Name=default-for-az,Values=true" --query "Subnets[*].SubnetId" --output text
```

#### Option B: Create New VPC (Recommended for Production)

Use AWS VPC wizard or CloudFormation to create a VPC with public and private subnets.

### 3. Create Security Group

Create a security group that allows inbound traffic on port 8080 (application port) and port 80 (if using ALB).

```bash
# Create security group
SG_ID=$(aws ec2 create-security-group \
  --group-name ecommerce-ecs-sg \
  --description "Security group for EcommerceInformatica ECS tasks" \
  --vpc-id <your-vpc-id> \
  --output text --query 'GroupId')

# Allow inbound traffic on port 8080
aws ec2 authorize-security-group-ingress \
  --group-id $SG_ID \
  --protocol tcp \
  --port 8080 \
  --cidr 0.0.0.0/0

# Allow inbound traffic on port 80 (for ALB)
aws ec2 authorize-security-group-ingress \
  --group-id $SG_ID \
  --protocol tcp \
  --port 80 \
  --cidr 0.0.0.0/0
```

### 4. Create IAM Roles

#### ECS Task Execution Role

This role allows ECS to pull container images from ECR and write logs to CloudWatch.

```bash
# Create trust policy
cat > ecs-task-execution-trust-policy.json <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "ecs-tasks.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
    }
  ]
}
EOF

# Create role
aws iam create-role \
  --role-name ecsTaskExecutionRole \
  --assume-role-policy-document file://ecs-task-execution-trust-policy.json

# Attach AWS managed policy
aws iam attach-role-policy \
  --role-name ecsTaskExecutionRole \
  --policy-arn arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy
```

#### ECS Task Role (Optional)

This role grants permissions to the application running in the container (e.g., accessing S3, DynamoDB).

```bash
# Create task role
aws iam create-role \
  --role-name ecsTaskRole \
  --assume-role-policy-document file://ecs-task-execution-trust-policy.json

# Attach custom policies as needed
```

### 5. Create CloudWatch Log Group

```bash
aws logs create-log-group --log-group-name /ecs/ecommerceinformatica-web
```

### 6. Set Up Database (PostgreSQL)

#### Option A: AWS RDS PostgreSQL

```bash
aws rds create-db-instance \
  --db-instance-identifier ecommerce-db \
  --db-instance-class db.t3.micro \
  --engine postgres \
  --master-username postgres \
  --master-user-password <your-secure-password> \
  --allocated-storage 20 \
  --vpc-security-group-ids <security-group-id> \
  --db-subnet-group-name <subnet-group-name> \
  --backup-retention-period 7 \
  --publicly-accessible false
```

#### Option B: External PostgreSQL

Ensure your ECS tasks can access the external database through security groups and network configuration.

---

## Building and Pushing Docker Images

### Using the Build and Push Script

The repository includes automated scripts for building and pushing Docker images.

#### Linux/macOS

```bash
chmod +x scripts/build-push.sh
./scripts/build-push.sh
```

#### Windows

```cmd
scripts\build-push.bat
```

### Script Workflow

1. **Prompts for registry selection**:
   - AWS ECR (Elastic Container Registry)
   - Docker Hub

2. **Collects registry credentials**:
   - For ECR: AWS region, account ID, repository name
   - For Docker Hub: username, password/access token

3. **Authenticates with the registry**

4. **Creates ECR repository if it doesn't exist** (for ECR option)

5. **Builds the Docker image** using the Dockerfile

6. **Pushes the image** to the selected registry

7. **Outputs the image URI** for use in deployment

### Manual Build and Push (AWS ECR)

```bash
# Set variables
AWS_REGION=us-east-1
AWS_ACCOUNT_ID=$(aws sts get-caller-identity --query Account --output text)
ECR_REPO=ecommerceinformatica-web
IMAGE_TAG=latest

# Authenticate with ECR
aws ecr get-login-password --region $AWS_REGION | \
  docker login --username AWS --password-stdin \
  ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com

# Create ECR repository (if not exists)
aws ecr create-repository --repository-name $ECR_REPO --region $AWS_REGION

# Build image
docker build -t ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${ECR_REPO}:${IMAGE_TAG} -f Dockerfile .

# Push image
docker push ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${ECR_REPO}:${IMAGE_TAG}
```

---

## ECS Fargate Deployment

### Understanding ECS Fargate

AWS Fargate is a serverless compute engine for containers that works with ECS. It eliminates the need to manage EC2 instances.

### Key Concepts

- **Task Definition**: Blueprint for your application (container image, CPU, memory, environment variables)
- **Service**: Maintains desired number of running tasks and integrates with load balancers
- **Cluster**: Logical grouping of tasks or services

### ECS Task Definition Explained

The task definition (`ecs/task-definition.json`) specifies:

- **Launch Type**: `FARGATE` (serverless)
- **Network Mode**: `awsvpc` (required for Fargate - each task gets its own ENI)
- **CPU and Memory**: Must use valid Fargate combinations:
  - CPU: 256 (.25 vCPU), 512 (.5 vCPU), 1024 (1 vCPU), 2048 (2 vCPU), 4096 (4 vCPU)
  - Memory: Depends on CPU value (see AWS documentation)
  - Default: `cpu: "512"`, `memory: "1024"`
- **Container Definitions**:
  - Image URI
  - Port mappings (containerPort only, no hostPort needed)
  - Environment variables
  - Logging configuration (CloudWatch Logs)
- **Execution Role**: IAM role for ECS to pull images and write logs
- **Task Role**: IAM role for application permissions (optional)

### ECS Service Configuration

The service definition (`ecs/service-definition.json`) specifies:

- **Desired Count**: Number of task instances (default: 2)
- **Launch Type**: `FARGATE`
- **Network Configuration**:
  - Subnets (at least 2 in different AZs)
  - Security groups
  - Public IP assignment (ENABLED for internet access)
- **Load Balancer** (optional):
  - Target group ARN
  - Container name and port
  - Health check grace period
- **Deployment Configuration**:
  - Rolling update strategy
  - Circuit breaker for automatic rollback
- **Tags**: Metadata for resource organization

### Using the Deployment Script

#### Linux/macOS

```bash
chmod +x scripts/deploy-image.sh
./scripts/deploy-image.sh
```

#### Windows

```cmd
scripts\deploy-image.bat
```

### Deployment Script Workflow

1. **Prompts for AWS configuration**:
   - AWS region
   - ECS cluster name (creates if doesn't exist)

2. **Collects network configuration**:
   - VPC ID
   - Subnet IDs (comma-separated)
   - Security group ID

3. **Prompts for Docker image URI**:
   - Use the output from the build-push script

4. **Collects database configuration**:
   - Database host (RDS endpoint)
   - Database port (default: 5432)
   - Database name
   - Database username
   - Database password

5. **Load balancer setup** (optional):
   - Asks if load balancer is needed
   - If yes: Creates Application Load Balancer and Target Group
   - Configures health checks on `/health` endpoint

6. **Registers ECS task definition**:
   - Replaces placeholders in task definition JSON
   - Registers with ECS

7. **Creates or updates ECS service**:
   - If service doesn't exist: Creates new service
   - If service exists: Updates with new task definition

8. **Waits for service stability**:
   - Monitors deployment progress
   - Ensures tasks are running and healthy

9. **Outputs deployment details**:
   - Service status and task counts
   - Application URL (if load balancer is used)
   - CloudWatch log group information

### Manual Deployment Steps

If you prefer to deploy manually:

#### 1. Register Task Definition

```bash
# Update placeholders in task-definition.json
cp ecs/task-definition.json ecs/task-definition-updated.json

# Replace placeholders (use sed or text editor)
sed -i 's|{{ACCOUNT_ID}}|123456789012|g' ecs/task-definition-updated.json
sed -i 's|{{AWS_REGION}}|us-east-1|g' ecs/task-definition-updated.json
sed -i 's|{{IMAGE_URI}}|123456789012.dkr.ecr.us-east-1.amazonaws.com/ecommerceinformatica-web:latest|g' ecs/task-definition-updated.json
sed -i 's|{{DB_HOST}}|mydb.abc123.us-east-1.rds.amazonaws.com|g' ecs/task-definition-updated.json
sed -i 's|{{DB_PORT}}|5432|g' ecs/task-definition-updated.json
sed -i 's|{{DB_NAME}}|ecommerce_informatica|g' ecs/task-definition-updated.json
sed -i 's|{{DB_USER}}|postgres|g' ecs/task-definition-updated.json
sed -i 's|{{DB_PASSWORD}}|your_password|g' ecs/task-definition-updated.json

# Register task definition
aws ecs register-task-definition --cli-input-json file://ecs/task-definition-updated.json
```

#### 2. Create ECS Service

```bash
# Update service definition
cp ecs/service-definition.json ecs/service-definition-updated.json

# Replace placeholders
sed -i 's|{{CLUSTER_NAME}}|my-ecs-cluster|g' ecs/service-definition-updated.json
sed -i 's|{{SUBNET_1}}|subnet-0abc123|g' ecs/service-definition-updated.json
sed -i 's|{{SUBNET_2}}|subnet-0def456|g' ecs/service-definition-updated.json
sed -i 's|{{SECURITY_GROUP}}|sg-0abc123def|g' ecs/service-definition-updated.json

# If using load balancer, replace target group ARN
sed -i 's|{{TARGET_GROUP_ARN}}|arn:aws:elasticloadbalancing:...|g' ecs/service-definition-updated.json

# Create service
aws ecs create-service --cli-input-json file://ecs/service-definition-updated.json
```

---

## Configuration Management

### Environment Variables

The application supports configuration through environment variables:

- `ASPNETCORE_ENVIRONMENT`: Environment name (Development, Staging, Production)
- `ASPNETCORE_URLS`: Listening URLs (default: `http://+:8080`)
- `DB_HOST`: Database host
- `DB_PORT`: Database port
- `DB_NAME`: Database name
- `DB_USER`: Database username
- `DB_PASSWORD`: Database password

### Using AWS Systems Manager Parameter Store

For sensitive configuration:

```bash
# Store database password
aws ssm put-parameter \
  --name /ecommerce/prod/db-password \
  --value "your-secure-password" \
  --type SecureString

# Reference in task definition
"secrets": [
  {
    "name": "DB_PASSWORD",
    "valueFrom": "arn:aws:ssm:region:account:parameter/ecommerce/prod/db-password"
  }
]
```

---

## Monitoring and Logging

### CloudWatch Logs

All application logs are sent to CloudWatch Logs:

- **Log Group**: `/ecs/ecommerceinformatica-web`
- **Log Stream Prefix**: `ecs`

#### View Logs

```bash
# Tail logs in real-time
aws logs tail /ecs/ecommerceinformatica-web --follow --region us-east-1

# Filter logs by pattern
aws logs filter-log-events \
  --log-group-name /ecs/ecommerceinformatica-web \
  --filter-pattern "ERROR"
```

### CloudWatch Metrics

ECS automatically publishes metrics to CloudWatch:

- CPU utilization
- Memory utilization
- Network traffic

### Health Checks

The application exposes a health check endpoint:

- **Endpoint**: `/health`
- **Response**: `Healthy` (HTTP 200)
- **Health Check**: Validates database connectivity

---

## Troubleshooting

### Common Issues

#### 1. Task Fails to Start

**Symptom**: Tasks transition from PENDING to STOPPED immediately.

**Possible Causes**:
- Invalid CPU/memory combination
- Image pull failure (check ECR permissions)
- Container crash on startup (check CloudWatch logs)

**Solution**:
```bash
# Check task stopped reason
aws ecs describe-tasks \
  --cluster my-ecs-cluster \
  --tasks <task-id> \
  --query 'tasks[0].stoppedReason'

# Check CloudWatch logs for errors
aws logs tail /ecs/ecommerceinformatica-web --follow
```

#### 2. Service Not Reaching Steady State

**Symptom**: Service stuck with pending tasks, cannot reach desired count.

**Possible Causes**:
- Health check failures
- Insufficient resources in subnets
- Security group blocking traffic

**Solution**:
```bash
# Check service events
aws ecs describe-services \
  --cluster my-ecs-cluster \
  --services ecommerceinformatica-service \
  --query 'services[0].events[:10]'

# Check target health (if using ALB)
aws elbv2 describe-target-health \
  --target-group-arn <target-group-arn>
```

#### 3. Database Connection Failures

**Symptom**: Application logs show database connection errors.

**Possible Causes**:
- Incorrect connection string
- Security group not allowing traffic from ECS tasks
- Database not accessible from ECS subnets

**Solution**:
- Verify security group rules allow PostgreSQL port (5432)
- Check database endpoint and credentials
- Ensure ECS tasks are in subnets with route to database

#### 4. Memory or CPU Issues

**Symptom**: Tasks being killed due to resource exhaustion.

**Solution**:
- Increase task CPU/memory in task definition
- Use valid Fargate CPU/memory combinations
- Monitor CloudWatch metrics to determine appropriate sizing

### Debugging Commands

```bash
# List running tasks
aws ecs list-tasks --cluster my-ecs-cluster --service-name ecommerceinformatica-service

# Describe task details
aws ecs describe-tasks --cluster my-ecs-cluster --tasks <task-id>

# Check service status
aws ecs describe-services --cluster my-ecs-cluster --services ecommerceinformatica-service

# View task logs
aws logs get-log-events \
  --log-group-name /ecs/ecommerceinformatica-web \
  --log-stream-name <log-stream-name>
```

---

## Security Considerations

### 1. Container Security

- **Non-root user**: Container runs as non-root user (`appuser`)
- **Read-only root filesystem**: Consider setting `readonlyRootFilesystem: true`
- **Minimal base image**: Uses official Microsoft ASP.NET runtime image

### 2. Network Security

- **Security groups**: Configure least-privilege rules
- **Private subnets**: Deploy tasks in private subnets with NAT gateway
- **VPC endpoints**: Use VPC endpoints for AWS services (ECR, CloudWatch)

### 3. Secrets Management

- **AWS Secrets Manager**: Store sensitive data (database passwords, API keys)
- **Parameter Store**: Use for configuration parameters
- **Never hardcode secrets**: Use environment variables or secrets references

### 4. IAM Permissions

- **Principle of least privilege**: Grant only necessary permissions
- **Task role**: Use for application-level permissions
- **Execution role**: Use for ECS infrastructure operations

### 5. Image Security

- **Scan images**: Use ECR image scanning for vulnerabilities
- **Update base images**: Regularly update to latest security patches
- **Signed images**: Consider using Docker Content Trust

---

## ECS Fargate Scaling and Management

### Auto Scaling

Configure Service Auto Scaling based on CloudWatch metrics:

```bash
# Register scalable target
aws application-autoscaling register-scalable-target \
  --service-namespace ecs \
  --resource-id service/my-ecs-cluster/ecommerceinformatica-service \
  --scalable-dimension ecs:service:DesiredCount \
  --min-capacity 2 \
  --max-capacity 10

# Create scaling policy (target tracking)
aws application-autoscaling put-scaling-policy \
  --service-namespace ecs \
  --resource-id service/my-ecs-cluster/ecommerceinformatica-service \
  --scalable-dimension ecs:service:DesiredCount \
  --policy-name cpu-target-tracking \
  --policy-type TargetTrackingScaling \
  --target-tracking-scaling-policy-configuration file://scaling-policy.json
```

### Blue/Green Deployments

Use AWS CodeDeploy for blue/green deployments with zero downtime:

```bash
# Create deployment group
aws deploy create-deployment-group \
  --application-name AppECS-my-ecs-cluster-ecommerceinformatica-service \
  --deployment-group-name ecommerce-dg \
  --deployment-config-name CodeDeployDefault.ECSAllAtOnce \
  --ecs-services clusterName=my-ecs-cluster,serviceName=ecommerceinformatica-service \
  --load-balancer-info targetGroupPairInfoList=[...]
```

### Rolling Updates

Update service with new task definition:

```bash
aws ecs update-service \
  --cluster my-ecs-cluster \
  --service ecommerceinformatica-service \
  --task-definition ecommerceinformatica-task:2 \
  --force-new-deployment
```

---

## Cost Optimization

- **Right-size resources**: Start with minimal CPU/memory and scale based on metrics
- **Use Fargate Spot**: For non-critical workloads, use Fargate Spot for up to 70% savings
- **Optimize images**: Reduce image size to minimize storage and transfer costs
- **Monitor usage**: Use AWS Cost Explorer to track ECS costs

---

## Additional Resources

- [AWS ECS Documentation](https://docs.aws.amazon.com/ecs/)
- [AWS Fargate Documentation](https://docs.aws.amazon.com/AmazonECS/latest/developerguide/AWS_Fargate.html)
- [ASP.NET Core Docker Documentation](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)

---

## Support

For issues or questions:
- Check CloudWatch logs for application errors
- Review ECS service events for deployment issues
- Consult AWS support or documentation

---

**Last Updated**: 2026-01-12
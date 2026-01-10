using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.SupplierName,
                opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.BusinessName : null))
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
            .ForMember(dest => dest.BrandName,
                opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null));

        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceDetails, opt => opt.Ignore());

        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.Supplier, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceDetails, opt => opt.Ignore());

        // Category mappings
        CreateMap<Category, CategoryDto>();

        CreateMap<CategoryCreateDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        // Brand mappings
        CreateMap<Brand, BrandDto>();

        CreateMap<BrandCreateDto, Brand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BrandId, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        CreateMap<BrandUpdateDto, Brand>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        // Supplier mappings
        CreateMap<Supplier, SupplierDto>();

        CreateMap<SupplierCreateDto, Supplier>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        CreateMap<SupplierUpdateDto, Supplier>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        // Person mappings
        CreateMap<Person, PersonDto>();

        CreateMap<PersonCreateDto, Person>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PersonUpdateDto, Person>();

        // TODO: Uncomment these mappings when DTOs and Services are implemented
        /*
        // Customer mappings
        CreateMap<Customer, CustomerDto>();

        CreateMap<CustomerCreateDto, Customer>()
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        CreateMap<CustomerUpdateDto, Customer>()
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        // Employee mappings
        CreateMap<Employee, EmployeeDto>();

        CreateMap<EmployeeCreateDto, Employee>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
            .ForMember(dest => dest.Manager, opt => opt.Ignore())
            .ForMember(dest => dest.Subordinates, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        CreateMap<EmployeeUpdateDto, Employee>()
            .ForMember(dest => dest.Manager, opt => opt.Ignore())
            .ForMember(dest => dest.Subordinates, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        // Invoice mappings
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CompanyName : null))
            .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src => src.Employee != null ?
                    $"{src.Employee.FirstName} {src.Employee.LastName}" : null))
            .ForMember(dest => dest.ShipperName,
                opt => opt.MapFrom(src => src.Shipper != null ? src.Shipper.CompanyName : null));

        CreateMap<InvoiceCreateDto, Invoice>()
            .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ForMember(dest => dest.Shipper, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceDetails, opt => opt.Ignore());

        CreateMap<InvoiceUpdateDto, Invoice>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Employee, opt => opt.Ignore())
            .ForMember(dest => dest.Shipper, opt => opt.Ignore())
            .ForMember(dest => dest.InvoiceDetails, opt => opt.Ignore());

        // InvoiceDetail mappings
        CreateMap<InvoiceDetail, InvoiceDetailDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null));

        CreateMap<InvoiceDetailCreateDto, InvoiceDetail>()
            .ForMember(dest => dest.Invoice, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        CreateMap<InvoiceDetailUpdateDto, InvoiceDetail>()
            .ForMember(dest => dest.Invoice, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

        // Shipper mappings
        CreateMap<Shipper, ShipperDto>();

        CreateMap<ShipperCreateDto, Shipper>()
            .ForMember(dest => dest.ShipperId, opt => opt.Ignore())
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());

        CreateMap<ShipperUpdateDto, Shipper>()
            .ForMember(dest => dest.Invoices, opt => opt.Ignore());
        */
    }
}

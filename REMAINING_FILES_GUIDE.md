# Remaining Files to Create

This document contains templates for all remaining CRUD pages that follow the same pattern.

## Suppliers CRUD Pages
All Supplier pages follow the same pattern as Products/Categories/Brands but with Supplier-specific fields:
- CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax, HomePage

Files needed:
- /Pages/Suppliers/Create.cshtml + Create.cshtml.cs
- /Pages/Suppliers/Edit.cshtml + Edit.cshtml.cs
- /Pages/Suppliers/Details.cshtml + Details.cshtml.cs
- /Pages/Suppliers/Delete.cshtml + Delete.cshtml.cs

## Persons CRUD Pages
All Person pages follow the same pattern with Person-specific fields:
- FirstName, LastName, BirthDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes

Files needed:
- /Pages/Persons/Index.cshtml + Index.cshtml.cs
- /Pages/Persons/Create.cshtml + Create.cshtml.cs
- /Pages/Persons/Edit.cshtml + Edit.cshtml.cs
- /Pages/Persons/Details.cshtml + Details.cshtml.cs
- /Pages/Persons/Delete.cshtml + Delete.cshtml.cs

## Implementation Pattern
Each CRUD set follows this pattern:
1. Index: List all items with search/filter, table display, action buttons
2. Create: Form with validation, file uploads where needed, manual DTO mapping
3. Edit: Form pre-populated with existing data, manual DTO mapping
4. Details: Read-only view with all fields, action buttons
5. Delete: Confirmation page with item details, post handler for deletion

All pages use:
- Proper authorization [Authorize]
- Error handling with try-catch
- TempData for success/error messages
- Manual ViewModel to DTO mapping (NO AutoMapper in Web layer)
- Bootstrap 5 for styling
- Bootstrap Icons for icons
- Proper logging

The pattern established in Products, Categories, and Brands should be followed exactly for Suppliers and Persons.

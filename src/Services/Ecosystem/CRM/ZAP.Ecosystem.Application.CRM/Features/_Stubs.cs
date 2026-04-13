// ============================================================
// CRM Feature Stubs
// Contains Query/Command types and stub handlers for features
// that are pending full migration from legacy CRM.BuildingBlocks.
// Each stub returns an empty but valid response so controllers
// can register their endpoints immediately.
// ============================================================
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Shared.Data;

// ---------- BRANDS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries
{
    public class GetBrandListQuery : IRequest<PagedResult<DTOs.BrandDto>>
    {
        public DTOs.BrandListRequestDto Request { get; set; } = new();
    }
    public class GetBrandByIdQuery : IRequest<DTOs.BrandDto?> { public Guid Id { get; set; } }

    public class GetBrandListQueryHandler : IRequestHandler<GetBrandListQuery, PagedResult<DTOs.BrandDto>>
    {
        public Task<PagedResult<DTOs.BrandDto>> Handle(GetBrandListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.BrandDto>(new List<DTOs.BrandDto>(), 0, request.Request.PageIndex, request.Request.PageSize));
    }
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, DTOs.BrandDto?>
    {
        public Task<DTOs.BrandDto?> Handle(GetBrandByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.BrandDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Commands
{
    using MediatR;
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Guid>
    {
        public Task<Guid> Handle(CreateBrandCommand request, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        public Task<bool> Handle(UpdateBrandCommand request, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- CATEGORIES ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Queries
{
    public class GetCategoryListQuery : IRequest<PagedResult<DTOs.CategoryDto>>
    {
        public DTOs.CategoryListRequestDto Request { get; set; } = new();
    }
    public class GetCategoryByIdQuery : IRequest<DTOs.CategoryDto?> { public Guid Id { get; set; } }
    public class GetCategoriesQuery : IRequest<IEnumerable<DTOs.CategoryDto>> { }

    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, PagedResult<DTOs.CategoryDto>>
    {
        public Task<PagedResult<DTOs.CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.CategoryDto>(new List<DTOs.CategoryDto>(), 0, request.Request.PageIndex, request.Request.PageSize));
    }
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, DTOs.CategoryDto?>
    {
        public Task<DTOs.CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.CategoryDto?>(null);
    }
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<DTOs.CategoryDto>>
    {
        public Task<IEnumerable<DTOs.CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken ct)
            => Task.FromResult<IEnumerable<DTOs.CategoryDto>>(new List<DTOs.CategoryDto>());
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Commands
{
    using MediatR;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        public Task<Guid> Handle(CreateCategoryCommand request, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        public Task<bool> Handle(UpdateCategoryCommand request, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- CUSTOMERS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries
{
    public class GetCustomerListQuery : IRequest<PagedResult<DTOs.CustomerDto>>
    {
        public DTOs.CustomerListRequestDto Request { get; set; } = new();
    }
    public class GetCustomerByIdQuery : IRequest<DTOs.CustomerDto?>
    {
        public string Id { get; set; } = string.Empty;
    }
    public class GetCustomerGroupListQuery : IRequest<PagedResult<DTOs.CustomerGroupDto>>
    {
        public DTOs.CustomerListRequestDto Request { get; set; } = new();
    }
    public class GetCustomerGroupByIdQuery : IRequest<DTOs.CustomerGroupDto?>
    {
        public string Id { get; set; } = string.Empty;
    }
    public class GetCustomerManagementListQuery : IRequest<PagedResult<DTOs.CustomerListDto>>
    {
        public DTOs.CustomerListRequestDto Request { get; set; } = new();
    }
    public class GetMembershipListQuery : IRequest<PagedResult<DTOs.MembershipListDto>>
    {
        public DTOs.CustomerListRequestDto Request { get; set; } = new();
    }

    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, PagedResult<DTOs.CustomerDto>>
    {
        public Task<PagedResult<DTOs.CustomerDto>> Handle(GetCustomerListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.CustomerDto>(new List<DTOs.CustomerDto>(), 0, 1, 20));
    }
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, DTOs.CustomerDto?>
    {
        public Task<DTOs.CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.CustomerDto?>(null);
    }
    public class GetCustomerGroupListQueryHandler : IRequestHandler<GetCustomerGroupListQuery, PagedResult<DTOs.CustomerGroupDto>>
    {
        public Task<PagedResult<DTOs.CustomerGroupDto>> Handle(GetCustomerGroupListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.CustomerGroupDto>(new List<DTOs.CustomerGroupDto>(), 0, 1, 20));
    }
    public class GetCustomerGroupByIdQueryHandler : IRequestHandler<GetCustomerGroupByIdQuery, DTOs.CustomerGroupDto?>
    {
        public Task<DTOs.CustomerGroupDto?> Handle(GetCustomerGroupByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.CustomerGroupDto?>(null);
    }
    public class GetCustomerManagementListQueryHandler : IRequestHandler<GetCustomerManagementListQuery, PagedResult<DTOs.CustomerListDto>>
    {
        public Task<PagedResult<DTOs.CustomerListDto>> Handle(GetCustomerManagementListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.CustomerListDto>(new List<DTOs.CustomerListDto>(), 0, 1, 20));
    }
    public class GetMembershipListQueryHandler : IRequestHandler<GetMembershipListQuery, PagedResult<DTOs.MembershipListDto>>
    {
        public Task<PagedResult<DTOs.MembershipListDto>> Handle(GetMembershipListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.MembershipListDto>(new List<DTOs.MembershipListDto>(), 0, 1, 20));
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Commands
{
    using MediatR;
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerGroupCommand, Guid>
    {
        public Task<Guid> Handle(CreateCustomerGroupCommand request, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        public Task<bool> Handle(UpdateCustomerCommand request, CancellationToken ct) => Task.FromResult(false);
    }
    public class UpdateCustomerGroupCommandHandler : IRequestHandler<UpdateCustomerGroupCommand, bool>
    {
        public Task<bool> Handle(UpdateCustomerGroupCommand request, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- DINING OPTIONS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.DTOs
{
    public class DiningOptionListRequestDto { public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20; }
}
namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Queries
{
    public class GetDiningOptionListQuery : IRequest<PagedResult<DTOs.DiningOptionDto>>
    {
        public DTOs.DiningOptionListRequestDto Request { get; set; } = new();
    }
    public class GetDiningOptionByIdQuery : IRequest<DTOs.DiningOptionDto?> { public Guid Id { get; set; } }

    public class GetDiningOptionListQueryHandler : IRequestHandler<GetDiningOptionListQuery, PagedResult<DTOs.DiningOptionDto>>
    {
        public Task<PagedResult<DTOs.DiningOptionDto>> Handle(GetDiningOptionListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.DiningOptionDto>(new List<DTOs.DiningOptionDto>(), 0, 1, 20));
    }
    public class GetDiningOptionByIdQueryHandler : IRequestHandler<GetDiningOptionByIdQuery, DTOs.DiningOptionDto?>
    {
        public Task<DTOs.DiningOptionDto?> Handle(GetDiningOptionByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.DiningOptionDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Commands
{
    using MediatR;
    public class CreateDiningOptionCommand : IRequest<Guid> { public string Name { get; set; } = string.Empty; }
    public class UpdateDiningOptionCommand : IRequest<bool> { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; }
    public class CreateDiningOptionCommandHandler : IRequestHandler<CreateDiningOptionCommand, Guid>
    {
        public Task<Guid> Handle(CreateDiningOptionCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateDiningOptionCommandHandler : IRequestHandler<UpdateDiningOptionCommand, bool>
    {
        public Task<bool> Handle(UpdateDiningOptionCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- EMPLOYEES (HRs) ----------
namespace ZAP.Ecosystem.Application.CRM.Features.HRs.v1.DTOs
{
    public class EmployeeDto { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; public string Email { get; set; } = string.Empty; public bool IsActive { get; set; } }
    public class EmployeeListRequestDto { public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20; }
}
namespace ZAP.Ecosystem.Application.CRM.Features.HRs.v1.Queries
{
    public class GetEmployeeListQuery : IRequest<PagedResult<DTOs.EmployeeDto>>
    {
        public DTOs.EmployeeListRequestDto Request { get; set; } = new();
    }
    public class GetEmployeeByIdQuery : IRequest<DTOs.EmployeeDto?> { public Guid Id { get; set; } }

    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, PagedResult<DTOs.EmployeeDto>>
    {
        public Task<PagedResult<DTOs.EmployeeDto>> Handle(GetEmployeeListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.EmployeeDto>(new List<DTOs.EmployeeDto>(), 0, 1, 20));
    }
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, DTOs.EmployeeDto?>
    {
        public Task<DTOs.EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.EmployeeDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.HRs.v1.Commands
{
    using MediatR;
    public class CreateEmployeeCommand : IRequest<Guid> { public string Name { get; set; } = string.Empty; }
    public class UpdateEmployeeCommand : IRequest<bool> { public Guid Id { get; set; } }
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        public Task<Guid> Handle(CreateEmployeeCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        public Task<bool> Handle(UpdateEmployeeCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- LOCATIONS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries
{
    public class GetLocationListQuery : IRequest<PagedResult<DTOs.LocationDto>>
    {
        public DTOs.LocationListRequestDto Request { get; set; } = new();
    }
    public class GetLocationByIdQuery : IRequest<DTOs.LocationDto?> { public Guid Id { get; set; } }
    public class GetProvinceListQuery : IRequest<IEnumerable<DTOs.ProvinceDto>> { }

    public class GetLocationListQueryHandler : IRequestHandler<GetLocationListQuery, PagedResult<DTOs.LocationDto>>
    {
        public Task<PagedResult<DTOs.LocationDto>> Handle(GetLocationListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.LocationDto>(new List<DTOs.LocationDto>(), 0, 1, 20));
    }
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, DTOs.LocationDto?>
    {
        public Task<DTOs.LocationDto?> Handle(GetLocationByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.LocationDto?>(null);
    }
    public class GetProvinceListQueryHandler : IRequestHandler<GetProvinceListQuery, IEnumerable<DTOs.ProvinceDto>>
    {
        public Task<IEnumerable<DTOs.ProvinceDto>> Handle(GetProvinceListQuery request, CancellationToken ct)
            => Task.FromResult<IEnumerable<DTOs.ProvinceDto>>(new List<DTOs.ProvinceDto>());
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Commands
{
    using MediatR;
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Guid>
    {
        public Task<Guid> Handle(CreateLocationCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, bool>
    {
        public Task<bool> Handle(UpdateLocationCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- MANAGEMENT (Price List) ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Management.v1.DTOs
{
    public class ManagementListRequestDto { public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20; }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Management.v1.Queries
{
    public class GetPriceListQuery : IRequest<PagedResult<DTOs.PriceListDto>>
    {
        public DTOs.ManagementListRequestDto Request { get; set; } = new();
    }
    public class GetPriceListQueryHandler : IRequestHandler<GetPriceListQuery, PagedResult<DTOs.PriceListDto>>
    {
        public Task<PagedResult<DTOs.PriceListDto>> Handle(GetPriceListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.PriceListDto>(new List<DTOs.PriceListDto>(), 0, 1, 20));
    }
}

// ---------- MENU ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Queries
{
    public class GetMenuListQuery : IRequest<PagedResult<DTOs.MenuListResultDto>>
    {
        public DTOs.MenuListRequestDto Request { get; set; } = new();
    }
    public class GetMenuListQueryHandler : IRequestHandler<GetMenuListQuery, PagedResult<DTOs.MenuListResultDto>>
    {
        public Task<PagedResult<DTOs.MenuListResultDto>> Handle(GetMenuListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.MenuListResultDto>(new List<DTOs.MenuListResultDto>(), 0, 1, 20));
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Commands
{
    using MediatR;
    public class DeleteMenuCommand : IRequest<bool> { public Guid Id { get; set; } }
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, bool>
    {
        public Task<bool> Handle(DeleteMenuCommand r, CancellationToken ct) => Task.FromResult(false);
    }
    public class CreateMenuCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        public Task<Guid> Handle(CreateProductCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        public Task<bool> Handle(UpdateProductCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- MODIFIER GROUPS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Queries
{
    public class GetModifierGroupsQuery : IRequest<PagedResult<DTOs.ModifierGroupDto>>
    {
        public DTOs.ModifierGroupListRequestDto Request { get; set; } = new();
    }
    public class GetModifierGroupByIdQuery : IRequest<DTOs.ModifierGroupDto?> { public Guid Id { get; set; } }

    public class GetModifierGroupsQueryHandler : IRequestHandler<GetModifierGroupsQuery, PagedResult<DTOs.ModifierGroupDto>>
    {
        public Task<PagedResult<DTOs.ModifierGroupDto>> Handle(GetModifierGroupsQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.ModifierGroupDto>(new List<DTOs.ModifierGroupDto>(), 0, 1, 20));
    }
    public class GetModifierGroupByIdQueryHandler : IRequestHandler<GetModifierGroupByIdQuery, DTOs.ModifierGroupDto?>
    {
        public Task<DTOs.ModifierGroupDto?> Handle(GetModifierGroupByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.ModifierGroupDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Commands
{
    using MediatR;
    public class CreateModifierGroupCommandHandler : IRequestHandler<CreateModifierGroupCommand, Guid>
    {
        public Task<Guid> Handle(CreateModifierGroupCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateModifierGroupCommandHandler : IRequestHandler<UpdateModifierGroupCommand, bool>
    {
        public Task<bool> Handle(UpdateModifierGroupCommand r, CancellationToken ct) => Task.FromResult(false);
    }
    public class DeleteModifierGroupCommandHandler : IRequestHandler<DeleteModifierGroupCommand, bool>
    {
        public Task<bool> Handle(DeleteModifierGroupCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- ORDERS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Orders.v1.DTOs
{
    public class OrderDto { public Guid Id { get; set; } public string OrderNumber { get; set; } = string.Empty; public decimal TotalAmount { get; set; } public string Status { get; set; } = string.Empty; public DateTime CreatedAt { get; set; } }
    public class OrderListRequestDto { public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20; }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Orders.v1.Queries
{
    public class GetOrderListQuery : IRequest<PagedResult<DTOs.OrderDto>>
    {
        public DTOs.OrderListRequestDto Request { get; set; } = new();
    }
    public class GetOrderByIdQuery : IRequest<DTOs.OrderDto?> { public Guid Id { get; set; } }
    public class GetTransactionManagementListQuery : IRequest<PagedResult<DTOs.OrderDto>>
    {
        public DTOs.OrderListRequestDto Request { get; set; } = new();
    }

    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, PagedResult<DTOs.OrderDto>>
    {
        public Task<PagedResult<DTOs.OrderDto>> Handle(GetOrderListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.OrderDto>(new List<DTOs.OrderDto>(), 0, 1, 20));
    }
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, DTOs.OrderDto?>
    {
        public Task<DTOs.OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.OrderDto?>(null);
    }
    public class GetTransactionManagementListQueryHandler : IRequestHandler<GetTransactionManagementListQuery, PagedResult<DTOs.OrderDto>>
    {
        public Task<PagedResult<DTOs.OrderDto>> Handle(GetTransactionManagementListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.OrderDto>(new List<DTOs.OrderDto>(), 0, 1, 20));
    }
}

// ---------- ORGANIZATIONS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Organizations.v1.DTOs
{
    public class OrganizationDto { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; public bool IsActive { get; set; } }
    public class OrganizationListRequestDto { public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20; public string? Search { get; set; } }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Organizations.v1.Queries
{
    public class GetOrganizationListQuery : IRequest<PagedResult<DTOs.OrganizationDto>>
    {
        public DTOs.OrganizationListRequestDto Request { get; set; } = new();
    }
    public class GetOrganizationByIdQuery : IRequest<DTOs.OrganizationDto?> { public Guid Id { get; set; } }
    public class GetOrganizationListQueryHandler : IRequestHandler<GetOrganizationListQuery, PagedResult<DTOs.OrganizationDto>>
    {
        public Task<PagedResult<DTOs.OrganizationDto>> Handle(GetOrganizationListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.OrganizationDto>(new List<DTOs.OrganizationDto>(), 0, 1, 20));
    }
    public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, DTOs.OrganizationDto?>
    {
        public Task<DTOs.OrganizationDto?> Handle(GetOrganizationByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.OrganizationDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Organizations.v1.Commands
{
    using MediatR;
    public class CreateOrganizationCommand : IRequest<Guid> { public string Name { get; set; } = string.Empty; }
    public class UpdateOrganizationCommand : IRequest<bool> { public Guid Id { get; set; } }
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Guid>
    {
        public Task<Guid> Handle(CreateOrganizationCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, bool>
    {
        public Task<bool> Handle(UpdateOrganizationCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- PAYMENTS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries
{
    public class GetPaymentListQuery : IRequest<PagedResult<DTOs.PaymentListDto>>
    {
        public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20;
    }
    public class GetPaymentTermsListQuery : IRequest<PagedResult<DTOs.PaymentTermsDto>>
    {
        public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20;
    }
    public class GetPaymentTermsByIdQuery : IRequest<DTOs.PaymentTermsDto?> { public Guid Id { get; set; } }
    public class GetPaymentTypeListQuery : IRequest<PagedResult<DTOs.PaymentTypeDto>>
    {
        public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20;
    }
    public class GetPaymentTypeByIdQuery : IRequest<DTOs.PaymentTypeDto?> { public Guid Id { get; set; } }

    public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQuery, PagedResult<DTOs.PaymentListDto>>
    {
        public Task<PagedResult<DTOs.PaymentListDto>> Handle(GetPaymentListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.PaymentListDto>(new List<DTOs.PaymentListDto>(), 0, request.PageIndex, request.PageSize));
    }
    public class GetPaymentTermsListQueryHandler : IRequestHandler<GetPaymentTermsListQuery, PagedResult<DTOs.PaymentTermsDto>>
    {
        public Task<PagedResult<DTOs.PaymentTermsDto>> Handle(GetPaymentTermsListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.PaymentTermsDto>(new List<DTOs.PaymentTermsDto>(), 0, request.PageIndex, request.PageSize));
    }
    public class GetPaymentTermsByIdQueryHandler : IRequestHandler<GetPaymentTermsByIdQuery, DTOs.PaymentTermsDto?>
    {
        public Task<DTOs.PaymentTermsDto?> Handle(GetPaymentTermsByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.PaymentTermsDto?>(null);
    }
    public class GetPaymentTypeListQueryHandler : IRequestHandler<GetPaymentTypeListQuery, PagedResult<DTOs.PaymentTypeDto>>
    {
        public Task<PagedResult<DTOs.PaymentTypeDto>> Handle(GetPaymentTypeListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.PaymentTypeDto>(new List<DTOs.PaymentTypeDto>(), 0, request.PageIndex, request.PageSize));
    }
    public class GetPaymentTypeByIdQueryHandler : IRequestHandler<GetPaymentTypeByIdQuery, DTOs.PaymentTypeDto?>
    {
        public Task<DTOs.PaymentTypeDto?> Handle(GetPaymentTypeByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.PaymentTypeDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands
{
    using MediatR;
    public class CreatePaymentTermsCommandHandler : IRequestHandler<CreatePaymentTermsCommand, Guid>
    {
        public Task<Guid> Handle(CreatePaymentTermsCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdatePaymentTermsCommandHandler : IRequestHandler<UpdatePaymentTermsCommand, bool>
    {
        public Task<bool> Handle(UpdatePaymentTermsCommand r, CancellationToken ct) => Task.FromResult(false);
    }
    public class CreatePaymentTypeCommandHandler : IRequestHandler<CreatePaymentTypeCommand, Guid>
    {
        public Task<Guid> Handle(CreatePaymentTypeCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdatePaymentTypeCommandHandler : IRequestHandler<UpdatePaymentTypeCommand, bool>
    {
        public Task<bool> Handle(UpdatePaymentTypeCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- PROMOTIONS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.DTOs
{
    public class PromotionDto { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; public string Type { get; set; } = string.Empty; public bool IsActive { get; set; } }
    public class PromotionListRequestDto { public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20; }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Promotions.Queries
{
    public class GetPromotionListQuery : IRequest<PagedResult<DTOs.PromotionDto>>
    {
        public DTOs.PromotionListRequestDto Request { get; set; } = new();
    }
    public class GetPromotionByIdQuery : IRequest<DTOs.PromotionDto?> { public Guid Id { get; set; } }
    public class GetPromotionListQueryHandler : IRequestHandler<GetPromotionListQuery, PagedResult<DTOs.PromotionDto>>
    {
        public Task<PagedResult<DTOs.PromotionDto>> Handle(GetPromotionListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.PromotionDto>(new List<DTOs.PromotionDto>(), 0, 1, 20));
    }
    public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, DTOs.PromotionDto?>
    {
        public Task<DTOs.PromotionDto?> Handle(GetPromotionByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.PromotionDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Promotions.Commands
{
    using MediatR;
    public class CreatePromotionCommand : IRequest<Guid> { public string Name { get; set; } = string.Empty; }
    public class UpdatePromotionCommand : IRequest<bool> { public Guid Id { get; set; } }
    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, Guid>
    {
        public Task<Guid> Handle(CreatePromotionCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, bool>
    {
        public Task<bool> Handle(UpdatePromotionCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- REPORTS ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.DTOs
{
    public class ReportDto { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; public string Type { get; set; } = string.Empty; }
    public class ReportListRequestDto { public int PageIndex { get; set; } = 1; public int PageSize { get; set; } = 20; }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Reports.Queries
{
    public class GetReportListQuery : IRequest<PagedResult<DTOs.ReportDto>>
    {
        public DTOs.ReportListRequestDto Request { get; set; } = new();
    }
    public class GetReportByIdQuery : IRequest<DTOs.ReportDto?> { public Guid Id { get; set; } }
    public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, PagedResult<DTOs.ReportDto>>
    {
        public Task<PagedResult<DTOs.ReportDto>> Handle(GetReportListQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.ReportDto>(new List<DTOs.ReportDto>(), 0, 1, 20));
    }
    public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, DTOs.ReportDto?>
    {
        public Task<DTOs.ReportDto?> Handle(GetReportByIdQuery request, CancellationToken ct)
            => Task.FromResult<DTOs.ReportDto?>(null);
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Reports.Commands
{
    using MediatR;
    public class CreateReportCommand : IRequest<Guid> { public string Name { get; set; } = string.Empty; }
    public class UpdateReportCommand : IRequest<bool> { public Guid Id { get; set; } }
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Guid>
    {
        public Task<Guid> Handle(CreateReportCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, bool>
    {
        public Task<bool> Handle(UpdateReportCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

// ---------- UNITS (UOM) ----------
namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Queries
{
    public class GetUnitsQuery : IRequest<PagedResult<DTOs.UnitDto>>
    {
        public DTOs.UnitListRequestDto Request { get; set; } = new();
    }
    public class GetUnitsQueryHandler : IRequestHandler<GetUnitsQuery, PagedResult<DTOs.UnitDto>>
    {
        public Task<PagedResult<DTOs.UnitDto>> Handle(GetUnitsQuery request, CancellationToken ct)
            => Task.FromResult(new PagedResult<DTOs.UnitDto>(new List<DTOs.UnitDto>(), 0, 1, 20));
    }
}
namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Commands
{
    using MediatR;
    public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, Guid>
    {
        public Task<Guid> Handle(CreateUnitCommand r, CancellationToken ct) => Task.FromResult(Guid.NewGuid());
    }
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateAndDeleteUnitCommands.UpdateUnitCommand, bool>
    {
        public Task<bool> Handle(UpdateAndDeleteUnitCommands.UpdateUnitCommand r, CancellationToken ct) => Task.FromResult(false);
    }
    public class DeleteUnitCommand : IRequest<bool> { public Guid Id { get; set; } }
    public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, bool>
    {
        public Task<bool> Handle(DeleteUnitCommand r, CancellationToken ct) => Task.FromResult(false);
    }
}

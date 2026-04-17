using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Shared.Interfaces;
using ZAP.CRM.Catalog.Domain;
using MediatR;
using FluentValidation;
using ZAP.CRM.Catalog.Application.Features.Locations.v1.Queries;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Locations.v1.Validators;
    public class GetLocationListQueryValidator : AbstractValidator<GetLocationListQuery>
    {
        public GetLocationListQueryValidator()
        {
            RuleFor(x => x.Request.page_index).GreaterThan(0);
            RuleFor(x => x.Request.page_size).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }




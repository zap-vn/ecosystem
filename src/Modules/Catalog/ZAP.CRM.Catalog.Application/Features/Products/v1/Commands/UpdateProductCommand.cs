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
using System;
using System.Collections.Generic;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Products.v1.Commands;
    public class UpdateProductCommand : IRequest<object>
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescriptionHtml { get; set; }
        public int StatusId { get; set; }
        public bool IsFeatured { get; set; }
        public int ProductTypeId { get; set; } = 1; // 1: PHYSICAL

        public List<UpdateProductVariantCommand> Variants { get; set; } = new List<UpdateProductVariantCommand>();
    }

    public class UpdateProductVariantCommand
    {
        public Guid? Id { get; set; }
        public string? VariantName { get; set; }
        public string? SkuCode { get; set; }
        public string? Barcode { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? Uom { get; set; }
        public bool IsActive { get; set; }
    }




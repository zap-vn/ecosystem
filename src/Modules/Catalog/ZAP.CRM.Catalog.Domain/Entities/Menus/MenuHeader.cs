using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using ZAP.Ecosystem.Shared.Entities;

namespace ZAP.CRM.Catalog.Domain.Entities.Menus;
    [Table("menu_header", Schema = "catalog")]
    public class MenuHeader
    {
        public Guid id { get; set; }
        public int serial_id { get; set; }
        public string name { get; set; }
        public string? menu_type { get; set; }
        public Guid? app_id { get; set; }
        public int status_id { get; set; }
        [NotMapped] public string? status_code { get; set; }
        [NotMapped] public string? status_name { get; set; }
        public string? timezone_id { get; set; }
        public bool is_active { get; set; }
        
        [ForeignKey("menu_id")]
        public ICollection<MenuItemHd> sections { get; set; } = new List<MenuItemHd>();
        [NotMapped]
        public int TotalItems { get; set; }
    }

    [Table("menu_section", Schema = "catalog")]
    public class MenuItemHd
    {
        [Key]
        public Guid id { get; set; }
        public Guid menu_id { get; set; }
        public string name { get; set; }
        public int sort_order { get; set; }
        public bool is_active { get; set; }
    }






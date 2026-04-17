using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System;

namespace ZAP.Ecosystem.Sales.Domain.Entities;
    [Table("dining_option", Schema = "commerce")]
    public class DiningOption
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string? code { get; set; }
        public string? name { get; set; }
        public bool is_active { get; set; } = true;
    }





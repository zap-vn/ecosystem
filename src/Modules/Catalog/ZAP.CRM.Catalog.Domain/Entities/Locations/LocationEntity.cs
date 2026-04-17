using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZAP.Ecosystem.Shared.Entities;

namespace ZAP.CRM.Catalog.Domain.Entities.Locations;
[Table("location", Schema = "catalog")]
[PrimaryKey(nameof(Id))]
public class LocationEntity
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

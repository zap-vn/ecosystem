using System;
using System.ComponentModel.DataAnnotations.Schema;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Shared.Entities;

[Table("entity_field_dictionary", Schema = "system")]
public class EntityFieldDictionary : BaseEntity
{
    [Column("serial_id")]
    public int SerialId { get; set; }

    [Column("serial_number")]
    public string? SerialNumber { get; set; }

    [Column("entity_dictionary_id")]
    public Guid EntityDictionaryId { get; set; }

    [Column("field_name")]
    public string FieldName { get; set; } = string.Empty;

    [Column("data_type")]
    public string DataType { get; set; } = "string";

    [Column("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [Column("ui_component_type")]
    public string UiComponentType { get; set; } = "text";

    [Column("is_required")]
    public bool IsRequired { get; set; } = false;

    [Column("is_visible_list")]
    public bool IsVisibleList { get; set; } = true;

    [Column("is_visible_detail")]
    public bool IsVisibleDetail { get; set; } = true;

    [Column("is_readonly")]
    public bool IsReadonly { get; set; } = false;

    [Column("is_searchable")]
    public bool IsSearchable { get; set; } = false;

    [Column("is_filterable")]
    public bool IsFilterable { get; set; } = false;

    [Column("is_sortable")]
    public bool IsSortable { get; set; } = false;

    [Column("default_value")]
    public string? DefaultValue { get; set; }

    [Column("min_length")]
    public int? MinLength { get; set; }

    [Column("max_length")]
    public int? MaxLength { get; set; }

    [Column("regex_pattern")]
    public string? RegexPattern { get; set; }

    [Column("lookup_reference")]
    public string? LookupReference { get; set; }

    [Column("tooltip_text")]
    public string? TooltipText { get; set; }

    [Column("sort_order")]
    public int SortOrder { get; set; } = 0;

    [System.Text.Json.Serialization.JsonIgnore]
    [ForeignKey(nameof(EntityDictionaryId))]
    public virtual EntityDictionary? Entity { get; set; }
}

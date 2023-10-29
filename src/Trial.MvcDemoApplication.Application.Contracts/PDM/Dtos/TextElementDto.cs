using System;
using Volo.Abp.Application.Dtos;

namespace Trial.MvcDemoApplication.PDM;

public class TextElementDto: FullAuditedEntityDto<Guid>
{
    public string TextName { get; set; } = null!;
    public string UniqueTextId { get; set; } = null!;
    public bool IsProduct { get; set; }
    public bool IsSubProduct { get; set; }
    public bool IsStructure { get; set; }
    public bool IsComponent { get; set; }
    public bool IsDescriptor { get; set; }
    public bool IsOption { get; set; }
    public bool IsActive { get; set; }
    public string? German { get; set; }
    public string? French { get; set; }
    public string? Spanish { get; set; }
    public string? Russian { get; set; }
    public string? Chinese { get; set; }
    public string? Remarks { get; set; }

}

public class CreateTextElementDto
{
    public string TextName { get; set; } = null!;
    public string UniqueTextId { get; set; } = null!;
    public bool IsProduct { get; set; }
    public bool IsSubProduct { get; set; }
    public bool IsStructure { get; set; }
    public bool IsComponent { get; set; }
    public bool IsDescriptor { get; set; }
    public bool IsOption { get; set; }
    public string? German { get; set; }
    public string? French { get; set; }
    public string? Spanish { get; set; }
    public string? Russian { get; set; }
    public string? Chinese { get; set; }
    public string? Remarks { get; set; }
}
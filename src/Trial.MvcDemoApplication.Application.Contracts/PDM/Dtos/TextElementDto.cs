using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

public class CreateTextElementDto:IValidatableObject
{
    [Required]
    [StringLength(255)]
    public string TextName { get; set; } = null!;
    [Required]
    [StringLength(100)]
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
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!(IsProduct || IsSubProduct || IsStructure || IsComponent || IsDescriptor || IsOption ))
        {
            yield return new ValidationResult("The Text Type was not marked correctly");

        }
    }
}
public class TextElementListRequestDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public bool IsProduct { get; set; }
    public bool IsSubProduct { get; set; }
    public bool IsStructure { get; set; }
    public bool IsComponent { get; set; }
    public bool IsDescriptor { get; set; }
    public bool IsOption { get; set; }
}

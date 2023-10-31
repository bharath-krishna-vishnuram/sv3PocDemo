using PDM;
using System;
using Trial.MvcDemoApplication.Common.Dto;

namespace Trial.MvcDemoApplication.PDM.Dtos.Structure;

public class StructureDto : IdNameDto<Guid>
{
    public Guid NameId { get; set; }
    public string Description { get; set; } = string.Empty;
    public StructureType Type { get; protected set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

using PDM;
using System;

namespace Trial.MvcDemoApplication.PDM.Dtos.Structure;

public class CreateStructureDto
{
    public Guid NameId { get; set; }
    public string Description { get; set; } = string.Empty;
    public StructureType Type { get; protected set; }
}
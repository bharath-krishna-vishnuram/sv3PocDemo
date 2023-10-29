using System;
using Trial.MvcDemoApplication.Common.Dto;

namespace Trial.MvcDemoApplication.PDM.Dtos;

public class StructureDto : IdNameDto<Guid> 
{
    public DateTime CreationTime { get; set; }
}

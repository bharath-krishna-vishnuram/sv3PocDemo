using System;
using System.Collections.Generic;
using System.Linq;
using Trial.MvcDemoApplication.Common.Dto;

namespace Trial.MvcDemoApplication.PDM.Dtos;

public class ComponentHierarchyDto : IdNameDto<Guid>
{
    public override string ToString() => Name;
    public List<ComponentHierarchyDto> SubComponents { get; set; } = new();
}

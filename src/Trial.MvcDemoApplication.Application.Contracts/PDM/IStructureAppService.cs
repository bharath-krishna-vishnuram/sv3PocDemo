using System;
using System.Collections.Generic;
using System.Text;
using Trial.MvcDemoApplication.PDM.Dtos;
using Volo.Abp.Application.Services;

namespace Trial.MvcDemoApplication.PDM;

public interface IStructureAppService : ICrudAppService<StructureHierarchyDto, StructureDto, Guid, StructureDto, StructureDto, StructureDto>
{
}

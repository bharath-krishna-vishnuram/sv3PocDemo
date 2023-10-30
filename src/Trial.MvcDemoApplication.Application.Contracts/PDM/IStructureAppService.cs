using System;
using System.Collections.Generic;
using System.Text;
using Trial.MvcDemoApplication.Common.Dto;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Trial.MvcDemoApplication.PDM;

public interface IStructureAppService : ICrudAppService<StructureHierarchyDto, StructureDto, Guid, PagedAndFilteredResultRequestDto, CreateStructureDto, StructureDto>
{
}

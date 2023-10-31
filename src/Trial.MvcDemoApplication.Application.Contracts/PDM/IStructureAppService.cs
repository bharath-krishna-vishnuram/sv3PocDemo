using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.Common.Dto;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Trial.MvcDemoApplication.PDM;

public interface IStructureAppService : ICrudAppService<StructureDto, StructureDto, Guid, PagedAndFilteredResultRequestDto, CreateStructureDto, CreateStructureDto>
{
    Task<StructureHierarchyDto> GetStructureHierarchyAsync(Guid structureId);
}

using PDM.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trial.MvcDemoApplication.PDM.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class StructureAppService : CrudAppService<Structure, StructureHierarchyDto, StructureDto, Guid, StructureDto, StructureDto, StructureDto>, IStructureAppService
{
    readonly IReadOnlyRepository<StructureElement, Guid> _elementRepository;
    public StructureAppService(IRepository<Structure, Guid> repository,
        IReadOnlyRepository<StructureElement, Guid> elementRepository) : base(repository)
    {
        _elementRepository = elementRepository;
    }
    protected async override Task<Structure> GetEntityByIdAsync(Guid id)
    {
        var elementsQuery = await _elementRepository.WithDetailsAsync(rec => rec.AssociatedStructure, rec  => rec.SelectedComponent.SubComponents);
        var elements = await AsyncExecuter.ToListAsync(elementsQuery.Where(rec => rec.AssociatedStructureId == id).OrderBy(e => e.ElementOrder)) ?? 
            throw new EntityNotFoundException(typeof(Structure), id);
        var structure = elements.Select(rec => rec.AssociatedStructure).FirstOrDefault();
        return structure!;
    }
}

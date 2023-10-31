using PDM.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.Common.Dto;
using Trial.MvcDemoApplication.PDM.Dtos.Structure;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class StructureAppService : CrudAppService<Structure, StructureDto, StructureDto, Guid, PagedAndFilteredResultRequestDto, CreateStructureDto, CreateStructureDto>, 
    IStructureAppService
{
    readonly IReadOnlyRepository<TextElement, Guid> _textElementRepository;
    readonly IReadOnlyRepository<StructureElement, Guid> _structureElementRepository;
    public StructureAppService(IRepository<Structure, Guid> repository,
        IReadOnlyRepository<StructureElement, Guid> elementRepository,
        IReadOnlyRepository<TextElement, Guid> textElementRepository) : base(repository)
    {
        _structureElementRepository = elementRepository;
        _textElementRepository = textElementRepository;
    }
    public async Task<StructureHierarchyDto> GetStructureHierarchyAsync(Guid structureId)
    {
        var structure = await GetStructureWithHierarchyDetailsAsync(structureId);
        return ObjectMapper.Map<Structure, StructureHierarchyDto>(structure);
    }
    protected override async Task MapToEntityAsync(CreateStructureDto updateInput, Structure entity)
    {
        var newStructureName = await _textElementRepository.GetAsync(updateInput.NameId)!;
        base.MapToEntity(updateInput, entity);
        entity.UpdateName(newStructureName);
    }
    protected override async Task<Structure> MapToEntityAsync(CreateStructureDto createInput)
    {
        var structureName = await _textElementRepository.GetAsync(createInput.NameId)!;
        return Structure.CreateStructure(structureName, createInput.Description, createInput.Type);
    }
    protected override IQueryable<Structure> ApplyPaging(IQueryable<Structure> query, PagedAndFilteredResultRequestDto input)
    {
        if (!input.Filter.IsNullOrEmpty())
        {
            query = query
                .Where(rec => rec.Name.TextName.Contains(input.Filter!));
        }
        return base.ApplyPaging(query, input);
    }
    protected async Task<Structure> GetStructureWithHierarchyDetailsAsync(Guid id)
    {
        var elementsQuery = await _structureElementRepository.WithDetailsAsync(rec => rec.AssociatedStructure, rec  => rec.SelectedComponent.SubComponents);
        var elements = await AsyncExecuter.ToListAsync(elementsQuery.Where(rec => rec.AssociatedStructureId == id).OrderBy(e => e.ElementOrder)) ?? 
            throw new EntityNotFoundException(typeof(Structure), id);
        var structure = elements.Select(rec => rec.AssociatedStructure).FirstOrDefault();
        return structure!;
    }
}

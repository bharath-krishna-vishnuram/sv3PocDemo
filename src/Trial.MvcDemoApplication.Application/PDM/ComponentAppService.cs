using AutoMapper;
using PDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Trial.MvcDemoApplication.Common.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class ComponentAppService : CrudAppService<Component, ComponentDetailsDto, ComponentDto, Guid, ComponentDto, CreateUpdateComponentDto, CreateUpdateComponentDto>,
    IComponentAppService
{
    readonly IReadOnlyRepository<TextElement, Guid> _textElementRepository;
    readonly IReadOnlyRepository<ComponentDescriptor, Guid> _descriptorRepository;
    readonly IRepository<DescriptorOption, Guid> _optionRepository;
    readonly IReadOnlyRepository<StructureElement, Guid> _structureElementRepository;
    public ComponentAppService(IRepository<Component, Guid> repository,
        IReadOnlyRepository<TextElement, Guid> textElementRepository,
        IReadOnlyRepository<ComponentDescriptor, Guid> descriptorRepository,
        IReadOnlyRepository<StructureElement, Guid> structureElementRepository,
        IRepository<DescriptorOption, Guid> optionRepository
        ) : base(repository)
    {
        _textElementRepository = textElementRepository;
        _optionRepository = optionRepository;
        _descriptorRepository = descriptorRepository;
        _structureElementRepository = structureElementRepository;
    }

    public async Task<Guid> GetStructureIdAsync(Guid ComponentId)
    {
        var query = await Repository.WithDetailsAsync(rec => rec.AssociatedStructureElement);
        var structureId = await AsyncExecuter.FirstOrDefaultAsync(query
            .Where(rec => rec.Id == ComponentId)
            .Select(rec => rec.AssociatedStructureElement.AssociatedStructureId));
        return structureId;
    }

    public async Task IncreaseOrderAsync(Guid ComponentId)
    {
        var(subComponents, componentDetails) = await GetAllSiblingComponentsAsync(ComponentId);
        Component.IncreaseComponentOrder(subComponents, componentDetails);
    }
    public async Task DecreaseOrderAsync(Guid ComponentId)
    {
        var (subComponents, componentDetails) = await GetAllSiblingComponentsAsync(ComponentId);
        Component.DecreaseComponentOrder(subComponents, componentDetails);
    }
    private async Task<(List<Component> subComponents, KeyValuePair<Guid?, int> componentDetails)> GetAllSiblingComponentsAsync(Guid componentId)
    {
        var query = await Repository.GetQueryableAsync();
        var componentDetails = await AsyncExecuter
            .FirstOrDefaultAsync(query.Where(rec => rec.Id == componentId).Select(rec =>
            new KeyValuePair<Guid?, int>(rec.ParentComponentId, rec.AssociatedStructureElement.ElementOrder)));

        if (componentDetails.Key == null)
        {
            throw new UserFriendlyException("Cannot Move root components");
        }
        var subComponentsQuery = await Repository.WithDetailsAsync(rec => rec.AssociatedStructureElement);
        var subComponents = await AsyncExecuter.ToListAsync(subComponentsQuery.Where(rec =>
            rec.ParentComponentId == componentDetails.Key));
        return (subComponents, componentDetails);
    }

    protected async override Task<Component> MapToEntityAsync(CreateUpdateComponentDto createInput)
    {
        var query = await Repository.WithDetailsAsync(rec => rec.SubComponents, 
            rec => rec.AssociatedStructureElement.AssociatedStructure);
        var parentComponent = await AsyncExecuter
            .FirstOrDefaultAsync(query.Where(rec => rec.Id == createInput.ParentComponentId)) 
            ?? throw new BusinessException($"Component Not Found:{createInput.ParentComponentId}");
        var newSubComponentName = await _textElementRepository.GetAsync(createInput.NameId)!;
        var subComponent = parentComponent.AddSubComponent(newSubComponentName);
        return subComponent;
    }
    protected override async Task<Component> GetEntityByIdAsync(Guid id)
    {
        var query = await Repository.WithDetailsAsync(rec => rec.Descriptors, rec => rec.SubComponents, rec => rec.ConstraintDescriptors);
        return await AsyncExecuter.FirstAsync(query.Where(rec => rec.Id == id));
    }
    public async Task AddDescriptorAsync(Guid ComponentId, Guid DescriptorNameId)
    {
        var query = await Repository.WithDetailsAsync(rec => rec.Descriptors);
        var component = await AsyncExecuter.FirstOrDefaultAsync(query.Where(rec => rec.Id == ComponentId))!
            ?? throw new UserFriendlyException($"Component with id:{ComponentId} not found");
        var newDescriptorName = await _textElementRepository.GetAsync(DescriptorNameId)!;
        if (!newDescriptorName.IsDescriptor)
        {
            throw new UserFriendlyException("Text selected is not meant for descriptor");
        }
        component.AddDescriptor(newDescriptorName);
    }
    public async Task RemoveDescriptorAsync(Guid ComponentId, Guid DescriptorId)
    {
        var query = await Repository.WithDetailsAsync(rec => rec.Descriptors.Where(d => d.Id == DescriptorId));
        var component = await AsyncExecuter.FirstOrDefaultAsync(query.Where(rec => rec.Id == ComponentId))!
            ?? throw new UserFriendlyException($"Component with id:{ComponentId} not found");
        component.Descriptors.RemoveAll(rec => rec.Id == DescriptorId);
    }

    public async Task<List<IdNameDto<Guid>>> GetAllDescriptorOptionsAsync(Guid DescriptorId)
    {
        var query = await _optionRepository.WithDetailsAsync(rec => rec.AssociatedDescriptor);
        var options = await AsyncExecuter.ToListAsync(query.Where(rec => rec.AssociatedDescriptor.Id == DescriptorId));
        return ObjectMapper.Map<List<DescriptorOption>, List<IdNameDto<Guid>>>(options);
    }
    public async Task AddDescriptorOptionsAsync(Guid DescriptorId, Guid OptionsNameId)
    {
        var query = await _descriptorRepository.GetQueryableAsync();
        var descriptor = await AsyncExecuter.FirstOrDefaultAsync(query.Where(d => d.Id == DescriptorId))!
                   ?? throw new UserFriendlyException($"descriptor with id:{DescriptorId} not found");
        var DescriptorOptionName = await _textElementRepository.GetAsync(OptionsNameId)!;
        if (!DescriptorOptionName.IsOption)
        {
            throw new UserFriendlyException("Text selected is not meant for options");
        }
        descriptor.AddDescriptorOption(DescriptorOptionName);
    }
    public async Task RemoveDescriptorOptionAsync(Guid OptionId)
    {
        await _optionRepository.DeleteAsync(OptionId);
    }

    protected async Task<Structure> GetStructureWithHierarchyDetailsAsync(Guid id)
    {
        var elementsQuery = await _structureElementRepository.WithDetailsAsync(rec => rec.AssociatedStructure, rec => rec.SelectedComponent.SubComponents);
        var elements = await AsyncExecuter.ToListAsync(elementsQuery.Where(rec => rec.AssociatedStructureId == id).OrderBy(e => e.ElementOrder)) ??
            throw new EntityNotFoundException(typeof(Structure), id);
        var structure = elements.Select(rec => rec.AssociatedStructure).FirstOrDefault();
        return structure!;
    }
    public async Task<List<IdNameDto<Guid>>> GetAllConstraintComponentsAsync(Guid ComponentId, string? ComponentNameFilter)
    {
        var componentQuery = await Repository.WithDetailsAsync(rec => rec.AssociatedStructureElement);
        var structureElement = await AsyncExecuter.FirstOrDefaultAsync(componentQuery.Where(rec => rec.Id == ComponentId)
            .Select(rec => rec.AssociatedStructureElement))!
            ?? throw new EntityNotFoundException($"Component with id:{ComponentId} not found");
        var structure = await GetStructureWithHierarchyDetailsAsync(structureElement.AssociatedStructureId);
        List<Component> allowedComponents = structure.GetComponentsBeforeElement(structureElement.Id);
        var results = ObjectMapper.Map<List<Component>, List<IdNameDto<Guid>>>(allowedComponents);
        return results.WhereIf(!ComponentNameFilter.IsNullOrEmpty(), rec => rec.Name.ToLower().Contains(ComponentNameFilter!.ToLower())).ToList(); 
    }
    public async Task AddConstraintDescriptorAsync(Guid ComponentId, Guid DescriptorId)
    {
        var query = await _descriptorRepository.WithDetailsAsync(rec => rec.AssociatedComponent.AssociatedStructureElement);
        var descriptor = await AsyncExecuter.FirstOrDefaultAsync(query.Where(d => d.Id == DescriptorId))!
                   ?? throw new EntityNotFoundException($"descriptor with id:{DescriptorId} not found");
        var componentQuery = await Repository.WithDetailsAsync(rec => rec.Descriptors, rec => rec.AssociatedStructureElement);
        var component = await AsyncExecuter.FirstOrDefaultAsync(componentQuery.Where(rec => rec.Id == ComponentId))!
            ?? throw new EntityNotFoundException($"Component with id:{ComponentId} not found");
        component.AddConstraintDescriptor(descriptor);
    }
    public async Task RemoveConstraintDescriptorAsync(Guid ComponentId, Guid DescriptorId)
    {
        var query = await _descriptorRepository.GetQueryableAsync();
        if(!(await AsyncExecuter.AnyAsync(query.Where(d => d.Id == DescriptorId))))
            throw new EntityNotFoundException($"descriptor with id:{DescriptorId} not found");
        var componentQuery = await Repository.WithDetailsAsync(rec => rec.ConstraintDescriptors);
        var component = await AsyncExecuter.FirstOrDefaultAsync(componentQuery.Where(rec => rec.Id == ComponentId))!
            ?? throw new EntityNotFoundException($"Component with id:{ComponentId} not found");
        if (component.ConstraintDescriptors.RemoveAll(rec => rec.Id == DescriptorId) == 0)
            throw new UserFriendlyException($"Descriptor constraint not available for component");
    }
}

public interface IComponentAppService : ICrudAppService<ComponentDetailsDto, ComponentDto, Guid, ComponentDto, CreateUpdateComponentDto, CreateUpdateComponentDto>
{
    Task<Guid> GetStructureIdAsync(Guid ComponentId);
    Task IncreaseOrderAsync(Guid ComponentId);
    Task DecreaseOrderAsync(Guid ComponentId);
    
    Task AddDescriptorAsync(Guid ComponentId, Guid DescriptorNameId);
    Task RemoveDescriptorAsync(Guid ComponentId, Guid DescriptorId);
    
    Task<List<IdNameDto<Guid>>> GetAllDescriptorOptionsAsync(Guid DescriptorId);
    Task AddDescriptorOptionsAsync(Guid DescriptorId, Guid OptionsNameId);
    Task RemoveDescriptorOptionAsync(Guid OptionId);

    Task<List<IdNameDto<Guid>>> GetAllConstraintComponentsAsync(Guid ComponentId, string? ComponentNameFilter);
    Task AddConstraintDescriptorAsync(Guid ComponentId, Guid DescriptorId);
    Task RemoveConstraintDescriptorAsync(Guid ComponentId, Guid DescriptorId);
}

[AutoMap(typeof(Component))]
public class ComponentDto : IdNameDto<Guid> {
    public Guid NameId { get; set; }
    public Guid? ParentComponentId { get; set; }
}
public class CreateUpdateComponentDto : EntityDto<Guid>
{
    public Guid NameId { get; set; }
    public Guid? ParentComponentId { get; set; }
}

[AutoMap(typeof(Component))]
public class ComponentDetailsDto : IdNameDto<Guid>
{
    public List<IdNameDto<Guid>> Descriptors { get; set; } = new();
    public List<IdNameDto<Guid>> SubComponents { get; set; } = new();
    public List<IdNameDto<Guid>> ConstraintDescriptors { get; set; } = new();
}

[AutoMap(typeof(ComponentDescriptor))]
public class DescriptorOptionsDto : IdNameDto<Guid>
{
    public List<IdNameDto<Guid>> DescriptorOptions { get; set; } = new();
}
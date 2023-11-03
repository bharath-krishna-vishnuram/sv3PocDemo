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
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class ComponentAppService : CrudAppService<Component, ComponentDetailsDto, ComponentDto, Guid, ComponentDto, CreateUpdateComponentDto, CreateUpdateComponentDto>,
    IComponentAppService
{
    readonly IReadOnlyRepository<TextElement, Guid> _textElementRepository;
    //readonly IReadOnlyRepository<ComponentDescriptor, Guid> _descriptorRepository;
    readonly IReadOnlyRepository<DescriptorOption, Guid> _optionRepository;
    public ComponentAppService(IRepository<Component, Guid> repository,
        IReadOnlyRepository<TextElement, Guid> textElementRepository,
        IReadOnlyRepository<DescriptorOption, Guid> optionRepository
        ) : base(repository)
    {
        _textElementRepository = textElementRepository;
        _optionRepository = optionRepository;
    }
    //public async Task<List<DescriptorOptionsDto>> GetAllDescriptorDetailsAsync(Guid ComponentId)
    //{
    //    var query = await _descriptorRepository.WithDetailsAsync(rec => rec.DescriptorOptions, rec => rec.AssociatedComponent);
    //    var descriptors = await AsyncExecuter.ToListAsync(query.Where(rec => rec.AssociatedComponent.Id == ComponentId));
    //    return ObjectMapper.Map<List<ComponentDescriptor>, List<DescriptorOptionsDto>>(descriptors);
    //}
    public async Task<List<IdNameDto<Guid>>> GetAllDescriptorOptionsAsync(Guid DescriptorId)
    {
        var query = await _optionRepository.WithDetailsAsync(rec => rec.AssociatedDescriptor);
        var options = await AsyncExecuter.ToListAsync(query.Where(rec => rec.AssociatedDescriptor.Id == DescriptorId));
        return ObjectMapper.Map<List<DescriptorOption>, List<IdNameDto<Guid>>>(options);
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
        var query = await Repository.WithDetailsAsync(rec => rec.Descriptors, rec => rec.SubComponents);
        return await AsyncExecuter.FirstAsync(query.Where(rec => rec.Id == id));
    }
}

public interface IComponentAppService : ICrudAppService<ComponentDetailsDto, ComponentDto, Guid, ComponentDto, CreateUpdateComponentDto, CreateUpdateComponentDto>
{
    //Task<List<DescriptorOptionsDto>> GetAllDescriptorDetailsAsync(Guid ComponentId);
    Task<List<IdNameDto<Guid>>> GetAllDescriptorOptionsAsync(Guid DescriptorId);
    Task<Guid> GetStructureIdAsync(Guid ComponentId);
    Task IncreaseOrderAsync(Guid ComponentId);
    Task DecreaseOrderAsync(Guid ComponentId);
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
}

[AutoMap(typeof(ComponentDescriptor))]
public class DescriptorOptionsDto : IdNameDto<Guid>
{
    public List<IdNameDto<Guid>> DescriptorOptions { get; set; } = new();
}
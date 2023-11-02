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
    public ComponentAppService(IRepository<Component, Guid> repository, 
        IReadOnlyRepository<TextElement, Guid> textElementRepository) : base(repository)
    {
        _textElementRepository = textElementRepository;
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

[AutoMap(typeof(Component))]
public class DescriptorOptionsDto : IdNameDto<Guid>
{
    public List<IdNameDto<Guid>> DescriptorOptions { get; set; } = new();
}
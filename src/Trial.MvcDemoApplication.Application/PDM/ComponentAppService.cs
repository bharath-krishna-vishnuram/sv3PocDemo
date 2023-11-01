using AutoMapper;
using PDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.Common.Dto;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class ComponentAppService : CrudAppService<Component, ComponentDetailsDto, ComponentDto, Guid, ComponentDto, ComponentDto, ComponentDto>,
    IComponentAppService
{
    public ComponentAppService(IRepository<Component, Guid> repository) : base(repository) { }
    protected override async Task<Component> GetEntityByIdAsync(Guid id)
    {
        var query = await Repository.WithDetailsAsync(rec => rec.Descriptors, rec => rec.SubComponents);
        return await AsyncExecuter.FirstAsync(query.Where(rec => rec.Id == id));
    }
}

public interface IComponentAppService : ICrudAppService<ComponentDetailsDto, ComponentDto, Guid, ComponentDto, ComponentDto, ComponentDto>
{
}

[AutoMap(typeof(Component))]
public class ComponentDto : IdNameDto<Guid> { }

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
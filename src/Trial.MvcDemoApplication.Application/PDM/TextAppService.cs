using PDM.Models;
using System;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class TextAppService : CrudAppService<TextElement, TextElementDto, Guid, TextElementListRequestDto, CreateTextElementDto>, ITextAppService
{
    public TextAppService(IRepository<TextElement, Guid> repository) : base(repository)
    {
    }
    protected override IQueryable<TextElement> ApplyPaging(IQueryable<TextElement> query, TextElementListRequestDto input)
    {
        query = query
            .WhereIf(input.IsProduct, rec => rec.IsProduct)
            .WhereIf(input.IsSubProduct, rec => rec.IsSubProduct)
            .WhereIf(input.IsStructure, rec => rec.IsStructure)
            .WhereIf(input.IsComponent, rec => rec.IsComponent)
            .WhereIf(input.IsDescriptor, rec => rec.IsDescriptor)
            .WhereIf(input.IsOption, rec => rec.IsOption)
            .WhereIf(!input.Filter.IsNullOrEmpty(), rec => rec.UniqueTextId.Contains(input.Filter!)
                    || rec.TextName.Contains(input.Filter!));
        return base.ApplyPaging(query, input);
    }
}

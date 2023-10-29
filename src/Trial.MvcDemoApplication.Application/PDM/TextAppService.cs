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
        if (!input.Filter.IsNullOrEmpty())
        {
            query = query
                .Where(rec => rec.UniqueTextId.Contains(input.Filter!)
                        || rec.TextName.Contains(input.Filter!));
        }
        return base.ApplyPaging(query, input);
    }
}

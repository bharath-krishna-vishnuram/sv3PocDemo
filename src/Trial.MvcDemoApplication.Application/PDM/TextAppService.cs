using PDM.Models;
using System;
using System.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class TextAppService : CrudAppService<TextElement, TextElementDto, Guid, PagedResultRequestDto, CreateTextElementDto>, ITextAppService
{
    public TextAppService(IRepository<TextElement, Guid> repository) : base(repository)
    {
    }
    protected override IQueryable<TextElement> ApplyPaging(IQueryable<TextElement> query, PagedResultRequestDto input)
    {
        return base.ApplyPaging(query.OrderByDescending(rec => rec.Id), input);
    }
}

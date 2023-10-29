using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Trial.MvcDemoApplication.PDM;

public interface ITextAppService: ICrudAppService<TextElementDto, Guid, TextElementListRequestDto, CreateTextElementDto>
{
}

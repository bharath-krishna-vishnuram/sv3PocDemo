using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Trial.MvcDemoApplication.Common.Dto;

public class PagedAndFilteredResultRequestDto : PagedResultRequestDto
{
    public string? Filter { get; set; }
}

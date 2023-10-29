using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Trial.MvcDemoApplication.Common.Dto;

public class IdNameDto<T>: EntityDto<T>
{
    public string Name { get; set; } = string.Empty;
}

using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.TextManager;

public class UpdateTextModel : MvcDemoApplicationPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateTextElementDto Text { get; set; } = new();

    private readonly ITextAppService _textAppService;

    public UpdateTextModel(ITextAppService textAppService)
    {
        _textAppService = textAppService;
    }

    public async Task OnGetAsync()
    {
        var elementDetails = await _textAppService.GetAsync(Id);
        Text = ObjectMapper.Map<TextElementDto, CreateTextElementDto>(elementDetails);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _textAppService.UpdateAsync(Id, Text);
        return NoContent();
    }
}

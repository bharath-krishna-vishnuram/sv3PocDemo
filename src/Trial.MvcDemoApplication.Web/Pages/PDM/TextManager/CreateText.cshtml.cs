using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Trial.MvcDemoApplication.PDM;

namespace Trial.MvcDemoApplication.Web.Pages.PDM.TextManager;

public class CreateTextModel : MvcDemoApplicationPageModel
{
    [BindProperty]
    public CreateTextElementDto Text { get; set; } = new();

    private readonly ITextAppService _textAppService;

    public CreateTextModel(ITextAppService textAppService)
    {
        _textAppService = textAppService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var text = await _textAppService.CreateAsync(Text);
        return new OkObjectResult(text);
    }
}

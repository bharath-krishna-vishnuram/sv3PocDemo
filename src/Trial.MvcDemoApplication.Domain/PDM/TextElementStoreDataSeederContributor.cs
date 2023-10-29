using PDM.Models;
using Polly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Trial.MvcDemoApplication.PDM;

public class TextElementStoreDataSeederContributor : IDataSeedContributor, ITransientDependency

{
    private readonly IRepository<Structure, Guid> _structureRepository;
    private readonly IRepository<TextElement, Guid> _textRepository;

    public TextElementStoreDataSeederContributor(IRepository<TextElement, Guid> textRepository, IRepository<Structure, Guid> structureRepository)
    {
        _textRepository = textRepository;
        _structureRepository = structureRepository;
    }
    public async Task SeedAsync(DataSeedContext context)
    {
        var allElements = TextElement.GetAllTextElements();
        if (await _textRepository.GetCountAsync() <= 0)
        {
            await _textRepository.InsertManyAsync(allElements, autoSave: true);
        }
        var allElementTexts = allElements.ToDictionary(rec => rec.UniqueTextId, rec => rec);

        if (await _structureRepository.GetCountAsync() <= 0)
        {
            var assmembly = Structure.GetSampleData(allElementTexts);
            await _structureRepository.InsertAsync(assmembly, autoSave: true);
        }
    }
}

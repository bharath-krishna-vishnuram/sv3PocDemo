using PDM.Models;
using Polly;
using System;
using System.Collections.Generic;
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
    private readonly IRepository<StructureElement, Guid> _structureElementRepository;

    public TextElementStoreDataSeederContributor(IRepository<TextElement, Guid> textRepository, IRepository<Structure, Guid> structureRepository, 
        IRepository<StructureElement, Guid> structureElementRepository)
    {
        _textRepository = textRepository;
        _structureRepository = structureRepository;
        _structureElementRepository = structureElementRepository;
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

        if (!await _structureElementRepository.AsyncExecuter.AnyAsync(await _structureElementRepository.WithDetailsAsync(rec => rec.SelectedComponent.ConstraintDescriptors), rec => rec.SelectedComponent.ConstraintDescriptors.Any()))
        {
            var componentList = new List<string> { "valveBodyText" , "bonnetText", "seatRingText", "plugHeadText" };
            var descriptorList = new List<string> { "bodyMaterialDescriptorText", "bodySizeDescriptorText", "seatRingMaterialDescriptorText" };
            var sampleElements = (await _structureElementRepository.WithDetailsAsync(rec => rec.SelectedComponent.Descriptors.Where(d => descriptorList.Contains(d.Name.UniqueTextId))))
                .Where(rec => componentList.Contains(rec.SelectedComponent.Name.UniqueTextId)
                && rec.AssociatedStructure.Name.UniqueTextId == "bodySubAssyText").ToDictionary(rec => rec.SelectedComponent.Name.UniqueTextId, rec => rec);

            var sampleDescriptors = sampleElements.SelectMany(rec => rec.Value.SelectedComponent.Descriptors).ToDictionary(rec => rec.Name.UniqueTextId, rec => rec);

            sampleElements["bonnetText"].SelectedComponent.AddConstraintDescriptor(sampleDescriptors["bodyMaterialDescriptorText"]);
            sampleElements["seatRingText"].SelectedComponent.AddConstraintDescriptor(sampleDescriptors["bodySizeDescriptorText"]);
            sampleElements["plugHeadText"].SelectedComponent.AddConstraintDescriptor(sampleDescriptors["seatRingMaterialDescriptorText"]);

            await _structureElementRepository.UpdateManyAsync(sampleElements.Values, autoSave: true);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities.Auditing;

namespace PDM.Models;

public class Structure : FullAuditedEntity<Guid>
{
    public override string ToString() => Name.ToString();
    public Structure()
    {
        Id = Guid.NewGuid();
    }
    public TextElement Name { get; protected set; } = null!;
    public string Description { get; set; } = string.Empty;
    public StructureType Type { get; protected set; }
    public Component RootComponent { get; protected set; } = null!;

    public List<StructureElement> StructureElements { get; protected set; } = new();
    protected Structure(TextElement name, StructureType type) : base()
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        RootComponent = Component.AddRootComponent(name, this);
    }

    public static Structure CreateStructure(TextElement name, string description, StructureType type)
    {
        return new Structure(name, type)
        {
            Description = description,
        };
    }
    public static Structure GetSampleData(Dictionary<string, TextElement> allElementTexts)
    {
        var assembly = new Structure(allElementTexts["bodySubAssyText"], StructureType.SubAssembly);

        var root = assembly.RootComponent;

        var valveBodyComponent = root.AddSubComponent(allElementTexts["valveBodyText"]);
        #region valve Body Descriptors

        var bodyTypeDescriptor = valveBodyComponent.AddDescriptor(allElementTexts["bodyTypeDescriptorText"]);
        bodyTypeDescriptor.AddDescriptorOption(allElementTexts["globeOptionText"]);
        bodyTypeDescriptor.AddDescriptorOption(allElementTexts["ansiAngleOptionText"]);

        var bodySizeDescriptor = valveBodyComponent.AddDescriptor(allElementTexts["bodySizeDescriptorText"]);
        bodySizeDescriptor.AddDescriptorOption(allElementTexts["size1OptionText"]);
        bodySizeDescriptor.AddDescriptorOption(allElementTexts["size2OptionText"]);

        var bodyRatingDescriptor = valveBodyComponent.AddDescriptor(allElementTexts["bodyRatingDescriptorText"]);
        bodyRatingDescriptor.AddDescriptorOption(allElementTexts["ansi150OptionText"]);
        bodyRatingDescriptor.AddDescriptorOption(allElementTexts["ansi300OptionText"]);

        var bodyMaterialDescriptor = valveBodyComponent.AddDescriptor(allElementTexts["bodyMaterialDescriptorText"]);
        bodyMaterialDescriptor.AddDescriptorOption(allElementTexts["wcbOptionText"]);
        bodyMaterialDescriptor.AddDescriptorOption(allElementTexts["cf8mOptionText"]);

        #endregion
        var seatRingComponent = valveBodyComponent.AddSubComponent(allElementTexts["seatRingText"]);
        #region seat ring Descriptors

        var seatRingTypeDescriptor = seatRingComponent.AddDescriptor(allElementTexts["seatRingTypeDescriptorText"]);
        seatRingTypeDescriptor.AddDescriptorOption(allElementTexts["threadedOptionText"]);

        var portDiameterDescriptor = seatRingComponent.AddDescriptor(allElementTexts["portDiameterDescriptorText"]);
        portDiameterDescriptor.AddDescriptorOption(allElementTexts["diameter075OptionText"]);
        portDiameterDescriptor.AddDescriptorOption(allElementTexts["diameter1OptionText"]);
        portDiameterDescriptor.AddDescriptorOption(allElementTexts["diameter15OptionText"]);
        portDiameterDescriptor.AddDescriptorOption(allElementTexts["diameter2OptionText"]);

        var ratedCVDescriptor = seatRingComponent.AddDescriptor(allElementTexts["ratedCVDescriptorText"]);
        ratedCVDescriptor.AddDescriptorOption(allElementTexts["ratedCV6OptionText"]);
        ratedCVDescriptor.AddDescriptorOption(allElementTexts["ratedCV11OptionText"]);
        ratedCVDescriptor.AddDescriptorOption(allElementTexts["ratedCV25OptionText"]);
        ratedCVDescriptor.AddDescriptorOption(allElementTexts["ratedCV44OptionText"]);

        var seatRingMaterialDescriptor = seatRingComponent.AddDescriptor(allElementTexts["seatRingMaterialDescriptorText"]);
        seatRingMaterialDescriptor.AddDescriptorOption(allElementTexts["ss316OptionText"]);
        seatRingMaterialDescriptor.AddDescriptorOption(allElementTexts["ss440COptionText"]);
        #endregion

        var plugHeadComponent = seatRingComponent.AddSubComponent(allElementTexts["plugHeadText"]);

        #region plugHead Descriptors
        var valveCHSDescriptor = plugHeadComponent.AddDescriptor(allElementTexts["valveCHSDescriptorText"]);
        valveCHSDescriptor.AddDescriptorOption(allElementTexts["linearOptionText"]);
        valveCHSDescriptor.AddDescriptorOption(allElementTexts["equalPercentOptionText"]);
        valveCHSDescriptor.AddDescriptorOption(allElementTexts["qoOptionText"]);

        var plugHeadMaterialDescriptor = plugHeadComponent.AddDescriptor(allElementTexts["plugHeadMaterialDescriptorText"]);
        plugHeadMaterialDescriptor.AddDescriptorOption(allElementTexts["ss316OptionText"]);
        plugHeadMaterialDescriptor.AddDescriptorOption(allElementTexts["ss440COptionText"]);
        #endregion

        var plugStemComponent = seatRingComponent.AddSubComponent(allElementTexts["plugStemText"]);
        var connectingPinComponent = seatRingComponent.AddSubComponent(allElementTexts["connectingPinText"]);
        var seatRingRetainerComponent = valveBodyComponent.AddSubComponent(allElementTexts["seatRingRetainerText"]);
        var bonnetComponent = root.AddSubComponent(allElementTexts["bonnetText"]);

        #region bonnet Descriptors
        var bonnetTypeDescriptor = bonnetComponent.AddDescriptor(allElementTexts["bonnetTypeDescriptorText"]);
        bonnetTypeDescriptor.AddDescriptorOption(allElementTexts["plainOptionText"]);
        bonnetTypeDescriptor.AddDescriptorOption(allElementTexts["finnedOptionText"]);
        bonnetTypeDescriptor.AddDescriptorOption(allElementTexts["extendedOptionText"]);

        var bonnetMaterialDescriptor = bonnetComponent.AddDescriptor(allElementTexts["bonnetMaterialDescriptorText"]);
        bonnetMaterialDescriptor.AddDescriptorOption(allElementTexts["wcbOptionText"]);
        bonnetMaterialDescriptor.AddDescriptorOption(allElementTexts["cf8mOptionText"]);
        #endregion

        var packingComponent = bonnetComponent.AddSubComponent(allElementTexts["packingText"]);
        var packingFlangeComponent = bonnetComponent.AddSubComponent(allElementTexts["packingFlangeText"]);
        var packingFollowerComponent = bonnetComponent.AddSubComponent(allElementTexts["packingFollowerText"]);
        var packingFlangeBoltingComponent = bonnetComponent.AddSubComponent(allElementTexts["packingFlangeBoltingText"]);
        var bodyBonnetGasketComponent = root.AddSubComponent(allElementTexts["bodyBonnetGasketText"]);
        var bodyBonnetBoltsComponent = root.AddSubComponent(allElementTexts["bodyBonnetBoltsText"]);
        var bodyBonnetNutsComponent = root.AddSubComponent(allElementTexts["bodyBonnetNutsText"]);
        var tagPlateComponent = root.AddSubComponent(allElementTexts["tagPlateText"]);

        return assembly;
    }

    public void UpdateName(TextElement newStructureName)
    {
        Name = newStructureName;
    }
}

public class StructureElement : Volo.Abp.Domain.Entities.Entity<Guid>
{
    private StructureElement() => Id = Guid.NewGuid();
    public StructureElement(Structure associatedStructure) :this()
    {
        AssociatedStructure = associatedStructure; 
    }

    public StructureElement(Component rec) : base()
    {
        SelectedComponent = rec;
    }

    public Structure AssociatedStructure { get; set; } = null!;
    public Guid AssociatedStructureId { get; set; }
    public Component SelectedComponent { get; set; } = null!;
    public Guid SelectedComponentId { get; set; }
    public int ElementOrder { get; set; }

    internal static List<StructureElement> AddRootElement(Component assembly)
    {
        return new() { new StructureElement(assembly) };
    }
}


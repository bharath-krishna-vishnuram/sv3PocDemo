using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace PDM.Models;

public class TextElement : FullAuditedAggregateRoot<Guid>
{
    protected TextElement()
    {
    }
    private TextElement(string name, string uniqueString) : this()
    {
        TextName = name;
        UniqueTextId = uniqueString;
    }
    public override string ToString() => TextName;

    [Required]
    public string TextName { get; set; } = null!;
    [Required]
    public string UniqueTextId { get; set; } = null!;
    public bool IsProduct { get; set; }
    public bool IsSubProduct { get; set; }
    public bool IsStructure { get; set; }
    public bool IsComponent { get; set; }
    public bool IsDescriptor { get; set; }
    public bool IsOption { get; set; }
    //public bool ReplaceID { get; set; } // This can be changed back to string or another type if 'Y'/'N' was not the intended data.
    public string? German { get; set; }
    public string? French { get; set; }
    public string? Spanish { get; set; }
    public string? Russian { get; set; }
    public string? Chinese { get; set; }
    public string? Remarks { get; set; }
    public List<Structure> Structures { get; set; } = new();
    public List<Component> Components { get; set; } = new();
    public List<ComponentDescriptor> ComponentDescriptors { get; set; } = new();
    public List<DescriptorOption> DescriptorOptions { get; set; } = new();

    public static List<TextElement> GetAllTextElements()
    {
        return new()
        {
            new TextElement("Body Sub Assy", "bodySubAssyText") { IsComponent = true },
            #region Valve Body
            new TextElement("Valve Body", "valveBodyText") { IsComponent = true },
            new TextElement("Body Type", "bodyTypeDescriptorText") { IsDescriptor = true },
            new TextElement("Globe", "globeOptionText") { IsOption = true },
            new TextElement("ANSI Angle", "ansiAngleOptionText") { IsOption = true },
            new TextElement("Body Size", "bodySizeDescriptorText") { IsDescriptor = true },
            new TextElement("1", "size1OptionText") { IsOption = true },
            new TextElement("2", "size2OptionText") { IsOption = true },
            new TextElement("Body Rating", "bodyRatingDescriptorText") { IsDescriptor = true },
            new TextElement("ANSI 150", "ansi150OptionText") { IsOption = true },
            new TextElement("ANSI 300", "ansi300OptionText") { IsOption = true },
            new TextElement("Body Material", "bodyMaterialDescriptorText") { IsDescriptor = true },
            new TextElement("WCB", "wcbOptionText") { IsOption = true },
            new TextElement("CF8M", "cf8mOptionText") { IsOption = true },
            #endregion
                #region SeatRing
                new TextElement("Seat Ring", "seatRingText") { IsComponent = true },
                new TextElement("Seat Ring Type", "seatRingTypeDescriptorText") { IsDescriptor = true },
                new TextElement("Threaded", "threadedOptionText") { IsOption = true },
                new TextElement("Port Diameter", "portDiameterDescriptorText") { IsDescriptor = true },
                new TextElement("0.75", "diameter075OptionText") { IsOption = true },
                new TextElement("1", "diameter1OptionText") { IsOption = true },
                new TextElement("1.5", "diameter15OptionText") { IsOption = true },
                new TextElement("2", "diameter2OptionText") { IsOption = true },
                new TextElement("Rated CV", "ratedCVDescriptorText") { IsDescriptor = true },
                new TextElement("6", "ratedCV6OptionText") { IsOption = true },
                new TextElement("11", "ratedCV11OptionText") { IsOption = true },
                new TextElement("25", "ratedCV25OptionText") { IsOption = true },
                new TextElement("44", "ratedCV44OptionText") { IsOption = true },
                new TextElement("Seat Ring Material", "seatRingMaterialDescriptorText") { IsDescriptor = true },
                new TextElement("SS 316", "ss316OptionText") { IsOption = true },
                new TextElement("SS 440C", "ss440COptionText") { IsOption = true },
                #endregion
                    #region plugHead
                    new TextElement("Plug Head", "plugHeadText") { IsComponent = true },
                    new TextElement("Valve CHS", "valveCHSDescriptorText") { IsDescriptor = true },
                    new TextElement("Linear", "linearOptionText") { IsOption = true },
                    new TextElement("Equal %", "equalPercentOptionText") { IsOption = true },
                    new TextElement("QO", "qoOptionText") { IsOption = true },
                    new TextElement("Plug Head Material", "plugHeadMaterialDescriptorText") { IsDescriptor = true },
                    #endregion
                    new TextElement("Plug Stem", "plugStemText") { IsComponent = true },
                    new TextElement("Connecting Pin", "connectingPinText") { IsComponent = true },
                new TextElement("Seat Ring Retainer", "seatRingRetainerText") { IsComponent = true },
            #region bonnet
            new TextElement("Bonnet", "bonnetText") { IsComponent = true },
            new TextElement("Bonnet Type", "bonnetTypeDescriptorText") { IsDescriptor = true },
            new TextElement("Bonnet Material", "bonnetMaterialDescriptorText") { IsDescriptor = true },
            new TextElement("Plain", "plainOptionText") { IsOption = true },
            new TextElement("Finned", "finnedOptionText") { IsOption = true },
            new TextElement("Extended", "extendedOptionText") { IsOption = true },
            new TextElement("Packing", "packingText") { IsComponent = true },
            new TextElement("Packing Flange", "packingFlangeText") { IsComponent = true },
            new TextElement("Packing Follower", "packingFollowerText") { IsComponent = true },
            new TextElement("Packing Flange Bolting", "packingFlangeBoltingText") { IsComponent = true },
            #endregion
            new TextElement("Body Bonnet Gasket", "bodyBonnetGasketText") { IsComponent = true },
            new TextElement("Body Bonnet Bolts", "bodyBonnetBoltsText") { IsComponent = true },
            new TextElement("Body Bonnet Nuts", "bodyBonnetNutsText") { IsComponent = true },
            new TextElement("Tag Plate", "tagPlateText") { IsComponent = true }

        };

    }

}

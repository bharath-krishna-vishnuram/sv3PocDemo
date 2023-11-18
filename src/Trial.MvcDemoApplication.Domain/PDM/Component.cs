using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace PDM.Models;

public class Component : FullAuditedEntity<Guid>
{
    public override string ToString() => Name.ToString();

    public TextElement Name { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public Component? ParentComponent { get; set; }
    public Guid? ParentComponentId { get; set; }
    public bool IsSubComponent => ParentComponentId.HasValue;
    public StructureElement AssociatedStructureElement { get; protected set; } //= null()!;
    //public Guid AssociatedStructureElementId { get; set; } 

    public List<ComponentDescriptor> Descriptors { get; set; } = new();
    public List<ComponentDescriptor> ConstraintDescriptors { get; set; } = new();
    public List<Component> SubComponents { get; set; } = new();
    public Component() => Id = Guid.NewGuid();
    private Component(TextElement _name) : this()
    {
        Name = _name;
    }

    public ComponentDescriptor AddDescriptor(TextElement descriptorText)
    {
        if (Descriptors.Any(c => c.Name.TextName == descriptorText.TextName))
            throw new UserFriendlyException($"Subcomponent {descriptorText} already part of component {Name}");
        ComponentDescriptor descriptor = new(descriptorText);
        Descriptors.Add(descriptor);
        return descriptor;
    }

    public ComponentDescriptor AddConstraintDescriptor(ComponentDescriptor constraint)
    {
        if (ConstraintDescriptors.Any(c => c.Name.TextName == constraint.Name.TextName))
            throw new UserFriendlyException($"Constraint {constraint.Name} already part of component {Name}");

        if (constraint.AssociatedComponent.Id == Id)
            throw new UserFriendlyException($"Constraint Descriptor [{constraint.Name}] already part of component descriptors");

        if (constraint.AssociatedComponent.AssociatedStructureElement.AssociatedStructureId != 
            AssociatedStructureElement.AssociatedStructureId)
            throw new UserFriendlyException($"Constraint Descriptor [{constraint.Name}] not part of component structure");

        ConstraintDescriptors.Add(constraint);
        return constraint;
    }

    public Component AddSubComponent(TextElement subComponentName)
    {
        if (SubComponents.Any(c => c.Name.TextName == subComponentName.TextName))
            throw new Volo.Abp.BusinessException($"Subcomponent {subComponentName} already part of component {Name}");
        Component subComponent = new(subComponentName)
        {
            AssociatedStructureElement = new StructureElement(AssociatedStructureElement.AssociatedStructure)
            {
                ElementOrder = SubComponents.Count
            }
        };
        SubComponents.Add(subComponent);
        return subComponent;
    }

    internal static Component AddRootComponent(TextElement name, Structure structure)
    {
        return new Component(name)
        {
            AssociatedStructureElement = new StructureElement(structure)
        };
    }

    public static void IncreaseComponentOrder(List<Component> subComponents, KeyValuePair<Guid?, int> componentDetails)
    {
        subComponents.RemoveAll(rec => rec.AssociatedStructureElement.ElementOrder < componentDetails.Value);
        var nextSubComponent = subComponents.Where(rec => rec.AssociatedStructureElement.ElementOrder != componentDetails.Value)
                                               .MinBy(rec => rec.AssociatedStructureElement.ElementOrder) ??
                                            throw new UserFriendlyException("Cannot Move last component");
        var requestSubComponent = subComponents.MinBy(rec => rec.AssociatedStructureElement.ElementOrder)!;
        requestSubComponent.AssociatedStructureElement.ElementOrder = nextSubComponent.AssociatedStructureElement.ElementOrder;
        nextSubComponent.AssociatedStructureElement.ElementOrder = componentDetails.Value;
    }
    public static void DecreaseComponentOrder(List<Component> subComponents, KeyValuePair<Guid?, int> componentDetails)
    {
        subComponents.RemoveAll(rec => rec.AssociatedStructureElement.ElementOrder > componentDetails.Value);
        var nextSubComponent = subComponents.Where(rec => rec.AssociatedStructureElement.ElementOrder != componentDetails.Value)
                                               .MaxBy(rec => rec.AssociatedStructureElement.ElementOrder) ??
                                            throw new UserFriendlyException("Cannot Move first component");
        var requestSubComponent = subComponents.MaxBy(rec => rec.AssociatedStructureElement.ElementOrder)!;
        requestSubComponent.AssociatedStructureElement.ElementOrder = nextSubComponent.AssociatedStructureElement.ElementOrder;
        nextSubComponent.AssociatedStructureElement.ElementOrder = componentDetails.Value;
    }
}

public class ComponentDescriptor : FullAuditedEntity<Guid>
{
    public override string ToString() => Name.ToString();
    private ComponentDescriptor() => Id = Guid.NewGuid();
    public ComponentDescriptor(TextElement name) : this() => Name = name;
    public ComponentDescriptor(TextElement name, List<TextElement>? optionValues = null) : this(name)
    {
        optionValues?.ForEach(item => AddDescriptorOption(item));
    }

    public void AddDescriptorOption(TextElement item)
    {
        DescriptorOptions.Add(new(item));
    }

    public TextElement Name { get; set; } = null!;

    public string Description { get; set; } = string.Empty;
    public Component AssociatedComponent { get; set; } = null!;
    public List<DescriptorOption> DescriptorOptions { get; set; } = new();
    public List<Component> ConstraintComponents { get; set; } = new();
}

public class DescriptorOption : FullAuditedEntity<Guid>
{
    public override string ToString() => Name.ToString();
    private DescriptorOption() => Id = Guid.NewGuid();
    public DescriptorOption(TextElement _value) : this()
    {
        Name = _value;
    }
    public TextElement Name { get; set; } = null!;
    public ComponentDescriptor AssociatedDescriptor { get; set; } = null!;
    public List<StructureElement> AssociatedStructureElements { get; set; } = new();
}

//public class ComponentDescriptorConstraint
//{
//    public ComponentDescriptorConstraint() { }
//    public Guid Id { get; set; } = Guid.NewGuid();
//    public Component LeadComponent { get; set; } = null!;
//    public ComponentDescriptor ConstraintDescriptor { get; set; } = null!;
//}

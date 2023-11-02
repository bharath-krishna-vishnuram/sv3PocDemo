namespace Trial.MvcDemoApplication.PDM.Dtos.Structure;

public class StructureHierarchyDto : StructureDto
{
    public override string ToString() => Name;
    public ComponentHierarchyDto RootComponent { get; set; } = new();
}

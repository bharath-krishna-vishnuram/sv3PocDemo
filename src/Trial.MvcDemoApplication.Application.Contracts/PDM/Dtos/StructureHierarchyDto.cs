namespace Trial.MvcDemoApplication.PDM.Dtos;

public class StructureHierarchyDto : StructureDto
{
    public override string ToString() => Name;
    public string Type { get; set; } = string.Empty;
    public ComponentHierarchyDto RootComponent { get; set; } = new();
}

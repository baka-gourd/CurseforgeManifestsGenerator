using System.Text.Json.Serialization;

namespace CurseforgeManifestsGenerator;

using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

public class Manifest
{
    [J("minecraft")] public Minecraft Minecraft { get; set; }
    [J("manifestType")] public string ManifestType { get; set; }
    [J("manifestVersion")] public long ManifestVersion { get; set; }
    [J("name")] public string Name { get; set; }
    [J("version")] public string Version { get; set; }
    [J("author")] public string Author { get; set; }
    [J("files")] public File[] Files { get; set; }
    [J("overrides")] public string Overrides { get; set; }
}
public class File
{
    [J("projectID")] public long ProjectId { get; set; }
    [J("fileID")] public long FileId { get; set; }
    [J("required")] public bool FileRequired { get; set; }
}

public class Minecraft
{
    [J("version")] public string Version { get; set; }
    [J("modLoaders")] public ModLoader[] ModLoaders { get; set; }
}

public class ModLoader
{
    [J("id")] public string Id { get; set; }
    [J("primary")] public bool Primary { get; set; }
}

[JsonSerializable(typeof(Manifest))]
public partial class ManifestContext : JsonSerializerContext
{
}
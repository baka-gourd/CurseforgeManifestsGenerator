using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace CurseforgeManifestsGenerator;
public class Response
{
    [J("data")] public Data Data { get; set; }
}
public class Data
{
    [J("isCacheBuilt")] public bool IsCacheBuilt { get; set; }
    [J("exactMatches")] public ExactMatch[] ExactMatches { get; set; }
    [J("exactFingerprints")] public long[] ExactFingerprints { get; set; }
    [J("partialMatches")] public object[] PartialMatches { get; set; }
    [J("partialMatchFingerprints")] public object PartialMatchFingerprints { get; set; }
    [J("installedFingerprints")] public long[] InstalledFingerprints { get; set; }
    [J("unmatchedFingerprints")] public object UnmatchedFingerprints { get; set; }
}
public partial class ExactMatch
{
    [J("id")] public long Id { get; set; }
    [J("file")] public FileR File { get; set; }
    [J("latestFiles")] public FileR[] LatestFiles { get; set; }
}
public partial class FileR
{
    [J("id")] public long Id { get; set; }
    [J("gameId")] public long GameId { get; set; }
    [J("modId")] public long ModId { get; set; }
    [J("isAvailable")] public bool IsAvailable { get; set; }
    [J("displayName")] public string DisplayName { get; set; }
    [J("fileName")] public string FileName { get; set; }
    [J("releaseType")] public long ReleaseType { get; set; }
    [J("fileStatus")] public long FileStatus { get; set; }
    [J("hashes")] public Hash[] Hashes { get; set; }
    [J("fileDate")] public DateTimeOffset FileDate { get; set; }
    [J("fileLength")] public long FileLength { get; set; }
    [J("downloadCount")] public long DownloadCount { get; set; }
    [J("downloadUrl")] public Uri DownloadUrl { get; set; }
    [J("gameVersions")] public string[] GameVersions { get; set; }
    [J("sortableGameVersions")] public SortableGameVersion[] SortableGameVersions { get; set; }
    [J("dependencies")] public Dependency[] Dependencies { get; set; }
    [J("alternateFileId")] public long AlternateFileId { get; set; }
    [J("isServerPack")] public bool IsServerPack { get; set; }
    [J("fileFingerprint")] public long FileFingerprint { get; set; }
    [J("modules")] public Module[] Modules { get; set; }
}

public partial class Dependency
{
    [J("modId")] public long ModId { get; set; }
    [J("relationType")] public long RelationType { get; set; }
}
public partial class Hash
{
    [J("value")] public string Value { get; set; }
    [J("algo")] public long Algo { get; set; }
}

public partial class Module
{
    [J("name")] public string Name { get; set; }
    [J("fingerprint")] public long Fingerprint { get; set; }
}

public partial class SortableGameVersion
{
    [J("gameVersionName")] public string GameVersionName { get; set; }
    [J("gameVersionPadded")] public string GameVersionPadded { get; set; }
    [J("gameVersion")] public string GameVersion { get; set; }
    [J("gameVersionReleaseDate")] public DateTimeOffset GameVersionReleaseDate { get; set; }
    [J("gameVersionTypeId")] public long GameVersionTypeId { get; set; }
}

[JsonSerializable(typeof(Response))]
public partial class ResponseContext : JsonSerializerContext
{
}
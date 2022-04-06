using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CurseforgeManifestsGenerator;
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.Write("Welcome to use curseforge manifests generator.\nPlease enter your API key: ");

        string? key;

        while (true)
        {
            key = Console.ReadLine();
            if (key is not null)
            {
                break;
            }
        }

        var directory = Directory.GetCurrentDirectory();
        var httpClient = new HttpClient();

        const string reqUrl = "https://api.curseforge.com/v1/fingerprints";

        Console.WriteLine($"Current scan directory: {directory}");
        var files = new List<File>();

        foreach (var file in Directory.GetFiles(directory))
        {
            if (Path.GetExtension(file) is not ".jar")
            {
                continue;
            }
            var fingerPrint = MurmurHash2.HashNormal(await System.IO.File.ReadAllBytesAsync(file));
            Console.WriteLine($"{Path.GetFileName(file)}'s finger print is {fingerPrint}");
            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(reqUrl),
                Content = new StringContent(
                    @"{
            ""fingerprints"": [
            replace
            ]
        }".Replace("replace", fingerPrint.ToString())
                , Encoding.UTF8, "application/json")
            };
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            req.Headers.Add("x-api-key", key);
            var resp = await httpClient.SendAsync(req);
            try
            {
                var json = await JsonSerializer.DeserializeAsync(await resp.Content.ReadAsStreamAsync(), ResponseContext.Default.Response);
                if (json is null)
                {
                    Console.WriteLine("Cannot match this mod in CurseForge.");
                }

                var match =
                    json!.Data.ExactMatches.FirstOrDefault(match => match.File.FileName == Path.GetFileName(file)) ?? json.Data.ExactMatches[0];
                files.Add(new File { FileId = match.File.Id, FileRequired = true, ProjectId = match.Id });
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot match this mod in CurseForge.");
            }
        }

        var manifest = new Manifest
        {
            Author = "CFMG",
            Files = files.ToArray(),
            ManifestType = "minecraftModpack",
            ManifestVersion = 1,
            Minecraft = new Minecraft
            {
                ModLoaders = new ModLoader[] { new ModLoader() { Id = "forge-14.23.5.2855", Primary = true } },
                Version = "1.12.2"
            },
            Name = "CFMG",
            Overrides = "overrides",
            Version = "1.0"
        };

        await System.IO.File.WriteAllTextAsync("./manifest.json", JsonSerializer.Serialize(manifest, new ManifestContext(new JsonSerializerOptions() { WriteIndented = true }).Manifest));
        Console.WriteLine("Done!");
        Console.ReadLine();
    }
}
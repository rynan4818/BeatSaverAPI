using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaverSharp;
using Newtonsoft.Json;

namespace BeatSaverAPI
{
    [JsonObject]
    public class Metadata
    {
        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("automapper")]
        public string Automapper { get; set; }

        [JsonProperty("levelAuthorName")]
        public string LevelAuthorName { get; set; }

        [JsonProperty("songAuthorName")]
        public string SongAuthorName { get; set; }

        [JsonProperty("songName")]
        public string SongName { get; set; }

        [JsonProperty("songSubName")]
        public string SongSubName { get; set; }

        [JsonProperty("bpm")]
        public float Bpm { get; set; }

    }

    [JsonObject]
    public class Beatmap_data
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("uploaded")]
        public string Uploaded { get; set; }

        [JsonProperty("directDownload")]
        public string DirectDownload { get; set; }

        [JsonProperty("downloadURL")]
        public string DownloadURL { get; set; }

        [JsonProperty("coverURL")]
        public string CoverURL { get; set; }

        [JsonProperty("deletedAt")]
        public string DeletedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int timeout = 10;
            if (args.Length >= 2)
            {
                timeout = int.Parse(args[1]);
            }
            HttpOptions options = new HttpOptions
            {
                ApplicationName = "BeatSaverAPI",
                Version = new Version(1, 0, 0),
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            // Use this to interact with the API
            BeatSaver beatsaver = new BeatSaver(options);
            Beatmap bm;
            if (args[0].Length == 40)
            {
                bm = beatsaver.Hash(args[0]).Result;
            } else
            {
                bm = beatsaver.Key(args[0]).Result;
            }
            if (bm != null)
            {
                Beatmap_data beatmap_data = new Beatmap_data
                {
                    Id = bm.ID,
                    Key = bm.Key,
                    Name = bm.Name,
                    Hash = bm.Hash,
                    Uploaded = bm.Uploaded.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    DirectDownload = bm.DirectDownload,
                    DownloadURL = bm.DownloadURL,
                    CoverURL = bm.CoverURL,
                    DeletedAt = null,
                    Description = bm.Description,
                    Metadata = new Metadata
                    {
                        Duration = bm.Metadata.Duration,
                        Automapper = bm.Metadata.Automapper,
                        LevelAuthorName = bm.Metadata.LevelAuthorName,
                        SongAuthorName = bm.Metadata.SongAuthorName,
                        SongName = bm.Metadata.SongName,
                        SongSubName = bm.Metadata.SongSubName,
                        Bpm = bm.Metadata.BPM
                    }
                };
                string json = JsonConvert.SerializeObject(beatmap_data, Formatting.Indented);
                Console.WriteLine(json);
            }
            else
            {
                Console.WriteLine("err");
            }
        }
    }
}

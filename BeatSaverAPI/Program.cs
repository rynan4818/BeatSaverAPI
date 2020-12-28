using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaverSharp;
using Newtonsoft.Json;

namespace BeatSaverAPI
{
    public class Beatmap_data
    {
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("stats")]
        public Stats Stats { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("deletedAt")]
        public string DeletedAt { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uploader")]
        public Uploader Uploader { get; set; }

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
    }

    public class Metadata
    {
        [JsonProperty("difficulties")]
        public MetadataDifficulties Difficulties { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("automapper")]
        public string Automapper { get; set; }

        [JsonProperty("characteristics")]
        public Characteristic[] Characteristics { get; set; }

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

    public class Characteristic
    {
        [JsonProperty("difficulties")]
        public CharacteristicDifficulties Difficulties { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class CharacteristicDifficulties
    {
        [JsonProperty("easy")]
        public Difficulty Easy { get; set; }

        [JsonProperty("expert")]
        public Difficulty Expert { get; set; }

        [JsonProperty("expertPlus")]
        public Difficulty ExpertPlus { get; set; }

        [JsonProperty("hard")]
        public Difficulty Hard { get; set; }

        [JsonProperty("normal")]
        public Difficulty Normal { get; set; }
    }

    public class Difficulty
    {
        [JsonProperty("duration")]
        public float Duration { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("njs")]
        public float Njs { get; set; }

        [JsonProperty("njsOffset")]
        public float NjsOffset { get; set; }

        [JsonProperty("bombs")]
        public int Bombs { get; set; }

        [JsonProperty("notes")]
        public int Notes { get; set; }

        [JsonProperty("obstacles")]
        public int Obstacles { get; set; }
    }

    public class MetadataDifficulties
    {
        [JsonProperty("easy")]
        public bool Easy { get; set; }

        [JsonProperty("expert")]
        public bool Expert { get; set; }

        [JsonProperty("expertPlus")]
        public bool ExpertPlus { get; set; }

        [JsonProperty("hard")]
        public bool Hard { get; set; }

        [JsonProperty("normal")]
        public bool Normal { get; set; }
    }

    public class Stats
    {
        [JsonProperty("downloads")]
        public int Downloads { get; set; }

        [JsonProperty("plays")]
        public int Plays { get; set; }

        [JsonProperty("downVotes")]
        public int DownVotes { get; set; }

        [JsonProperty("upVotes")]
        public int UpVotes { get; set; }

        [JsonProperty("heat")]
        public float Heat { get; set; }

        [JsonProperty("rating")]
        public float Rating { get; set; }
    }

    public class Uploader
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int timeout = 10;
            if (args.Length >= 2)
                timeout = int.Parse(args[1]);
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
                bm = beatsaver.Hash(args[0]).Result;
            else
                bm = beatsaver.Key(args[0]).Result;
            if (bm != null)
            {
                Characteristic[] meta_characteristics = new Characteristic[bm.Metadata.Characteristics.Count];
                int i = 0;
                foreach(BeatmapCharacteristic a in bm.Metadata.Characteristics)
                {
                    meta_characteristics[i] = new Characteristic();
                    meta_characteristics[i].Name = a.Name;
                    CharacteristicDifficulties meta_characteristicDifficulties = new CharacteristicDifficulties();
                    if (a.Difficulties.ContainsKey("easy"))
                        meta_characteristicDifficulties.Easy = Difficulty_data(a.Difficulties["easy"]);
                    if (a.Difficulties["expert"] != null)
                        meta_characteristicDifficulties.Expert = Difficulty_data((BeatmapCharacteristicDifficulty)a.Difficulties["expert"]);
                    if (a.Difficulties["expertPlus"] != null)
                        meta_characteristicDifficulties.ExpertPlus = Difficulty_data((BeatmapCharacteristicDifficulty)a.Difficulties["expertPlus"]);
                    if (a.Difficulties["hard"] != null)
                        meta_characteristicDifficulties.Hard = Difficulty_data((BeatmapCharacteristicDifficulty)a.Difficulties["hard"]);
                    if (a.Difficulties["normal"] != null)
                        meta_characteristicDifficulties.Normal = Difficulty_data((BeatmapCharacteristicDifficulty)a.Difficulties["normal"]);
                    meta_characteristics[i].Difficulties = meta_characteristicDifficulties;
                    i++;
                }
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
                        Bpm = bm.Metadata.BPM,
                        Characteristics = new Characteristic[]
                        {
                            new Characteristic
                            {
                                Name = bm.Metadata.Characteristics[0].Name
                            }
                        }
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
        private static Difficulty Difficulty_data(BeatmapCharacteristicDifficulty? characteristicDifficulty)
        {
            if (characteristicDifficulty != null)
            {
                BeatmapCharacteristicDifficulty temp = new BeatmapCharacteristicDifficulty();
                temp = (BeatmapCharacteristicDifficulty)characteristicDifficulty;
                return new Difficulty
                {
                    Duration = temp.Duration,
                    Length = temp.Length,
                    Njs = temp.NoteJumpSpeed,
                    NjsOffset = temp.NoteJumpSpeedOffset,
                    Bombs = temp.Bombs,
                    Notes = temp.Notes,
                    Obstacles = temp.Obstacles
                };
            }
            else
            {
                return null;
            }
        }
    }
}

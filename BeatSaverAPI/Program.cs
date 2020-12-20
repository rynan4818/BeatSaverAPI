using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaverSharp;

namespace BeatSaverAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpOptions options = new HttpOptions
            {
                ApplicationName = "Test Client",
                Version = new Version(1, 0, 0),
            };

            // Use this to interact with the API
            BeatSaver beatsaver = new BeatSaver(options);
            Task.Run(async () =>
            {
                Beatmap bm = await beatsaver.Hash("CA7D89AC3A74164C961385DBD147BF6BEC96C42E");
                if (bm != null)
                {
                    Console.WriteLine(bm.Key);
                }
                else
                {
                    Console.WriteLine("err");
                }
            });
            var str = Console.ReadLine();
        }
    }
}

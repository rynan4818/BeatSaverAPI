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
        }
    }
}

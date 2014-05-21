using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace hwo_statistics
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var parser = new HwoParser();
            //var json = parser.GetData();

            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data.json");
            string json = File.ReadAllText(path);

            var stats = new HwoStatistics();
            stats.Print(json);

            Console.ReadLine();
        }
    }

   
   

    public class Team
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }

        public bool HasRanking { get; set; }

        public bool BuildOk { get; set; }
    }
}
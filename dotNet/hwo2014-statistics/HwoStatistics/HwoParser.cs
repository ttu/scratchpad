using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace hwo_statistics
{
    public class HwoParser
    {
        private const string _url = "https://helloworldopen.com/team/";

        private CancellationTokenSource _fetchTokenSource = new CancellationTokenSource();
        private CancellationTokenSource _parseTokenSource = new CancellationTokenSource();

        private BlockingCollection<Tuple<int, string>> _htmls = new BlockingCollection<Tuple<int, string>>();
        private ConcurrentDictionary<int, Team> _teams = new ConcurrentDictionary<int, Team>();

        private volatile int _index = 1;

        public string GetData()
        {
            var fetchTasks = CreateFetchTasks(25);
            var parseTasks = CreateParseTasks(4);

            // When all parsers have been withouth work for 5sec we can quit
            Task.WaitAll(parseTasks.ToArray());
            _fetchTokenSource.Cancel();

            var json = JsonConvert.SerializeObject(_teams.Values);

            return json;
        }

        private Task[] CreateFetchTasks(int taskCount)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < taskCount; i++)
            {
                var fetchTask = Task.Factory.StartNew(async t =>
                {
                    var token = (CancellationToken)t;

                    var client = new WebClient();

                    while (!token.IsCancellationRequested)
                    {
                        int id = _index++;

                        try
                        {
                            var content = await client.DownloadStringTaskAsync(_url + id);
                            _htmls.Add(Tuple.Create(id, content));
                        }
                        catch (WebException)
                        {
                            Console.WriteLine("Not found {0}", id);
                        }
                    }
                }, _fetchTokenSource.Token);

                tasks.Add(fetchTask);
            }

            return tasks.ToArray();
        }

        private Task[] CreateParseTasks(int taskCount)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < taskCount; i++)
            {
                var parseTask = Task.Factory.StartNew(t =>
                {
                    var token = (CancellationToken)t;

                    try
                    {
                        while (true)
                        {
                            Tuple<int, string> data;

                            if (!_htmls.TryTake(out data, 5000, token))
                                break;

                            var team = ParseTeam(data.Item1, data.Item2);

                            if (team == null)
                                continue;

                            _teams.TryAdd(data.Item1, team);

                            Console.WriteLine(data.Item1);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }, _parseTokenSource.Token);

                tasks.Add(parseTask);
            }

            return tasks.ToArray();
        }

        private Team ParseTeam(int id, string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var hasTeam = doc.DocumentNode
                                .SelectNodes("//section[@class='page-header team-header']")
                                .Any();

            if (!hasTeam)
                return null;

            var countryUrl = doc.DocumentNode
                                .SelectNodes("//img[@class='country-flag']")
                                .First()
                                .Attributes["src"].Value;

            var startIndex = countryUrl.LastIndexOf('/') + 1;
            var country = countryUrl.Substring(startIndex, countryUrl.LastIndexOf('.') - startIndex);

            var language = doc.DocumentNode
                               .SelectNodes("//div[@class='item programming-language']")
                               .First()
                               .ChildNodes[2].InnerText.Trim();

            var hasRanking = doc.DocumentNode
                              .SelectNodes("//div[@class='ranking regional']") != null;

            var buildOk = doc.DocumentNode
                              .SelectSingleNode("//span[@class='buildStatus ok status']") != null;

            return new Team { Id = id, Country = country, Language = language, HasRanking = hasRanking, BuildOk = buildOk };
        }
    }
}
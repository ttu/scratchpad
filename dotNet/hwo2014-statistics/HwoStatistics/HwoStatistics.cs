using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace hwo_statistics
{
    // Not best solution performance wise, but does what it needs to

    public class HwoStatistics
    {
        public void Print(string json)
        {
            IList<Team> teams = JsonConvert.DeserializeObject<IList<Team>>(json);

            Debug.WriteLine("Country, Language, Total, Has Ranking, CI Build Ok, Total %, Has Ranking %, CI Build Ok %");
            Debug.WriteLine(string.Empty);

            var totalCount = teams.Count;
            var wRankingCount = teams.Where(t => t.HasRanking).Count();
            var wBuildOkCount = teams.Where(t => t.BuildOk).Count();

            GetGroupData("Global", teams, totalCount, wRankingCount, wBuildOkCount);

            var countries = teams.GroupBy(t => t.Country).OrderByDescending(g => g.Count());

            foreach (var group in countries)
            {
                GetGroupData(group.Key, group.ToList(), teams.Count, wRankingCount, wBuildOkCount);
            }
        }

        private static void GetGroupData(string key, IList<Team> group, double totalCount, double wRankingCount, double wBuildOkCount)
        {
            var withRanking = group.Where(t => t.HasRanking);
            var buildOk = group.Where(t => t.BuildOk);

            Debug.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                key,
                "All",
                group.Count(),
                withRanking.Count(),
                buildOk.Count(),
                String.Format("{0:P2}", group.Count() / totalCount),
                String.Format("{0:P2}", withRanking.Count() / wRankingCount),
                String.Format("{0:P2}", buildOk.Count() > 0 ? buildOk.Count() / wBuildOkCount : 0)              
            );

            var lanAll = group.GroupBy(t => t.Language).OrderByDescending(g => g.Count());
            var lanWithRanking = withRanking.GroupBy(t => t.Language);
            var lanBuildOk = buildOk.GroupBy(t => t.Language);

            foreach (var lanGroup in lanAll)
            {
                var selecedWithRanking = lanWithRanking.SingleOrDefault(g => g.Key == lanGroup.Key);
                double selWRanking = selecedWithRanking != null ? selecedWithRanking.ToList().Count() : 0;

                var selectedBuildOk = lanBuildOk.SingleOrDefault(g => g.Key == lanGroup.Key);
                double selBuildOk = selectedBuildOk != null ? selectedBuildOk.ToList().Count() : 0;

                Debug.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                    key,
                    lanGroup.Key,
                    lanGroup.Count(),
                    selWRanking,
                    selBuildOk,
                    String.Format("{0:P2}", lanGroup.Count() / (double)group.Count()),
                    String.Format("{0:P2}", withRanking.Count() > 0 && selWRanking > 0 ? selWRanking / withRanking.Count() : 0),
                    String.Format("{0:P2}", buildOk.Count() > 0 && selBuildOk > 0 ? selBuildOk / buildOk.Count() : 0)                );
            }

            Debug.WriteLine(string.Empty);
        }
    }
}
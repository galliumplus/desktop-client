using Couche_Data.Dao;
using Couche_Data.Interfaces;
using GalliumPlusApi.Dto;
using Modeles;
using System.Diagnostics;

namespace GalliumPlusApi.Dao
{
    public class LogDao : ILogDAO
    {
        private HistoryActionDetails.Mapper mapper = new();

        public void CreateLog(Log log) { /* non */ }

        public List<Log> GetLogs(int mois, int annee)
        {
            DateTime debutMois = new DateTime(annee, mois, 1);
            DateTime finMois = new DateTime(annee, mois, DateTime.DaysInMonth(annee, mois));

            string search = $"?from={debutMois:s}&to={finMois:s}&pageSize=99999";

            using var client = new GalliumPlusHttpClient();
            client.UseSessionToken(SessionStorage.Current.Get<string>("token"));

            try
            {
                var products = client.Get<List<HistoryActionDetails>>("v1/history" + search);

                List<Log> logList = mapper.ToModel(products).ToList();
                logList.Sort((log1, log2) => log2.Date.CompareTo(log1.Date));
                return logList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception non gérée lors de la récupération des logs : {ex}");
                return new List<Log>();
            }
        }
    }
}

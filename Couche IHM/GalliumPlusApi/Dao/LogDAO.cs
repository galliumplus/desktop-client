using Couche_Data.Interfaces;
using Modeles;

namespace GalliumPlusApi.Dao
{
    public class LogDAO : ILogDAO
    {
        public void CreateLog(Log log)
        {
            
        }

        public List<Log> GetLogs(int mois, int annee)
        {
            return new();
        }
    }
}

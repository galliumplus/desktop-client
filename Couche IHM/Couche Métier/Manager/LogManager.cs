using Couche_Data.Interfaces;
using GalliumPlusApi.Dao;
using Modeles;

namespace Couche_Métier.Manager
{
    public class LogManager
    {
        #region attributes
        /// <summary>
        /// Permet d'accéder aux données
        /// </summary>
        private ILogDAO logDao;

        /// <summary>
        /// Liste des logs
        /// </summary>
        private List<Log> logs = new List<Log>();
        #endregion

        #region constructor
        /// <summary>
        /// Constructeur du log Manager
        /// </summary>
        public LogManager(UserManager users)
        {
            logDao = new LogDAO(users.GetComptes());

            Task.Run(() =>
            {
                int annee = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                int mois = Convert.ToInt32(DateTime.Now.ToString("MM"));
                this.logs = this.logDao.GetLogs(mois, annee);
            });
        }
        #endregion

        #region methods
        public List<Log> GetLogs(int mois = 0,int annee=0)
        {
            List<Log> logs;
            if (mois == 0 || annee == 0)
            {
                logs = this.logs;
            }
            else
            {
                logs = this.logDao.GetLogs(mois, annee);
            }
            return logs;
        }


        public void CreateLog(Log log)
        {
            this.logs.Insert(0, log);
            this.logDao.CreateLog(log);
        }

        public IPaginatedLogReader GetLogsReader(int mois, int annee)
        {
            return this.logDao.GetLogsReader(mois, annee);
        }

        #endregion
    }
}

using Couche_Data;
using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Manager
{
    public class LogManager
    {

        /// <summary>
        /// Permet d'accéder aux données
        /// </summary>
        private LogDAO logDao;

        /// <summary>
        /// Liste des logs
        /// </summary>
        private List<Modeles.Log> logs = new List<Modeles.Log>();

        /// <summary>
        /// Liste des catégories de log
        /// </summary>
        private List<LogTheme> logThemes = new List<LogTheme>();

        public LogManager()
        {
            logDao = new LogDAO();
            this.logs = this.logDao.GetLogs();
            this.logThemes = this.logDao.GetLogsTheme();
        }

        public List<Modeles.Log> GetLogs()
        {
            return this.logs;
        }

        public List<Modeles.LogTheme> GetLogsTheme()
        {
            return this.logThemes;
        }

        public void CreateLog(Modeles.Log log)
        {
            this.logs.Insert(0, log);
            this.logDao.CreateLog(log);

        }
    }
}

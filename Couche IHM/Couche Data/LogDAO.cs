using Modeles;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class LogDAO
    {
        private List<Log> logs = new List<Log>();
        private List<LogTheme> logThemes = new List<LogTheme>();    

        public LogDAO()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM logs WHERE YEAR(date_at) = YEAR(CURRENT_DATE) AND MONTH(date_at) = MONTH(CURRENT_DATE) ORDER BY date_at DESC";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                logs.Add(new Log(rdr.GetInt32("log_id"), DateTime.Parse(rdr.GetString("date_at")),rdr.GetInt16("log_category_id"), rdr.GetString("text"), rdr.GetString("user")));
            }
            rdr.Close();    

            //Requette SQL
            string stm2 = "SELECT * FROM logs_categories";
            MySqlCommand cmd2 = new MySqlCommand(stm2, dbsDAO.Instance.Sql);
            cmd2.Prepare();

            //lecture de la requette
            MySqlDataReader rdr2 = cmd2.ExecuteReader();

            while (rdr2.Read())
            {
                logThemes.Add(new LogTheme(rdr2.GetInt32("log_category_id"), rdr2.GetString("name")));
            }

            dbsDAO.Instance.CloseDatabase();
        }

        public void CreateLog(Log log)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string formattedDate = log.Date.ToString("yyyy-MM-dd HH:mm:ss");
            string stm = $"INSERT INTO logs VALUES(0,'{log.Message}','{formattedDate}',{log.Theme},'{log.Auteur}')";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

            dbsDAO.Instance.CloseDatabase();
        }


        public List<Log> GetLogs()
        {
            return this.logs;
        }

        public List<LogTheme> GetLogsTheme()
        {
            return this.logThemes;
        }



    }
}

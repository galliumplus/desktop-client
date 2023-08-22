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


        public List<Log> GetLogs(int mois,int annee)
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = $"SELECT * FROM logs WHERE YEAR(date_at) =  {annee} AND MONTH(date_at) = {mois} ORDER BY date_at DESC";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<Log> logs = new List<Log>();
            while (rdr.Read())
            {
                logs.Add(new Log(rdr.GetInt32("log_id"), DateTime.Parse(rdr.GetString("date_at")), rdr.GetInt16("log_category_id"), rdr.GetString("text"), rdr.GetString("user")));
            }
            rdr.Close();
            dbsDAO.Instance.CloseDatabase();
            return logs;
        }

        public List<LogTheme> GetLogsTheme()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm2 = "SELECT * FROM logs_categories";
            MySqlCommand cmd2 = new MySqlCommand(stm2, dbsDAO.Instance.Sql);
            cmd2.Prepare();

            //lecture de la requette
            MySqlDataReader rdr2 = cmd2.ExecuteReader();

            List<LogTheme> logsTheme = new List<LogTheme>();
            while (rdr2.Read())
            {
                logsTheme.Add(new LogTheme(rdr2.GetInt32("log_category_id"), rdr2.GetString("name")));
            }

            dbsDAO.Instance.CloseDatabase();
            return logsTheme;
        }



    }
}

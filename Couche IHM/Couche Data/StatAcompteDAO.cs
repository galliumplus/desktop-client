using Modeles;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class StatAcompteDAO
    {

        public List<StatAcompte> GetStat()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT acompte_id,sum(amount) as argent FROM best_acomptes WHERE YEARWEEK(date) = YEARWEEK(CURRENT_DATE) group by acompte_id order by argent desc";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<StatAcompte> statAcompteList = new List<StatAcompte>();
            while (rdr.Read())
            {
                statAcompteList.Add(new StatAcompte(0, DateTime.Now, rdr.GetFloat("argent"), rdr.GetInt16("acompte_id")));
            }

            rdr.Close();
            dbsDAO.Instance.CloseDatabase();
            return statAcompteList;
        }



        public void CreateStat(StatAcompte stat)
        {
            lock (dbsDAO.Instance.DatabaseLock)
            {
                //Connection
                dbsDAO.Instance.OpenDataBase();

                //Requette SQL
                string formattedDate = stat.Date.ToString("yyyy-MM-dd");
                string formattedAmountMoney = stat.Amount_money.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string stm = $"INSERT INTO best_acomptes VALUES(0,{formattedAmountMoney},'{formattedDate}',{stat.Aompte_Id})";
                MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
                cmd.Prepare();

                //lecture de la requette
                cmd.ExecuteNonQuery();


                dbsDAO.Instance.CloseDatabase();
            }
        }
    }
}

using Modeles;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class StatProduitDAO
    {

        public List<StatProduit> GetStat()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT product_id,sum(number_sales) as nb FROM best_sales group by product_id order by nb desc";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<StatProduit> statProduitList = new List<StatProduit>();
            while (rdr.Read())
            {
                statProduitList.Add(new StatProduit(0, DateTime.Now, rdr.GetInt16("nb"), rdr.GetInt16("product_id")));
            }

            rdr.Close();
            dbsDAO.Instance.CloseDatabase();
            return statProduitList;
        }



        public void CreateStat(StatProduit stat)
        {
            lock (dbsDAO.Instance.DatabaseLock)
            {
                //Connection
                dbsDAO.Instance.OpenDataBase();

                //Requette SQL
                string formattedDate = stat.Date.ToString("yyyy-MM-dd");
                string stm = $"INSERT INTO best_sales VALUES(0,'{formattedDate}',{stat.Number_sales},{stat.Product_id})";
                MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
                cmd.Prepare();

                //lecture de la requette
                cmd.ExecuteNonQuery();


                dbsDAO.Instance.CloseDatabase();
            }
        }
    }
}

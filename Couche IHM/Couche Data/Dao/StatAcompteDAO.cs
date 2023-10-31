using Couche_Data.Interfaces;
using Modeles;
using MySql.Data.MySqlClient;


namespace Couche_Data.Dao
{
    public class StatAcompteDAO : IStatAcompteDAO
    {

        public List<StatAcompte> GetStat()
        {
            //Connection
            MySqlConnection sql = new MySqlConnection(dbsDAO.ConnectionString);
            sql.Open();

            //Requette SQL
            string stm = "SELECT acompte_id,sum(amount) as argent FROM best_acomptes WHERE YEARWEEK(date) = YEARWEEK(CURRENT_DATE) group by acompte_id order by argent desc";
            MySqlCommand cmd = new MySqlCommand(stm, sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<StatAcompte> statAcompteList = new List<StatAcompte>();
            while (rdr.Read())
            {
                statAcompteList.Add(new StatAcompte(0, DateTime.Now, rdr.GetFloat("argent"), rdr.GetInt16("acompte_id")));
            }

            rdr.Close();
            sql.Close();
            return statAcompteList;
        }



        public void CreateStat(StatAcompte stat)
        {
            string connString = String.Format("server={0};port={1};user id={2};password={3};database={4};SslMode={5}", "51.178.36.43", "3306", "c2_gallium", "DfD2no5UJc_nB", "c2_etismash", "none");
            MySqlConnection sql = new MySqlConnection(connString);
            sql.Open();

            //Requette SQL
            string formattedDate = stat.Date.ToString("yyyy-MM-dd");
            string formattedAmountMoney = stat.Money.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string stm = $"INSERT INTO best_acomptes VALUES(0,{formattedAmountMoney},'{formattedDate}',{stat.Acompte_Id})";
            MySqlCommand cmd = new MySqlCommand(stm, sql);
            cmd.Prepare();

            //lecture de la requette
            cmd.ExecuteNonQuery();

            sql.Close();
        }
    }
}

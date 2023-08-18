
using Modeles;
using MySql.Data.MySqlClient;

namespace Couche_Data
{
    public class AdhérentDao : IAdhérentDao
    {
        private List<Adhérent> adherents = new List<Adhérent>();

        public AdhérentDao()
        {
            //Connection
            dbsDAO.Instance.OpenDataBase();

            //Requette SQL
            string stm = "SELECT * FROM acompte ORDER BY nom";
            MySqlCommand cmd = new MySqlCommand(stm, dbsDAO.Instance.Sql);
            cmd.Prepare();

            //lecture de la requette
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                adherents.Add(new Adhérent(rdr.GetInt32("acompte_id"), rdr.GetString("login"), rdr.GetString("nom"), rdr.GetString("prenom"), rdr.GetFloat("balance"), ""));
            }

            dbsDAO.Instance.CloseDatabase();
        }

        public void CreateAdhérent(Adhérent adhérent)
        {
            this.adherents.Add(adhérent);
        }


        public List<Adhérent> GetAdhérents()
        {
            return this.adherents;
        }

        public void RemoveAdhérent(Adhérent adhérent)
        {
            this.adherents.Remove(adhérent);
        }

        public void UpdateAdhérent(Adhérent adhérent)
        {
            Adhérent adhér = adherents.Find(adh => adh.Id == adhérent.Id);
            adhér.Nom = adhérent.Nom;
            adhér.Prenom = adhérent.Prenom;
            adhér.Argent = adhérent.Argent;
            adhér.StillAdherent = adhérent.StillAdherent;
            adhér.CanPass = adhérent.CanPass;
            adhér.Formation = adhérent.Formation;
        }
    }
}

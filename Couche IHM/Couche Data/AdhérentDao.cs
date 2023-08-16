
using Modeles;


namespace Couche_Data
{
    public class AdhérentDao : IAdhérentDao
    {
        private List<Adhérent> adherents = new List<Adhérent>();

        public AdhérentDao()
        {
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

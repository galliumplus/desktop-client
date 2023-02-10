using Couche_Métier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Data
{
    public class FakeAdherentDao : IAdhérentDao
    {
        private Dictionary<string, Adhérent> adherents = new Dictionary<string, Adhérent>()
        {
            { "fm427410",new Adhérent("fm427410", "marteau", "florian", 10.5f, true) },
            { "dc456852",new Adhérent("dc456852", "chabret", "damien", 105f) },
            {"ar938426", new Adhérent("ar938426", "roura", "aimeric", 8.22f, true)},
            {"mb967843",new Adhérent("mb967843", "badet", "matteo", 0.3f, true) },
            {"em335144",new Adhérent("em335144", "macron", "emmanuel", 12f) },
            {"pg000000",new Adhérent("pg000000", "glesser", "philippe", 55f) },
            {"mg881210", new Adhérent("mg881210", "grandvincent", "marc", 7.77f)},
            {"pc656565",new Adhérent("pc656565", "caca", "pipi", 1.5f, true) }

        };
        public void CreateAdhérent(Adhérent adhérent)
        {
            this.adherents.Add(adhérent.Identifiant, adhérent);
        }

        public Adhérent GetAdhérent(int id)
        {
            // return this.adherents[id];
            return null;
        }

        public Dictionary<string, Adhérent> GetAdhérents()
        {
            return this.adherents;
        }

        public void RemoveAdhérent(Adhérent adhérent)
        {
            this.adherents.Remove(adhérent.Identifiant);
        }

        public void UpdateAdhérent(Adhérent adhérent)
        {
            this.adherents[adhérent.Identifiant] = adhérent;
        }
    }
}

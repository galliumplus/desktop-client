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
        public void CreateAdhérent(Adhérent adhérent)
        {
            throw new NotImplementedException();
        }

        public Adhérent GetAdhérent(string id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Adhérent> GetAdhérents()
        {
            Adhérent a1 = new Adhérent("fm427410", "marteau", "florian", true, 10.5f);
            Adhérent a2 = new Adhérent("dc456852", "chabret", "damien", false, 105f);
            Adhérent a3 = new Adhérent("ar938426", "roura", "aimeric", false, 8.22f);
            Adhérent a4 = new Adhérent("mb967843", "badet", "matteo", true, 0.3f);
            Adhérent a5 = new Adhérent("em335144", "macron", "emmanuel", true, 12f);
            Adhérent a6 = new Adhérent("pg000000", "glesser", "philippe", true, 55f);
            Adhérent a7 = new Adhérent("mg881210", "grandvincent", "marc", false, 7.77f);
            Adhérent a8 = new Adhérent("pc656565", "caca", "pipi", false, 1.5f);
            return new Dictionary<string, Adhérent>() { { a1.Id, a1 }, { a2.Id, a2 }, { a3.Id, a3 }, { a4.Id, a4 }, { a5.Id, a5 }, { a6.Id, a6 } , { a7.Id, a7 } 
            ,{ a8.Id, a8}};
        }

        public void RemoveAdhérent(Adhérent adhérent)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdhérent(Adhérent adhérent)
        {
            throw new NotImplementedException();
        }
    }
}

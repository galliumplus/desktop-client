﻿using Couche_Métier;
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
            { "fm427410",new Adhérent("fm427410", "marteau", "florian", 10.5f,"1A", true) },
            { "dc456852",new Adhérent("dc456852", "chabret", "damien", 105f,"1A") },
            {"ar938426", new Adhérent("ar938426", "roura", "aimeric", 8.22f,"1A", true)},
            {"mb967843",new Adhérent("mb967843", "badet", "matteo", 0.3f,"Chien", true) },
            {"em335144",new Adhérent("em335144", "macron", "emmanuel", 12f,"President") },
            {"pg000000",new Adhérent("pg000000", "glesser", "philippe", 55f,"1A") },
            {"mg881210", new Adhérent("mg881210", "grandvincent", "marc", 7.77f,"Chomeur")},
            {"pc656565",new Adhérent("pc656565", "caca", "pipi", 1.5f,"1A", true) },
            { "zz4684357",new Adhérent("zz4684357", "zidane", "zinedine", 0.57f,"1A") },
            {"km303124", new Adhérent("km303124", "mbappe", "kylian", 80.22f,"2A", true)},
            {"fp900000",new Adhérent("fp900000", "pourquoi", "feur", 10f,"1A", true) },
            {"ab115789",new Adhérent("ab115789", "bbbbbb", "aaaaaa", 12.35f,"Staps") },
            {"po000000",new Adhérent("po000000", "outai", "papa", 5.5f,"1A") },
            {"fr354678", new Adhérent("fr354678", "ribery", "franck", 17.27f,"1A")},
            {"lb113365",new Adhérent("lb113365", "blanc", "laurent", 12.57f,"Feur", true) },
            {"zz356482",new Adhérent("zz356482", "zizi", "zaza", 12.57f,"Foot", true) },


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

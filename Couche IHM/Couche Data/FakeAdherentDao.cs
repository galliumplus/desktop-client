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
        private Dictionary<int, Adhérent> adherents = new Dictionary<int, Adhérent>()
        {
            { 1,new Adhérent(1,"fm427410", "marteau", "florian", 10.5,"1A", true) },
            { 2,new Adhérent(2,"dc456852", "chabret", "damien", 105,"1A") },
            {3, new Adhérent(3,"ar938426", "roura", "aimeric", 8.22,"1A", true)},
            {4,new Adhérent(4,"mb967843", "badet", "matteo", 0.3,"Chien", true) },
            {5,new Adhérent(5,"em335144", "macron", "emmanuel", 12,"President") },
            {6,new Adhérent(6,"pg000000", "glesser", "philippe", 55,"1A") },
            {7, new Adhérent(7,"mg881210", "grandvincent", "marc", 7.77,"Chomeur")},
            {8,new Adhérent(8,"pc656565", "caca", "pipi", 1.5,"1A", true) },
            { 9,new Adhérent(9,"zz4684357", "zidane", "zinedine", 0.57,"1A") },
            {10, new Adhérent(10,"km303124", "mbappe", "kylian", 80.22,"2A", true)},
            {11,new Adhérent(11,"fp900000", "pourquoi", "feur", 10,"1A", true) },
            {12,new Adhérent(12,"ab115789", "bbbbbb", "aaaaaa", 12.35,"Staps") },
            {13,new Adhérent(13,"po000000", "outai", "papa", 5.5,"1A") },
            {14, new Adhérent(14,"fr354678", "ribery", "franck", 17.27,"1A")},
            {15,new Adhérent(15,"lb113365", "blanc", "laurent", 12.57,"Feur", true) },
            {16,new Adhérent(16,"zz356482", "zizi", "zaza", 12.57,"Foot", true) },


        };
        public void CreateAdhérent(Adhérent adhérent)
        {
            this.adherents.Add(adhérent.Id, adhérent);
        }

        public Adhérent GetAdhérent(int id)
        {
            return this.adherents[id];
        }

        public Dictionary<int, Adhérent> GetAdhérents()
        {
            return this.adherents;
        }

        public void RemoveAdhérent(Adhérent adhérent)
        {
            this.adherents.Remove(adhérent.Id);
        }

        public void UpdateAdhérent(Adhérent adhérent)
        {
            this.adherents[adhérent.Id] = adhérent;
        }
    }
}

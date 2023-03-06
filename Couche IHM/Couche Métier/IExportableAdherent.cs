using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier
{
    /// <summary>
    /// Permet d'exporter un adhérent
    /// </summary>
    public interface IExportableAdherent
    {
        /// <summary>
        /// Permet d'exporter des adhérents
        /// </summary>
        /// <param name="adhérents">liste des adhérents</param>
        public void Export(List<Adhérent> adhérents);
    }
}

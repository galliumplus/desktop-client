using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Log
{
    public interface ILog
    {
        /// <summary>
        /// Path du fichier
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Enregistrer les logs
        /// </summary>
        /// <param name="categorieLog"> Categorie du log </param>
        /// <param name="message"> message à enregistrer </param>
        public void registerLog(CategorieLog categorieLog, string message, User author);

        /// <summary>
        /// Récupère les logs
        /// </summary>
        public List<string> loadLog();
    }
}

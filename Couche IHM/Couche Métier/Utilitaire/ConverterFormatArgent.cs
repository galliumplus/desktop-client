using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Couche_Métier.Utilitaire
{
    /// <summary>
    /// Converti le format de l'argent
    /// </summary>
    public class ConverterFormatArgent
    {
        /// <summary>
        /// Permet de convertir l'argent en un string formatté
        /// </summary>
        /// <param name="argent"></param>
        public string ConvertFormat(double argent)
        {
            string ret = Convert.ToString(argent);
            // 1 => 1,00
            if (new Regex("^[0-9]+$").IsMatch(ret))
            {
                ret += ",00";
            }

            // 1,2 => 1,20
            if (new Regex("^[0-9]+,[0-9]$").IsMatch(ret))
            {
                ret += "0";
            }

            ret += " €";
            return ret;
        }
    }
}

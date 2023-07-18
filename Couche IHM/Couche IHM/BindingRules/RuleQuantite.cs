using Couche_IHM.VueModeles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Couche_IHM.BindingRules
{
    public class RuleQuantite : ValidationRule
    {

        /// <summary>
        /// Permet de tester la validité du binding
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = ValidationResult.ValidResult;

            int quantite;
            if((string)value == "")
            {
                result = new ValidationResult(false, "Quantite non spécifiée");
            }
            else
            {
                if (int.TryParse((string)value, out quantite))
                {
                    quantite = Convert.ToInt32(value);
                    if (quantite < 0)
                    {
                        result = new ValidationResult(false, "Quantite inférieur à 0");
                    }
                }
                else
                {
                    result = new ValidationResult(false, "Pas un nombre");
                }
            }
            
         

            return result;
        }
    }
}

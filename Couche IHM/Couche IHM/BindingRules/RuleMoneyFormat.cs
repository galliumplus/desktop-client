using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Couche_IHM.BindingRules
{

    public class RuleMoneyFormat : ValidationRule
    {
        /// <summary>
        /// Permet de tester la validité du binding
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result;
            string argentFormat = ((string)value).Replace(".", ",");
            argentFormat = argentFormat.Replace("€", " ");
            argentFormat = argentFormat.Trim();
            if (new Regex("^[0-9]+$").IsMatch(argentFormat) || new Regex("^[0-9]+,[0-9]+$").IsMatch(argentFormat))
            {
                result = ValidationResult.ValidResult;
            }
            else
            {
                result = new ValidationResult(false, "Format invalide");
            }
            return result;
        }
    }
}

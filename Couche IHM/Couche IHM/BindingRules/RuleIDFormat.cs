using Couche_IHM.VueModeles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Couche_IHM.BindingRules
{

    public class RuleIDFormat : ValidationRule
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

            AcompteViewModel adh = MainWindowViewModel.Instance.AdherentViewModel.CurrentAcompte;
            string identifiant = (string)value;
            if (identifiant.Length == 0)
            {
                result = new ValidationResult(false, "Veuillez saisir un nom d'utilisateur");
            }
            else if (identifiant.Length > 20)
            {
                result = new ValidationResult(false, "Un nom d'utilisateur ne peut pas dépasser 20 caractères");
            }
            else
            {
                if (identifiant.Any(c => !IsAsciiAndLowerCaseLetterOrDigit(c)))
                {
                    result = new ValidationResult(false, "Un nom d'utilisateur doit comprendre uniquement des lettre minuscules et des chiffres");
                }
            }

            return result;
        }

        private static bool IsAsciiAndLowerCaseLetterOrDigit(char c)
        {
            return char.IsAscii(c) && (char.IsLower(c) || char.IsDigit(c));
        }
    }
}

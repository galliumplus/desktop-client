using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallium_v1.Logique
{
    public static class Role
    {
        private static string[] role =
        {
            "Administrateur",
            "Président",
            "Vice-Président",
            "Secrétaire",
            "Responsable Communication",
            "Vice-Responsable Communication",
            "Trésorier",
            "Vice-Trésorier"
        };

        public static string[] Roles
        {
            get => role;
        }

        public static int RoleValue(string role)
        {
            int value = 0;

            switch (role)
            {
                case "Administrateur":
                    value = 0;
                    break;
                case "Président":
                    value = 1;
                    break ;
                case "Vice-Président":
                    value = 2;
                    break;
                case "Secrétaire":
                    value = 3;
                    break;
                case "Responsable Communication":
                    value = 4;
                    break;
                case "Vice-Responsable Communication":
                    value = 5;
                    break;
                case "Trésorier":
                    value = 6;
                    break;
                case "Vice-Trésorier":
                    value = 7;
                    break;

            }

            return value;
        }

    }
}

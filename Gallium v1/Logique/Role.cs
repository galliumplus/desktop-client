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


    }
}

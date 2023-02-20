using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Log
{
    public enum CategorieLog
    {
        // GENERAL
        CONNEXION,
        VENTE,
        // PRODUCTS
        ADD_PRODUCT,
        DELETE_PRODUCT,
        UPDATE_PRODUCT,
        // ADHERENTS
        CREATE_ADHERENT,
        DELETE_ADHERENT,
        UPDATE_ADHERENT,
        // USER
        CREATE_USER,
        DELETE_USER,
        UPDATE_USER

    }
}

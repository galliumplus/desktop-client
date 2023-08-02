using Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class LogViewModel
    {

        #region attributes
        private Log log;



        #endregion
        public LogViewModel(Log log)
        {
            this.log = log;
        }
    }
}

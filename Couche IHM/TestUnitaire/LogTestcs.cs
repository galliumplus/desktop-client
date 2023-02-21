using Couche_Métier.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitaire
{
    public class LogTestcs
    {
        [Fact]
        void ReadLog()
        {
            ILog log = new LogToTXT();
            Assert.NotEmpty(log.loadLog());
        }
    }
}

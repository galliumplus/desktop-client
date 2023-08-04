using Couche_Métier.Log;
using Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Couche_IHM.VueModeles
{
    public class LogsViewModel : INotifyPropertyChanged
    {

        #region attributes 
        private List<LogViewModel> logs = new List<LogViewModel>();
        private ILog logManager;

        public LogsViewModel()
        {
            this.logManager = new LogAdherentToTxt();
            List<Log> logs = logManager.loadLog();
            foreach (Log l in logs)
            {
                this.logs.Add(new LogViewModel(l));
            }
        }


        #endregion
        #region properties
        public List<LogViewModel> Logs 
        {
            get 
            { 
                return logs.FindAll(x => DateTime.Parse(x.Date).ToString("MMMM yyyy") == DateTime.Today.ToString("MMMM yyyy")); 
            }
            set => logs = value; 
        }
        #endregion

        #region notify
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

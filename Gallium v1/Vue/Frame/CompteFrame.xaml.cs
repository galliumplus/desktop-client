using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech;
using System.Speech.Synthesis;

namespace Gallium_v1.Vue.Frame
{
    /// <summary>
    /// Logique d'interaction pour CompteFrame.xaml
    /// </summary>
    public partial class CompteFrame : Page
    {
        public CompteFrame()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer speech;

            speech = new SpeechSynthesizer();
            speech.Volume = 100;
            speech.Speak("Je pète ma bière, ma libulule. 6 morts, 8 blessés, 3 camions, deux nains, un homme content, marc qui aboie : wouaf wouaf wouaf wouaf wouaf wouaaaaaaaaf");
        }
    }
}

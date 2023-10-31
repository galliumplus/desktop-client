using Couche_IHM.VueModeles;
using GalliumPlusApi.Exceptions;
using System;
using System.Windows;

namespace Couche_IHM
{
    public static class MessageBoxErrorHandler
    {
        public static Action<T> AttachToAction<T>(Action<T> action)
        {
            return input => DoesntThrow(() => action(input));
        }

        public static bool DoesntThrow(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (UnauthenticatedException)
            {
                MessageBox.Show("Veuillez vous reconnecter.", "Session expirée", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindowViewModel.Instance.MainWindow.AskToDisconnect();
                return false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}

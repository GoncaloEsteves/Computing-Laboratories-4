using PGB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PGB.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        private ObservableCollection<Notification> notifications;
        public ObservableCollection<Notification> Notifications
        {
            get { return notifications; }
            set
            {
                if (notifications != value)
                    notifications = value;
                OnPropertyChanged("Notifications");
            }
        }

        public ICommand Delete { protected set; get; }

        public NotificationsViewModel()
        {

            Notification teste = new Notification("notification teste");
            Notifications = new ObservableCollection<Notification>();
            Notifications.Add(teste);
            Delete = new Command<Notification>((notification) =>
            {
                //opreate the remove item

                Notifications.Remove(notification);
            });
        }
    }
}

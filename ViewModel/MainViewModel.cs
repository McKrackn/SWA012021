using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using exam.Communication;
using exam.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace exam.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Notification> _notifications;

        private string visibility;

        private string warnMessage;

        public MainViewModel()
        {
            visibility = "hidden";
            ExitBtnCommand = new RelayCommand(Application.Current.Dispatcher.InvokeShutdown);
            Notifications = new ObservableCollection<Notification>();
            new Client("127.0.0.1", 8000, GUIAction);
        }

        public RelayCommand ExitBtnCommand { get; set; }

        public string Visibility
        {
            get => visibility;

            set
            {
                visibility = value;
                RaisePropertyChanged();
            }
        }

        public string WarnMessage
        {
            get => warnMessage;

            set
            {
                warnMessage = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                _notifications = value;
                RaisePropertyChanged();
            }
        }

        private void GUIAction(string message)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    var splitted = message.Split(":");
                    var splitted2 = splitted[1].Split(" ");

                    var Key = splitted[0];
                    var Value = int.Parse(splitted2[1]);

                    var NotStat = new NotificationStatus();
                    if (splitted2[2].Trim().TrimStart('(').TrimEnd(')') == "normal")
                        NotStat = NotificationStatus.normal;
                    if (splitted2[2].Trim().TrimStart('(').TrimEnd(')') == "warning")
                        NotStat = NotificationStatus.warning;
                    if (splitted2[2].Trim().TrimStart('(').TrimEnd(')') == "danger")
                        NotStat = NotificationStatus.danger;

                    var tmp = new Notification(Key, Value, NotStat, NotStat.ToString());

                    if (tmp.Key.StartsWith("CPU") && tmp.State == NotificationStatus.danger)
                    {
                        WarnMessage = DateTime.Now + ": " + tmp.Key + " state dangerous!";
                        Task.Factory.StartNew(DisplayMsg); //nix übergeben kann?
                    }

                    var found = false;

                    //so ein scheiß
                    for (var i = 0; i < Notifications.Count; i++)
                        if (Notifications[i].Key == tmp.Key)
                        {
                            Notifications[i].Value = tmp.Value;
                            Notifications[i].State = tmp.State;
                            RaisePropertyChanged("Notifications");
                            //Notifications.RemoveAt(i);
                            //Notifications.Insert(i,tmp);
                            found = true;
                        }

                    if (found == false)
                        Notifications.Add(tmp);
                });
        }

        private void DisplayMsg()
        {
            Visibility = "visible";
            Thread.Sleep(3000);
            Visibility = "hidden";
        }
    }
}
using GalaSoft.MvvmLight;

namespace exam.Model
{

    public enum NotificationStatus
    {
        normal, warning, danger
    }
    public class Notification : ViewModelBase
    {
        private int _value;
        private NotificationStatus _state;
        public string Key { get; set; }

        public int Value
        {
            get => _value;
            set { _value = value; RaisePropertyChanged();}
        }

        public NotificationStatus State
        {
            get => _state;
            set { _state = value; RaisePropertyChanged(); }
        }

        public string FullNot { get; set; }

        public Notification(string key, int value, NotificationStatus state, string fullNot)
        {
            Key = key;
            Value = value;
            State = state;
            FullNot = fullNot;
        }
    }
}

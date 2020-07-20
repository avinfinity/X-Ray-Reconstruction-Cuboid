using System.ComponentModel;
using System.Windows.Input;

namespace RecoCuboid_Sample
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public override string ToString()
        {
            return "Basic";
        }
        public MouseGesture PanGesture => new MouseGesture(MouseAction.LeftClick);

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
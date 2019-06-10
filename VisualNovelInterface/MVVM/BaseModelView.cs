using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VisualNovelInterface.MVVM
{
    public class BaseObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseObject()
        {

        }


        protected void SetProperty<T>(ref T _propertyReference, T _value, [CallerMemberName]string _propertyName = "")
        {
            _propertyReference = _value;
            OnPropertyChanged(_propertyName);
        }
        
        protected void OnPropertyChanged([CallerMemberName]string _propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualNovelInterface.MVVM;

namespace VisualNovelInterface
{
    public class TestViewModel : BaseViewModel
    {
        string stringProperty;

        public string StringProperty {
            get => stringProperty;
            set {
                stringProperty = value;
                OnPropertyChanged(nameof(stringProperty));
            }
        }
    }
}

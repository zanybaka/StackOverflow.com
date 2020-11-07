using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows;

namespace TestNested.ViewModels
{
	public class ViewModelsBase : ViewModelBase, INotifyPropertyChanged
    {
        
        public virtual string DocumentationUrl
        {
            get
            {
                return "";
            }
        }

        public virtual string Name
        {
            get
            {
                return "";
            }
        }

        public ViewModelsBase() { }
    }
}

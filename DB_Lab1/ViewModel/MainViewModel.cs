using DB_Lab1.MVVM;
using DB_Lab1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DB_Lab1.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl activeControl = new UCSignUp();        

        public UserControl ActiveControl
        {
            get => activeControl;
            set
            {
                activeControl = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(Messenger msgr)
        {
            msgr.Receive(GetType(),
                (message) =>
                {                    
                    ActiveControl = message as UserControl;
                });
        }
    }
}

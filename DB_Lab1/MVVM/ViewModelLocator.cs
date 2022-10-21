using DB_Lab1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab1.MVVM
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IoCContainer.Resolve<MainViewModel>();

        public ViewModelDataBase ViewModelDataBase => IoCContainer.Resolve<ViewModelDataBase>();

        public ViewModelSignUp ViewModelSignUp => IoCContainer.Resolve<ViewModelSignUp>();

        public ViewModelSignIn ViewModelSignIn => IoCContainer.Resolve<ViewModelSignIn>();
    }
}

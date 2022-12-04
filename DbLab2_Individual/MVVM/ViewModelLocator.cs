using DbLab2_Individual.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.MVVM
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => IoC.Resolve<MainViewModel>();

        public CreateRelationViewModel CreateRelationViewModel => IoC.Resolve<CreateRelationViewModel>();

        public RequestsViewModel RequestsViewModel => IoC.Resolve<RequestsViewModel>();

        public ChartViewModel ChartViewModel => IoC.Resolve<ChartViewModel>();
    }
}

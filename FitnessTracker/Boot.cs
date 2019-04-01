using Caliburn.Micro;
using FitnessTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitnessTracker
{
    public class Boot : BootstrapperBase
    {
        public Boot()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}

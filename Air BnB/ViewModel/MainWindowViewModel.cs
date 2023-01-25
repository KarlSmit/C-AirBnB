using Air_BnB.Models;
using Air_BnB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_BnB.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();

        public MainWindowViewModel()
        {
            Db.Database.EnsureCreatedAsync();
        }
       
    }
}

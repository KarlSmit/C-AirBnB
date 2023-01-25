using Air_BnB.Models;
using Air_BnB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Air_BnB.Utils
{
    /// <summary>
    /// Base class for everything needed across multiple viewmodels such as IPropertyChanged and navigation
    /// </summary>
    public class ViewModelBase : BindableBase
    {
        public ICommand NavigateToViewCommand { get; set; }
        
        private object selectedViewModel;
        public object SelectedViewModel
        {
            get => selectedViewModel;
            set
            {
                selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        public ViewModelBase()
        {
            NavigateToViewCommand = new RelayCommand(NavigateToViewName);
        }

        /// <summary>
        /// Separate method for navigating from command
        /// </summary>
        /// <param name="viewName"></param>
        private void NavigateToViewName(object viewName)
        {
            Enum.TryParse(viewName.ToString(), out Views parsedViewName);
            NavigateToView(parsedViewName);
        }

        /// <summary>
        /// Navigate to view with the option of sending parameters
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="parameter">Optional parameters that can be required to create a ViewModel</param>
        public void NavigateToView(Views viewName, object parameter = null)
        {
            switch (viewName)
            {
                case Views.Home:
                    SelectedViewModel = new MainWindowViewModel();
                    break;
                case Views.LandlordList:
                    SelectedViewModel = new LandlordListViewModel();
                    break;
                case Views.RoomList:
                    SelectedViewModel = new RoomsListViewModel();
                    break;
                case Views.LocationList:
                    SelectedViewModel = new LocationListViewModel();
                    break;
                case Views.ReservationList:
                    SelectedViewModel = new ReservationListViewModel();
                    break;
                case Views.CustomerList:
                    SelectedViewModel = new CustomerListViewModel();
                    break;
                // TODO: nieuwe view models voor navigatie
                default:
                    break;
            }
        }
    }
}

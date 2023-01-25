using Air_BnB.Models;
using Air_BnB.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Air_BnB.ViewModel
{
    public class LocationListViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();

        /// <summary>
        /// Collection of all locations
        /// </summary>
        private ObservableCollection<Location> _locations;
        public ObservableCollection<Location> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Currently selected location for editing
        /// </summary>
        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// View for the selected locaction
        /// </summary>
        private LocationDetailsViewModel _selectedLocationViewModel;
        public LocationDetailsViewModel SelectedLocationViewModel
        {
            get => _selectedLocationViewModel;
            set
            {
                _selectedLocationViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command for selecting a location from the location list
        /// </summary>
        public ICommand OpenSelectedLocationCommand { get; set; }

        public LocationListViewModel()
        {
            // Retrieve the list of locations from the database
            RetrieveLocationList();

            SelectedLocationViewModel = new LocationDetailsViewModel(null);
            SelectedLocationViewModel.LocationUpdated += SelectedLocationViewModel_LocationUpdated;

            OpenSelectedLocationCommand = new RelayCommand(OpenSelectedLocation);
        }

        /// <summary>
        /// Retrieving a list of all locations from the database and updating the observablecollection
        /// </summary>
        private void RetrieveLocationList()
        {
            Locations = new ObservableCollection<Location>(Db.Locations.Include(location => location.Rooms).ToList());
        }

        /// <summary>
        /// Open the edit/create view for the selected Location
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenSelectedLocation(object parameter)
        {
            SelectedLocationViewModel = new LocationDetailsViewModel(SelectedLocation);
            SelectedLocationViewModel.LocationUpdated += SelectedLocationViewModel_LocationUpdated;
        }

        /// <summary>
        /// Event handler that updates the list of locations after changes
        /// </summary>
        /// <param name="sender">The object that publishes the event</param>
        /// <param name="e">Optional event arguments</param>
        private void SelectedLocationViewModel_LocationUpdated(object sender, EventArgs e)
        {
            RetrieveLocationList();
        }
    }
}

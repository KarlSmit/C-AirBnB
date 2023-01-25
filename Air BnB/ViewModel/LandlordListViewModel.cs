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
    /// <summary>
    /// Handles the showing of landlords and directing to the create/edit form
    /// </summary>
    public class LandlordListViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();

        /// <summary>
        /// Collection of all landlords
        /// </summary>
        private ObservableCollection<Landlord> _landlords;
        public ObservableCollection<Landlord> Landlords
        {
            get => _landlords;
            set
            {
                _landlords = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Currently selected landlord for editing
        /// </summary>
        private Landlord _selectedLandlord;
        public Landlord SelectedLandlord
        {
            get => _selectedLandlord;
            set
            {
                _selectedLandlord = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// View for the selected landlord
        /// </summary>
        private LandlordDetailsViewModel _selectedLandlordViewModel;
        public LandlordDetailsViewModel SelectedLandlordViewModel
        {
            get => _selectedLandlordViewModel;
            set
            {
                _selectedLandlordViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command for selecting a landlord from the landlord list
        /// </summary>
        public ICommand OpenSelectedLandlordCommand { get; set; }

        public LandlordListViewModel()
        {
            // Retrieve the list of landlords from the database
            RetrieveLandlordList();
            
            SelectedLandlordViewModel = new LandlordDetailsViewModel(null);
            SelectedLandlordViewModel.LandlordUpdated += SelectedLandlordViewModel_LandlordUpdated;

            OpenSelectedLandlordCommand = new RelayCommand(OpenSelectedLandlord);
        }

        /// <summary>
        /// Retrieving a list of all landlords from the database and updating the observablecollection
        /// </summary>
        private void RetrieveLandlordList()
        {
            Landlords = new ObservableCollection<Landlord>(Db.Landlords.Include(landlord => landlord.Rooms).ToList());
        }

        /// <summary>
        /// Open the edit/create view for the selected Landlord
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenSelectedLandlord(object parameter)
        {
            SelectedLandlordViewModel = new LandlordDetailsViewModel(SelectedLandlord);
            SelectedLandlordViewModel.LandlordUpdated += SelectedLandlordViewModel_LandlordUpdated;
        }

        /// <summary>
        /// Event handler that updates the list of landlords after changes
        /// </summary>
        /// <param name="sender">The object that publishes the event</param>
        /// <param name="e">Optional event arguments</param>
        private void SelectedLandlordViewModel_LandlordUpdated(object sender, EventArgs e)
        {
            RetrieveLandlordList();
        }
    }
}

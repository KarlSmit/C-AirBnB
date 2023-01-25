using Air_BnB.Models;
using Air_BnB.Utils;
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
    public class LocationDetailsViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();
        /// <summary>
        /// The location used in the view form
        /// </summary>
        private Location _location;
        public Location Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        private Visibility _textBoxVisibility;
        public Visibility TextBoxVisibility
        {
            get => _textBoxVisibility;
            set
            {
                _textBoxVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _textBlockVisibility;
        public Visibility TextBlockVisibility
        {
            get => _textBlockVisibility;
            set
            {
                _textBlockVisibility = value;
                OnPropertyChanged();
            }
        }

        // Command for starting the creation of a location
        public ICommand CreateLocationCommand { get; set; }

        // Command for starting the editing of a location
        public ICommand EditLocationCommand { get; set; }

        // Command for deleting a location
        public ICommand DeleteLocationCommand { get; set; }

        // Command for creating/updating a location
        public ICommand UpdateLocationCommand { get; set; }

        /// <summary>
        /// Event for publishing when a location gets updated
        /// </summary>
        public event EventHandler LocationUpdated;

        public LocationDetailsViewModel(Location location)
        {
            Location = location;

            _textBlockVisibility = Visibility.Visible;
            _textBoxVisibility = Visibility.Collapsed;

            CreateLocationCommand = new RelayCommand(CreateLocation);
            EditLocationCommand = new RelayCommand(EditLocation);
            UpdateLocationCommand = new RelayCommand(UpdateLocation);
            DeleteLocationCommand = new RelayCommand(DeleteLocation);
        }

        /// <summary>
        /// Creates a new location and inverses the textbox/textblock visibility
        /// </summary>
        /// <param name="obj"></param>
        private void CreateLocation(object obj)
        {
            InverseTextVisibility();

            Location = new Location();
        }

        /// <summary>
        /// Edit the selected location from the locations list
        /// </summary>
        /// <param name="id"></param>
        private void EditLocation(object id)
        {
            if (id == null || (int)id == 0)
            {
                return;
            }

            InverseTextVisibility();
        }

        /// <summary>
        /// Create or update a location
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateLocation(object obj)
        {
            // We're not editing if textbox is not visible
            if (_textBoxVisibility == Visibility.Collapsed)
            {
                return;
            }

            // Creating a location
            if (Location.Id == 0)
            {
                Db.Locations.Add(Location);
                Db.SaveChanges();
            }
            else // Updating Location
            {
                // Find location and update
                Db.Locations.Update(Location);
                Db.SaveChanges();
            }

            // Change textBoxes back to textBlocks
            InverseTextVisibility();

            // Update the location list with the newly changed locations
            UpdateLocationList();
        }


        /// <summary>
        /// Delete a location
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteLocation(object id)
        {
          
            if (id == null || (int)id == 0)
            {
                return;
            }
            using (var context = Db)
            {
                context.Locations.Remove(Location);
                context.SaveChanges();
            }

            Location = null;

            UpdateLocationList();
        }

        /// <summary>
        /// Publish the event for updating the location list
        /// </summary>
        private void UpdateLocationList()
        {
            LocationUpdated?.Invoke(this, null);
        }

        /// <summary>
        /// Helper function for inversing the Textbox/Textblock visibility
        /// </summary>
        private void InverseTextVisibility()
        {
            if (_textBlockVisibility == Visibility.Visible)
            {
                TextBlockVisibility = Visibility.Collapsed;
                TextBoxVisibility = Visibility.Visible;
            }
            else
            {
                TextBlockVisibility = Visibility.Visible;
                TextBoxVisibility = Visibility.Collapsed;
            }
        }
    }
}


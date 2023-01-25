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
    /// <summary>
    /// Handles updating/adding landlords
    /// </summary>
    public class LandlordDetailsViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();
        /// <summary>
        /// The landlord used in the view form
        /// </summary>
        private Landlord _landlord;
        public Landlord Landlord 
        { 
            get => _landlord;
            set
            {
                _landlord = value;
                OnPropertyChanged();
            }
        }

        // For toggling the visibility of the textboxes
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

        // For toggling the visibility of the textblocks
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

        // Command for starting the creation of a landlord
        public ICommand CreateLandlordCommand { get; set; }
        
        // Command for starting the editing of a landlord
        public ICommand EditLandlordCommand { get; set; }
        
        // Command for deleting a landlord
        public ICommand DeleteLandlordCommand { get; set; }

        // Command for creating/updating a landlord
        public ICommand UpdateLandlordCommand { get; set; }

        /// <summary>
        /// Event for publishing when a landlord gets updated
        /// </summary>
        public event EventHandler LandlordUpdated;

        public LandlordDetailsViewModel(Landlord landlord)
        {
            Landlord = landlord;

            _textBlockVisibility = Visibility.Visible;
            _textBoxVisibility = Visibility.Collapsed;

            CreateLandlordCommand = new RelayCommand(CreateLandLord);
            EditLandlordCommand = new RelayCommand(EditLandlord);
            UpdateLandlordCommand = new RelayCommand(UpdateLandLord);
            DeleteLandlordCommand = new RelayCommand(DeleteLandlord);
        }

        /// <summary>
        /// Creates a new landlord and inverses the textbox/textblock visibility
        /// </summary>
        /// <param name="obj"></param>
        private void CreateLandLord(object obj)
        {
            InverseTextVisibility();

            Landlord = new Landlord();
        }

        /// <summary>
        /// Edit the selected landlord from the landlords list
        /// </summary>
        /// <param name="id"></param>
        private void EditLandlord(object id)
        {
            if (id == null || (int)id == 0)
            {
                return;
            }

            InverseTextVisibility();
        }

        /// <summary>
        /// Create or update a landlord
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateLandLord(object obj)
        {
            // We're not editing if textbox is not visible
            if (_textBoxVisibility == Visibility.Collapsed)
            {
                return;
            }

            // Creating a landlord
            if (Landlord.Id == 0)
            {
                using (var context = Db)
                {
                    Db.Landlords.Add(Landlord);
                    Db.SaveChanges();
                }

            } 
            else // Updating Landlord
            {
                // Find landlord and update
                Db.Landlords.Update(Landlord);
                Db.SaveChanges();
            }

            // Change textBoxes back to textBlocks
            InverseTextVisibility();

            // Update the landlord list with the newly changed landlords
            UpdateLandlordList();
        }


        /// <summary>
        /// Delete a landlord
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteLandlord(object id)
        {
            if (Landlord.Rooms.Count > 0)
            {
                MessageBox.Show("Deze Landlord is eigenaar van een Room");
                return;
            }
            if (id == null || (int)id == 0)
            {
                return;
            }
            using (var context = Db)
            {
                context.Landlords.Remove(Landlord);
                context.SaveChanges();
            }

            Landlord = null;

            UpdateLandlordList();
        } 

        /// <summary>
        /// Publish the event for updating the landlord list
        /// </summary>
        private void UpdateLandlordList()
        {
            LandlordUpdated?.Invoke(this, null);
        }

        /// <summary>
        /// Helper function for inversing the Textbox/Textblock visibility
        /// </summary>
        private void InverseTextVisibility()
        {
            if(_textBlockVisibility == Visibility.Visible)
            {
                TextBlockVisibility = Visibility.Collapsed;
                TextBoxVisibility = Visibility.Visible;
            } else
            {
                TextBlockVisibility = Visibility.Visible;
                TextBoxVisibility = Visibility.Collapsed;
            }
        }
    }
}

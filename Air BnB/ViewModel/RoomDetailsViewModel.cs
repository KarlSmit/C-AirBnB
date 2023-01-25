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
    public class RoomDetailsViewModel : BindableBase
    {
        private AirBnBContext Db;

        private Room _room;
        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
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

        private bool _isLandlordComboboxEnabled;
        public bool IsLandlordComboboxEnabled
        {
            get => _isLandlordComboboxEnabled;
            set
            {
                _isLandlordComboboxEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isLocationComboboxEnabled;
        public bool IsLocationComboboxEnabled
        {
            get => _isLocationComboboxEnabled;
            set
            {
                _isLocationComboboxEnabled = value;
                OnPropertyChanged();
            }
        }

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

        // Command for starting the creation of a room
        public ICommand CreateRoomCommand { get; set; }

        // Command for starting the editing of a room
        public ICommand EditRoomCommand { get; set; }

        // Command for creating/updating a room
        public ICommand UpdateRoomCommand { get; set; }

        // Command for deleting a room
        public ICommand DeleteRoomCommand { get; set; }

        /// <summary>
        /// Event for publishing when a room gets updated
        /// </summary>
        public event EventHandler RoomUpdated;

        public RoomDetailsViewModel(Room room, AirBnBContext db)
        {
            Db = db;
            Landlords = new ObservableCollection<Landlord>(Db.Landlords.ToList());
            Locations = new ObservableCollection<Location>(Db.Locations.ToList());

            Room = room;

            _textBlockVisibility = Visibility.Visible;
            _textBoxVisibility = Visibility.Collapsed;

            _isLandlordComboboxEnabled = false;
            _isLocationComboboxEnabled = false;

            CreateRoomCommand = new RelayCommand(CreateRoom);
            EditRoomCommand = new RelayCommand(EditRoom);
            UpdateRoomCommand = new RelayCommand(UpdateRoom);
            DeleteRoomCommand = new RelayCommand(DeleteRoom);
        }

        /// <summary>
        /// Creates a new Room and inverses the textbox/textblock visibility
        /// </summary>
        /// <param name="obj"></param>
        private void CreateRoom(object obj)
        {
            InverseTextVisibility();

            Room = new Room();
        }

        /// <summary>
        /// Edit the selected room from the rooms list
        /// </summary>
        /// <param name="id"></param>
        private void EditRoom(object id)
        {
            if (id == null || (int)id == 0)
            {
                return;
            }

            InverseTextVisibility();
        }

        /// <summary>
        /// Create or update a room
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateRoom(object obj)
        {
            // We're not editing if textbox is not visible
            if (_textBoxVisibility == Visibility.Collapsed)
            {
                return;
            }

            // requires you to fill in a Landlord
            if (Room.OwnedBy == null)
            {
                MessageBox.Show("Er is geen Landlord toegevoegd.");

                return;
            }

            // requires you to fill in a Location
            if (Room.Location == null)
            {
                MessageBox.Show("Er is geen Location toegevoegd.");

                return;
            }

            // Creating a room
            if (Room.Id == 0)
            {
                Db.Rooms.Add(Room);

                Db.SaveChanges();
            }
            else // Updating a room
            {
                // Find room and update

                Db.Rooms.Update(Room);

                Db.SaveChanges();
            }
         
            // Change textBoxes back to textBlocks
            InverseTextVisibility();

            // Update the room list with the newly changed rooms
            UpdateRoomList();
        }

        /// <summary>
        /// Delete a Room
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteRoom(object id)
        {
            if (id == null || (int)id == 0)
            {
                return;
            }
            Db.Rooms.Remove(Room);
            Db.SaveChanges();

            Room = null;

            UpdateRoomList();
        }

        /// <summary>
        /// Publish the event for updating the room list
        /// </summary>
        private void UpdateRoomList()
        {
            RoomUpdated?.Invoke(this, null);
        }

        private void InverseTextVisibility()
        {
            if (_textBlockVisibility == Visibility.Visible)
            {
                TextBlockVisibility = Visibility.Collapsed;
                TextBoxVisibility = Visibility.Visible;
                IsLandlordComboboxEnabled = true;
                IsLocationComboboxEnabled = true;
            }
            else
            {
                TextBlockVisibility = Visibility.Visible;
                TextBoxVisibility = Visibility.Collapsed;
                IsLandlordComboboxEnabled = false;
                IsLocationComboboxEnabled = false;

            }
        }
    }
}

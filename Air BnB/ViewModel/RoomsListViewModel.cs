using Air_BnB.Models;
using Air_BnB.Utils;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace Air_BnB.ViewModel
{
    class RoomsListViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();

        /// <summary>
        /// Collection containing all the rooms
        /// </summary>
        private ObservableCollection<Room> _rooms;
        public ObservableCollection<Room> Rooms 
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Currently selected room for editing
        /// </summary>
        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// View for the selected Room
        /// </summary>
        private RoomDetailsViewModel _selectedRoomViewModel;
        public RoomDetailsViewModel SelectedRoomViewModel
        {
            get => _selectedRoomViewModel;
            set
            {
                _selectedRoomViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command for selecting a room from the room list
        /// </summary>
        public ICommand OpenSelectedRoomCommand { get; set; }

        public RoomsListViewModel()
        {
            RetrieveRooms();

            SelectedRoomViewModel = new RoomDetailsViewModel(null, Db);
            SelectedRoomViewModel.RoomUpdated += SelectedRoomViewModel_RoomUpdated;

            OpenSelectedRoomCommand = new RelayCommand(OpenSelectedRoom);
        }

        /// <summary>
        /// Retrieving a list of all rooms from the database and update the observable collection
        /// </summary>
        private void RetrieveRooms()
        {
            Rooms = new ObservableCollection<Room>(Db.Rooms.Include(room => room.OwnedBy).Include(room => room.Location).ToList());
        }

        /// <summary>
        /// Open the edit/create view for the selected Room
        /// </summary>
        /// <param name="obj"></param>
        private void OpenSelectedRoom(object obj)
        {
            SelectedRoomViewModel = new RoomDetailsViewModel(SelectedRoom, Db);
            SelectedRoomViewModel.RoomUpdated += SelectedRoomViewModel_RoomUpdated;
        }

        /// <summary>
        /// Event handler that updates the list of rooms after changes
        /// </summary>
        /// <param name="sender">The object that publishes the event</param>
        /// <param name="e">Optional event arguments</param>
        private void SelectedRoomViewModel_RoomUpdated(object sender, EventArgs e)
        {
            RetrieveRooms();
        }
    }
}

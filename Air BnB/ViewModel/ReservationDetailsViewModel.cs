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
    public class ReservationDetailsViewModel : BindableBase
    {
        private AirBnBContext Db;

        private Reservation _reservation;
        public Reservation Reservation
        {
            get => _reservation;
            set
            {
                _reservation = value;
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

        private bool _areComboboxesEnabled;
        public bool AreComboboxesEnabled
        {
            get => _areComboboxesEnabled;
            set
            {
                _areComboboxesEnabled = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;

                OnPropertyChanged();
            }
        }

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

        // Command for starting the creation of a reservation
        public ICommand CreateReservationCommand { get; set; }

        // Command for starting the editing of a reservation
        public ICommand EditReservationCommand { get; set; }

        // Command for creating/updating a reservation
        public ICommand UpdateReservationCommand { get; set; }

        // Command for deleting a landlord
        public ICommand DeleteReservationCommand { get; set; }

        /// <summary>
        /// Event for publishing when a reservation gets updated
        /// </summary>
        public event EventHandler ReservationUpdated;

        public ReservationDetailsViewModel(Reservation reservation, AirBnBContext db)
        {
            Db = db;
            Customers = new ObservableCollection<Customer>(Db.Customers.ToList());
            Rooms = new ObservableCollection<Room>(Db.Rooms.ToList());

            Reservation = reservation;

            _textBlockVisibility = Visibility.Visible;
            _textBoxVisibility = Visibility.Collapsed;

            AreComboboxesEnabled = false;

            CreateReservationCommand = new RelayCommand(CreateReservation);
            EditReservationCommand = new RelayCommand(EditReservation);
            UpdateReservationCommand = new RelayCommand(UpdateReservation);
            DeleteReservationCommand = new RelayCommand(DeleteReservation);
        }

        /// <summary>
        /// Creates a new Reservation and inverses the textbox/textblock visibility
        /// </summary>
        /// <param name="obj"></param>
        private void CreateReservation(object obj)
        {
            InverseTextVisibility();

            Reservation = new Reservation();
        }

        /// <summary>
        /// Edit the selected reservation from the reservations list
        /// </summary>
        /// <param name="id"></param>
        private void EditReservation(object id)
        {
            if (id == null || (int)id == 0)
            {
                return;
            }

            InverseTextVisibility();
        }

        /// <summary>
        /// Create or update a reservation
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateReservation(object obj)
        {
            // We're not editing if textbox is not visible
            if (_textBoxVisibility == Visibility.Collapsed)
            {
                return;
            }

            // Check if the reservation is valid
            if(!IsReservationValid())
            {
                return;
            }

            // Creating a reservation
            if (Reservation.Id == 0)
            {
                Db.Reservations.Add(Reservation);

                Db.SaveChanges();
            }
            else // Updating a reservation
            {
                // Find reservation and update

                Db.Reservations.Update(Reservation);

                Db.SaveChanges();
            }

            // Change textBoxes back to textBlocks
            InverseTextVisibility();

            // Update the reservation list with the newly changed reservations
            UpdateReservationList();
        }

        private bool IsReservationValid()
        {
            // requires you to fill in a Landlord
            if (Reservation.Room == null || Reservation.Customer == null)
            {
                MessageBox.Show("Er is geen room of customer toegevoegd.");

                return false;
            }

            if(Reservation.StartDate > Reservation.EndDate)
            {
                MessageBox.Show("De aankomst datum kan niet later zijn dan de vertrekdatum.");

                return false;
            }

            return true;
        }

        /// <summary>
        /// Delete a Reservation
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteReservation(object id)
        {
            if (id == null || (int)id == 0)
            {
                return;
            }
            Db.Reservations.Remove(Reservation);
            Db.SaveChanges();

            Reservation = null;

            UpdateReservationList();
        }

        /// <summary>
        /// Publish the event for updating the reservation list
        /// </summary>
        private void UpdateReservationList()
        {
            ReservationUpdated?.Invoke(this, null);
        }

        private void InverseTextVisibility()
        {
            if (_textBlockVisibility == Visibility.Visible)
            {
                TextBlockVisibility = Visibility.Collapsed;
                TextBoxVisibility = Visibility.Visible;
                AreComboboxesEnabled = true;
            }
            else
            {
                TextBlockVisibility = Visibility.Visible;
                TextBoxVisibility = Visibility.Collapsed;
                AreComboboxesEnabled = false;
            }
        }


    }
}

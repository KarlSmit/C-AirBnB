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
    class ReservationListViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();

        /// <summary>
        /// Collection containing all the reservations
        /// </summary>
        private ObservableCollection<Reservation> _reservations;
        public ObservableCollection<Reservation> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Currently selected reservation for editing
        /// </summary>
        private Reservation _selectedReservation;
        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// View for the selected Reservation
        /// </summary>
        private ReservationDetailsViewModel _selectedReservationViewModel;
        public ReservationDetailsViewModel SelectedReservationViewModel
        {
            get => _selectedReservationViewModel;
            set
            {
                _selectedReservationViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command for selecting a reservation from the reservation list
        /// </summary>
        public ICommand OpenSelectedReservationCommand { get; set; }

        public ReservationListViewModel()
        {
            RetrieveReservations();

            SelectedReservationViewModel = new ReservationDetailsViewModel(null, Db);
            SelectedReservationViewModel.ReservationUpdated += SelectedReservationViewModel_ReservationUpdated;

            OpenSelectedReservationCommand = new RelayCommand(OpenSelectedReservation);
        }

        /// <summary>
        /// Retrieving a list of all reservations from the database and update the observable collection
        /// </summary>
        private void RetrieveReservations()
        {
            Db.Reservations.Include(reservation => reservation.Room).Include(reservation2 => reservation2.Customer).Load();
            Reservations = Db.Reservations.Local.ToObservableCollection(); //new ObservableCollection<Reservation>(Db.Reservations.Include(reservation => reservation.Room).Include(reservation2 => reservation2.Customer)
        }

        /// <summary>
        /// Open the edit/create view for the selected Reservation
        /// </summary>
        /// <param name="obj"></param>
        private void OpenSelectedReservation(object obj)
        {
            SelectedReservationViewModel = new ReservationDetailsViewModel(SelectedReservation, Db);
            SelectedReservationViewModel.ReservationUpdated += SelectedReservationViewModel_ReservationUpdated;
        }

        /// <summary>
        /// Event handler that updates the list of reservations after changes
        /// </summary>
        /// <param name="sender">The object that publishes the event</param>
        /// <param name="e">Optional event arguments</param>
        private void SelectedReservationViewModel_ReservationUpdated(object sender, EventArgs e)
        {
            RetrieveReservations();
        }
    }
}

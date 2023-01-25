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
    /// Handles the showing of customers and directing to the create/edit form
    /// </summary>
    class CustomerListViewModel : ViewModelBase
    {
        private AirBnBContext Db = new AirBnBContext();

        /// <summary>
        /// Collection containing all the customers
        /// </summary>
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

        /// <summary>
        /// Currently selected customer for editing
        /// </summary>
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// View for the selected Customer
        /// </summary>
        private CustomerDetailsViewModel _selectedCustomerViewModel;
        public CustomerDetailsViewModel SelectedCustomerViewModel
        {
            get => _selectedCustomerViewModel;
            set
            {
                _selectedCustomerViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command for selecting a customer from the customer list
        /// </summary>
        public ICommand OpenSelectedCustomerCommand { get; set; }

        public CustomerListViewModel()
        {
            // Retrieve the list of customers from the database
            RetrieveCustomersList();

            SelectedCustomerViewModel = new CustomerDetailsViewModel(null);
            SelectedCustomerViewModel.CustomerUpdated += SelectedCustomerViewModel_CustomerUpdated;

            OpenSelectedCustomerCommand = new RelayCommand(OpenSelectedCustomer);
        }

        /// <summary>
        /// Retrieving a list of all customers from the database and update the observable collection
        /// </summary>
        private void RetrieveCustomersList()
        {
            Customers = new ObservableCollection<Customer>(Db.Customers.Include(customer => customer.Reservations).ToList());
        }

        /// <summary>
        /// Open the edit/create view for the selected Customer
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenSelectedCustomer(object parameter)
        {
            SelectedCustomerViewModel = new CustomerDetailsViewModel(SelectedCustomer);
            SelectedCustomerViewModel.CustomerUpdated += SelectedCustomerViewModel_CustomerUpdated;
        }

        /// <summary>
        /// Event handler that updates the list of customers after changes
        /// </summary>
        /// <param name="sender">The object that publishes the event</param>
        /// <param name="e">Optional event arguments</param>
        private void SelectedCustomerViewModel_CustomerUpdated(object sender, EventArgs e)
        {
            RetrieveCustomersList();
        }
    }
}

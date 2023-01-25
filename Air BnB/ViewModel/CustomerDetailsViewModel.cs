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
    /// Handles updating/adding customers
    /// </summary>
    class CustomerDetailsViewModel : BindableBase
    {
        private AirBnBContext Db = new AirBnBContext();
        /// <summary>
        /// The customer used in the view form
        /// </summary>
        private Customer _customer;
        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged();
            }
        }

        // For toggling the visibility of the textbox
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

        // For toggling the visibility of the textblock
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

        // Command for starting the creation of a customer
        public ICommand CreateCustomerCommand { get; set; }

        // Command for starting the editing of a customer
        public ICommand EditCustomerCommand { get; set; }

        // Command for creating/updating a customer
        public ICommand UpdateCustomerCommand { get; set; }

        // Command for deleting a customer
        public ICommand DeleteCustomerCommand { get; set; }

        /// <summary>
        /// Event for publishing when a customer gets updated
        /// </summary>
        public event EventHandler CustomerUpdated;

        public CustomerDetailsViewModel(Customer customer)
        {
            Customer = customer;

            _textBlockVisibility = Visibility.Visible;
            _textBoxVisibility = Visibility.Collapsed;

            CreateCustomerCommand = new RelayCommand(CreateCustomer);
            EditCustomerCommand = new RelayCommand(EditCustomer);
            UpdateCustomerCommand = new RelayCommand(UpdateCustomer);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer);
        }

        /// <summary>
        /// Creates a new Customer and inverses the textbox/textblock visibility
        /// </summary>
        /// <param name="obj"></param>
        private void CreateCustomer(object obj)
        {
            InverseTextVisibility();

            Customer = new Customer();
        }

        /// <summary>
        /// Edit the selected customer from the customers list
        /// </summary>
        /// <param name="id"></param>
        private void EditCustomer(object id)
        {
            if (id == null || (int)id == 0)
            {
                return;
            }

            InverseTextVisibility();
        }

        /// <summary>
        /// Create or update a customer
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateCustomer(object obj)
        {
            // We're not editing if textbox is not visible
            if (_textBoxVisibility == Visibility.Collapsed)
            {
                return;
            }

            // Creating a customer
            if (Customer.Id == 0)
            {
                Db.Customers.Add(Customer);
                Db.SaveChanges();
            }
            else // Updating a customer
            {
                // Find customer and update
                Db.Customers.Update(Customer);
                Db.SaveChanges();
            }
            // Change textBoxes back to textBlocks
            InverseTextVisibility();

            // Update the customer list with the newly changed customers
            UpdateCustomerList();
        }

        /// <summary>
        /// Delete a Customer
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteCustomer(object id)
        {
            if (Customer.Reservations.Count > 0)
            {
                MessageBox.Show("Deze Customer is gekoppeld aan een Reservation");
                return;
            }
            if (id == null || (int)id == 0)
            {
                return;
            }
            Db.Customers.Remove(Customer);
            Db.SaveChanges();

            Customer = null;

            UpdateCustomerList();
        }

        /// <summary>
        /// Publish the event for updating the customer list
        /// </summary>
        private void UpdateCustomerList()
        {
            CustomerUpdated?.Invoke(this, null);
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

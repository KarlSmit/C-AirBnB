using Air_BnB.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_BnB.Models
{
    /// <summary>
    /// A customer who can make a reservation
    /// </summary>
    public class Customer : BindableBase
    {
        /// <summary>
        /// The id of a customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of the customer
        /// </summary>
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The last name of the customer
        /// </summary>
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The passport number of the customer to keep track of his identity
        /// </summary>
        private string _passportNumber;
        public string PassportNumber
        {
            get { return _passportNumber; }
            set
            {
                _passportNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The name of the city where the customer lives
        /// </summary>
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The streetname where the customer lives
        /// </summary>
        private string _streetName;
        public string StreetName
        {
            get { return _streetName; }
            set
            {
                _streetName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The street number of where the customer lives
        /// </summary>
        private int _streetNumber;
        public int StreetNumber
        {
            get { return _streetNumber; }
            set
            {
                _streetNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The postal code of the customer
        /// </summary>
        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Full name for showing inside the view
        /// </summary>
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

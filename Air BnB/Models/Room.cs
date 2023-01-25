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
    /// A room containing all the details
    /// </summary>
    public class Room : BindableBase
    {
        /// <summary>
        /// Id of this room, we need id because of Microsoft.EntityFrameworkCore.Sqlserver tool
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the room for easier selection when making a reservation
        /// </summary>
        private string _name;
        public string Name 
        {
            get => _name; 
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Street name of this room
        /// </summary>
        private string _streetName;
        public string StreetName 
        {
            get => _streetName;
            set
            {
                _streetName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Street number of this room
        /// </summary>
        private int _streetNumber;
        public int StreetNumber
        {
            get => _streetNumber;
            set
            {
                _streetNumber = value;
                OnPropertyChanged();
            } 
        }

        /// <summary>
        /// Postal code of this room
        /// </summary>
        private string _postalCode;
        public string PostalCode 
        {
            get => _postalCode; 
            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The price of the room
        /// </summary>
        private double _price;
        public double Price 
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            } 
        }

        /// <summary>
        /// The amount of persons who are allowed to be in this room
        /// </summary>
        private int _personAmount;
        public int PersonAmount 
        {
            get => _personAmount; 
            set
            {
                _personAmount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The person who booked the room 
        /// </summary>
        private Location _location;
        public virtual Location Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The person who booked the room 
        /// </summary>
        private Landlord _ownedBy;
        public virtual Landlord OwnedBy
        {
            get => _ownedBy;
            set
            {
                _ownedBy = value;
                OnPropertyChanged();
            }
        }
    }
}

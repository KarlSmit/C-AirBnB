using Air_BnB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_BnB.Models
{
    /// <summary>
    /// A reservation for keeping track of customers, the room they booked and the start and end date of their reservation.
    /// </summary>
    public class Reservation : BindableBase
    {
        /// <summary>
        /// The id of this reservation
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The start date of the reservation
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The end date of the reservation
        /// </summary>
        public DateTime EndDate { get; set; } = DateTime.Now;

        /// <summary>
        /// The person who booked the room 
        /// </summary>
        private Room _room;
        public virtual Room Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged();
            }
        }
        public int RoomId { get; set; }

        /// <summary>
        /// The person who booked the room 
        /// </summary>
        private Customer _customer;
        public virtual Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged();
            }
        }
        public int CustomerId { get; set; }
        
    }
}

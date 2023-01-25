using Air_BnB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_BnB.Models
{
    /// <summary>
    /// Properties of a landlord
    /// </summary>
    public class Landlord : BindableBase
    {
        /// <summary>
        /// Unique identifier of a landlord
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of a landlord
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
        /// Last name of a landlord
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
        /// Full name for showing inside the view
        /// </summary>
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}

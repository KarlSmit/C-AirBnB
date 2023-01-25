using Air_BnB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Air_BnB.Models 
{
    /// <summary>
    /// The location of a room
    /// </summary>
    public class Location : BindableBase
    {
        /// <summary>
        /// Id of a location
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of location
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}

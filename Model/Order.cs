using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientsAndOrders
{
    public class Order :  INotifyPropertyChanged, ICloneable
    {
        public int id;
        public string description;
        public string owner;


        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged("Owner");
            }
        }

        public object Clone()
        {
            return new Order { Description = this.Description, ID = this.ID, Owner = this.Owner };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

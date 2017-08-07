using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientsAndOrders
{
    public class Client : INotifyPropertyChanged, ICloneable
    {
        private string name;
        private string address;
        private bool vip;

        public string Name 
        { 
            get {return name;}
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            } 
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public bool VIP
        {
            get { return vip; }
            set
            {
                vip = value;
                OnPropertyChanged("VIP");
            }
        }

        public object Clone()
        {
            return new Client { Name = this.Name, Address = this.Address, VIP = this.VIP };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

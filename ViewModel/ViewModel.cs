using System;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClientsAndOrders
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Client selectedClient;
        private Order selectedOrder;
        private Client newClient;
        private Order newOrder;

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Order> OrdersForView { get; set; }
        public Client SelectedClient
        {
            get { return selectedClient; }
            set 
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        public Client NewClient
        {
            get { return newClient; }
            set
            {
                newClient = value;
                OnPropertyChanged("NewClient");
            }
        }

        public Order NewOrder
        {
            get { return newOrder; }
            set
            {
                newOrder = value;
                OnPropertyChanged("NewOrder");
            }
        }

        private Command delClientCommand;
        public Command DelClientCommand
        {
            get
            {
                return delClientCommand ??
                  (delClientCommand = new Command(obj =>
                  {
                      Client client = obj as Client;
                      if (client != null)
                      {
                          Clients.Remove(client);
                      }
                  }));
            }
        }

        private Command editClientCommand;
        public Command EditClientCommand
        {
            get
            {
                return editClientCommand ??
                  (editClientCommand = new Command(obj =>
                  {
                      
                          for (int i=0;i<Clients.Count;i++)
                          {
                              if (SelectedClient!=null && Clients[i].Name == SelectedClient.Name)
                              {
                                  if (NewClient.Name != "" && NewClient.Address != "") 
                                      Clients[i] = (Client)NewClient.Clone();
                              }
                          }
                  }));
            }
        }
          
        private Command addClientCommand;
        public Command AddClientCommand
        {
            get
            {
                return addClientCommand ??
                  (addClientCommand = new Command(obj =>
                  {
                      if (NewClient!=null && NewClient.Name != "" && NewClient.Address != "")
                        Clients.Add(NewClient);
                  }));
            }
        }

        private Command delOrderCommand;
        public Command DelOrderCommand
        {
            get
            {
                return delOrderCommand ??
                  (delOrderCommand = new Command(obj =>
                  {
                      Order order = obj as Order;
                      if (order != null)
                      {
                          OrdersForView.Remove(order);
                      }
                  }));
            }
        }

        private Command addOrderCommand;
        public Command AddOrderCommand
        {
            get
            {
                return addOrderCommand ??
                  (addOrderCommand = new Command(obj =>
                  {
                      if (NewOrder != null &&
                      NewOrder.Description != "" &&
                      NewOrder.ID != null &&
                      checkID(NewOrder.ID) &&
                      NewOrder.Owner != "")
                      {
                          Orders.Add(NewOrder);
                          //build orders
                          FillOrderForViewCollection(OrdersForView, Orders, SelectedClient);
                      }
                  }));
            }
        }

        private Command editOrderCommand;
        public Command EditOrderCommand
        {
            get
            {
                return editOrderCommand ??
                  (editOrderCommand = new Command(obj =>
                  {
                      for (int i = 0; i < Orders.Count; i++)
                      {
                          if (SelectedOrder != null && Orders[i].ID == SelectedOrder.ID)
                          {
                              if (NewOrder.ID != null &&
                                    (checkID(NewOrder.ID) || NewOrder.ID == Orders[i].ID) &&
                                    NewOrder.Description != "" &&
                                    NewOrder.Owner != "" &&
                                    isClientExist(NewOrder.Owner))
                              {
                                  Orders[i] = (Order)NewOrder.Clone();
                                  //build orders
                                  FillOrderForViewCollection(OrdersForView, Orders, SelectedClient);
                              }
                          }
                      }

                  }));
            }
        }


        public ViewModel()
        {
            Clients = new ObservableCollection<Client>
            {
                new Client  { Name="McDonalds", Address="USA", VIP=true },
                new Client  { Name="Starbucks", Address="USA", VIP=false },
                new Client  { Name="Sausalitos", Address="Germany", VIP=true },
                new Client  { Name="KFC", Address="USA", VIP=false }
            };

            Orders = new ObservableCollection<Order>
            {
                new Order { Description = "Paper", ID = 3426, Owner = "McDonalds" },
                new Order { Description = "Forks", ID = 3423, Owner = "McDonalds" },
                new Order { Description = "Boxes", ID = 3424, Owner = "Sausalitos" }
            };

            OrdersForView = new ObservableCollection<Order>();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));

            if (prop == "SelectedClient")
            {
                //build orders
                FillOrderForViewCollection(OrdersForView, Orders, SelectedClient);

                //copy to NewClient
                if (this.selectedClient!= null)
                    NewClient = (Client)SelectedClient.Clone();
            }

            if (prop == "SelectedOrder")
            {
                if (this.selectedOrder != null)
                    NewOrder = (Order)SelectedOrder.Clone();
            }
        }


        private bool checkID(int ID)
        {
            bool isValueExist = true;
            foreach (Order a in Orders)
            {
                if (a.ID == ID)
                    isValueExist = false;
            }
            return isValueExist;
        }

        private bool isClientExist(string clientName)
        {
            foreach (Client a in Clients)
            {
                if (a.Name == clientName)
                    return true;
            }
            return false;
        }

        private void FillOrderForViewCollection(ObservableCollection<Order> ordersForView, ObservableCollection<Order> orders, Client _selectedClient)
        {
            ordersForView.Clear();
            foreach (Order a in orders)
            {
                if (_selectedClient != null && _selectedClient.Name == a.Owner)
                {
                    ordersForView.Add(a);
                }
            }
        }
    }
}

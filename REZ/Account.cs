using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace REZ
{
    public class Account : INotifyPropertyChanged
    {
        private string name;
        public string cpf;
        private List<Product> itemsList;
        private double totalPrice = 0.0;
        private bool isEnabled = true;
        private bool isSelected;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled= value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public List<Product> ItemsList 
        {
            get { return itemsList; }
            set { itemsList = value; }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string CPF
        {
            get { return cpf; }
            set { cpf = value; }
        }

        //----------------------------------------------------------------------------
        public Account(string name, string cpf)
        {
            itemsList = new List<Product> { };
            Name = name;
            CPF = cpf;
            AccountsList.SelectedAccount = this;
        }

        //----------------------------------------------------------------------------
        public void AddItem(Product item)
        {
            ItemsList.Add(item);
            totalPrice += item.Price;
        }

        //----------------------------------------------------------------------------
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}


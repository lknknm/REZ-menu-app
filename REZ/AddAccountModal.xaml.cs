using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace REZ
{
    /// <summary>
    /// Modal Page for account creation with Input Validation
    /// </summary>
    public sealed partial class AddAccountModal : Page
    {
        private ContentDialog dialog;
        public AddAccountModal() { }

        private bool IsNameInputValid = false;
        private bool IsCPFInputValid = false;

        //----------------------------------------------------------------------------
        public AddAccountModal(ContentDialog dialog)
        {
            this.InitializeComponent();
            this.dialog = dialog;
            InputsValidation();

            DataContext = this;
        }

        //----------------------------------------------------------------------------
        public Account CreateNewAccount()
        {
            Account newAccount = new Account(Name.Text, CPF.Text);
            return newAccount;
        }

        //----------------------------------------------------------------------------
        private void InputsValidation()
        {
            dialog.IsPrimaryButtonEnabled = false;
            if (IsNameInputValid == true && IsCPFInputValid == true)
                dialog.IsPrimaryButtonEnabled = true;
            else
                dialog.IsPrimaryButtonEnabled = false;
        }

        //----------------------------------------------------------------------------
        // This is a workaround but it's good for now.
        // At least we can have a small input validation and can improve upon this.
        // Maybe later we can add the INotifyPropertyChanged interface.
        private void InputName_TextChange(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Zа]{1,20}$");
            if (String.IsNullOrEmpty(Name.Text))
            {
                ErrorMessage_Name.Visibility = Visibility.Visible;
                ErrorMessage_Name.Text = Name.Text + "Por favor digite um nome.";
                IsNameInputValid = false;
            }
            else if (!regex.IsMatch(Name.Text))
            {
                ErrorMessage_Name.Visibility = Visibility.Visible;
                ErrorMessage_Name.Text = Name.Text + " não é um nome válido. Por favor insira outro nome.";
                IsNameInputValid = false;
            }
            else 
            { 
                ErrorMessage_Name.Visibility = Visibility.Collapsed;
                IsNameInputValid = true;
            }
            InputsValidation();
        }

        //----------------------------------------------------------------------------
        // This is a workaround but it's good for now.
        // At least we can have a small input validation and can improve upon this.
        // Maybe later we can add the INotifyPropertyChanged interface.
        private void CPFNumber_TextChange(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$");
            if (String.IsNullOrEmpty(CPF.Text))
            {
                ErrorMessage_CPF.Visibility = Visibility.Visible;
                ErrorMessage_CPF.Text = CPF.Text + "Por favor digite um número de CPF.";
                IsCPFInputValid = false;
            }
            else if (!regex.IsMatch(CPF.Text))
            {
                ErrorMessage_CPF.Visibility = Visibility.Visible;
                ErrorMessage_CPF.Text = CPF.Text + " não é um CPF válido. Por favor insira um número de CPF.";
                IsCPFInputValid = false;
            }
            else 
            { 
                ErrorMessage_CPF.Visibility = Visibility.Collapsed;
                IsCPFInputValid = true;
            }
            InputsValidation();
        }

    }
}

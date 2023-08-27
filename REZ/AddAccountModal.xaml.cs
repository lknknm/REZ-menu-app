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
            Regex regex = new Regex("^([a-zA-ZÀ-ÿ]{3,10})+([ a-zA-ZÀ-ÿ]{0,20})$");
            if (!regex.IsMatch(Name.Text))
            {
                ErrorMessage_Name.Visibility = Visibility.Visible;
                IsNameInputValid = false;
                ErrorMessage_Name.Text = String.IsNullOrEmpty(Name.Text)
                    ? $"Por favor digite um nome."
                    : $"{Name.Text} - não e um nome valido. Por favor insira outro nome.";
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
            TextBox textBox = sender as TextBox;
            string formattedCPF = FormatCPF(textBox.Text);

            if (formattedCPF != textBox.Text)
            {
                textBox.Text = formattedCPF;
                textBox.SelectionStart = formattedCPF.Length;
            }

            if (String.IsNullOrEmpty(CPF.Text))
            {
                ErrorMessage_CPF.Visibility = Visibility.Visible;
                ErrorMessage_CPF.Text = CPF.Text + "Por favor digite um número de CPF.";
                IsCPFInputValid = false;
            }
            //else if (!ValidateCPF(CPF.Text))
            //{
            //    ErrorMessage_CPF.Visibility = Visibility.Visible;
             //   ErrorMessage_CPF.Text = CPF.Text + " não é um CPF válido. Por favor insira um número de CPF.";
            //    IsCPFInputValid = false;
            //}
            else 
            { 
                ErrorMessage_CPF.Visibility = Visibility.Collapsed;
                IsCPFInputValid = true;
            }
            InputsValidation();
        }

        //----------------------------------------------------------------------------
        // This will validate the CPF number with its specific format and logic
        private bool ValidateCPF(string cpf)
        {
            string digitsOnly = new string(cpf.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length != 11)
            {
                return false;
            }

            if (new string(digitsOnly[0], 11) == digitsOnly)
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(digitsOnly[i].ToString()) * (10 - i);
            }
            int firstDigit = (sum * 10) % 11;
            if (firstDigit == 10)
            {
                firstDigit = 0;
            }

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(digitsOnly[i].ToString()) * (11 - i);
            }
            int secondDigit = (sum * 10) % 11;
            if (secondDigit == 10)
            {
                secondDigit = 0;
            }

            return int.Parse(digitsOnly[9].ToString()) == firstDigit &&
                   int.Parse(digitsOnly[10].ToString()) == secondDigit;
        }


        //----------------------------------------------------------------------------
        // This method will autoformat user input adding dots and dash to the CPF number
        // for correct formatting.
        // By this, the user will only need to input 1234568900 and the TextBox field 
        // will live update to 123.456.789-00.
        private string FormatCPF(string cpf)
        {
            string digitsOnly = new string(cpf.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length > 11)
            {
                digitsOnly = digitsOnly.Substring(0, 11);
            }
            string formattedCPF = digitsOnly;

            if (digitsOnly.Length >= 4)
            {
                formattedCPF = formattedCPF.Insert(3, ".");
            }
            if (digitsOnly.Length >= 7)
            {
                formattedCPF = formattedCPF.Insert(7, ".");
            }
            if (digitsOnly.Length >= 10)
            {
                formattedCPF = formattedCPF.Insert(11, "-");
            }
            return formattedCPF;
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{

    public sealed partial class MainPage : Page
    {
        public AccountsList accountsList = new AccountsList();
        public AddAccountModal addAccountModal = new AddAccountModal();
        public Account NewAccount;
        public ShoppingCart shoppingCart = AccountsList.Cart;
        private string jsonString;
        public Account User = AccountsList.SelectedAccount;
        public List<Account> Accounts = AccountsList.Accounts;
        private static List<Product> products;

        private List<string> ProductsSuggestions;

        //----------------------------------------------------------------------------
        public static List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        //----------------------------------------------------------------------------
        public MainPage()
        {
            this.InitializeComponent();

            if (User != null)
            {
                UpdateUser(User);
            } else
            {
                Debug.WriteLine("[MainPage] Current account is null");
                Debug.WriteLine($"[MainPage] Accounts created: {Accounts.Count}");
            }

            var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Properties", "Products.json");
            StreamReader reader = new(jsonFilePath);
            jsonString = reader.ReadToEnd();
            Products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            ProductsSuggestions = Products.Select(p => p.Name).ToList();
        }

        //----------------------------------------------------------------------------
        private void OpenFoodMenu(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Frame.Navigate(typeof(FoodMenu), button, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        //----------------------------------------------------------------------------
        private void ShoppingCart_ButtonClick(object sender, RoutedEventArgs e)
        {
            shoppingCart.OpenShoppingModal(this, shoppingCart, ToggleThemeTeachingTip1);
        }

        //----------------------------------------------------------------------------
        public void SwitchUser_ButtonClick(object sender, RoutedEventArgs e)
        {
            AccountsList.OpenSwitchAccountModal(this, UpdateUser);

        }

        //----------------------------------------------------------------------------
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length > 0)
                {
                    sender.ItemsSource = this.GetSuggestions(sender.Text);
                }
            }
        }

        //----------------------------------------------------------------------------
        private object GetSuggestions(string text)
        {
            List<string> result = null;
            result = ProductsSuggestions.Where(x => x.ToLower().Contains(text.ToLower())).ToList();
            return result;
        }

        //----------------------------------------------------------------------------
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            SearchBar.Text = args.SelectedItem.ToString();
            //Frame.Navigate(typeof(FoodMenu), args.SelectedItem.ToString(), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        //----------------------------------------------------------------------------
        private void SearchInFoodMenu(object sender, KeyRoutedEventArgs e)
        {
            AutoSuggestBox autoSuggestBox = sender as AutoSuggestBox;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Frame.Navigate(typeof(FoodMenu), autoSuggestBox.Text, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        //----------------------------------------------------------------------------
        private void SearchInFoodMenu(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FoodMenu), SearchBar.Text, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        //----------------------------------------------------------------------------
        private void AccountInfoRedirect(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountInfo), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        //----------------------------------------------------------------------------
        private void AddNewAccount(object sender, RoutedEventArgs e)
        {
            if (Accounts.Count < 1)
            {
                CreateAccount(sender, e);
            }
        }

        //----------------------------------------------------------------------------
        public async void SwitchAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Olá! Vamos começar?";
            dialog.PrimaryButtonText = "Trocar usuário";
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new SwitchAccountModal();
            //dialog.PrimaryButtonClick += delegate { AddAccount(dialog.Content); };
            var result = await dialog.ShowAsync();
        }

        //----------------------------------------------------------------------------
        private async void CreateAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Olá! Vamos começar?";
            dialog.PrimaryButtonText = "Adicionar usuário";
            if (User != null)
                dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new AddAccountModal(dialog);
            dialog.PrimaryButtonClick += delegate { AddAccount(dialog.Content); };
            var result = await dialog.ShowAsync();
        }

        //----------------------------------------------------------------------------
        public void AddAccount(object sender)
        {
            Account NewUser = null;
            AddAccountModal aam = sender as AddAccountModal;
            NewUser = aam.CreateNewAccount();
            
            List<Account> accountsList = AccountsList.AddNewAccount(NewUser);
            UpdateUser(AccountsList.SelectedAccount);
        }

        //----------------------------------------------------------------------------
        public void UpdateUser(Account user)
        {
            User = AccountsList.SwitchAccounts(user);
            Debug.WriteLine($"[MainPage] Account changed to {User.Name}");
            CurrentUsername.Content = User.Name;
            ShoppingCart.DefineUser(User);
            Greetings.Text = $"Olá, {User.Name}!";
            if (User != null)
            {
                CurrentUsername.Visibility = Visibility.Visible;
            }
            //return Accounts;
        }
    }
}

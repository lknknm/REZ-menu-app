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
        private static List<Product> products;

        private List<string> suggestions;

        public static List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            if (AccountsList.Accounts.Count > 0)
            {
                UpdateUser(AccountsList.SelectedAccount);
            };

            //User = new Account((string)CurrentUser.Content, "123");
            //ShoppingCart.DefineUser(User);

            

            var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Properties", "Products.json");
            StreamReader reader = new(jsonFilePath);
            jsonString = reader.ReadToEnd();
            Products = JsonConvert.DeserializeObject<List<Product>>(jsonString);
            suggestions = Products.Select(p => p.Name).ToList();
        }

        public void UpdateUser(Account user)
        {
            CurrentUsername.Content = user.Name;
            ShoppingCart.DefineUser(user);
        }

        private void OpenFoodMenu(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Frame.Navigate(typeof(FoodMenu), button, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void ShoppingCart_ButtonClick(object sender, RoutedEventArgs e)
        {
            shoppingCart.OpenShoppingModal(this, shoppingCart, ToggleThemeTeachingTip1);
        }

        private void SwitchUser_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AccountsList.SwitchAccounts(button.Content.ToString());
        }

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

        private object GetSuggestions(string text)
        {
            List<string> result = null;
            result = suggestions.Where(x => x.ToLower().Contains(text.ToLower())).ToList();
            return result;
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            SearchBar.Text = args.SelectedItem.ToString();
            //Frame.Navigate(typeof(FoodMenu), args.SelectedItem.ToString(), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void SearchInFoodMenu(object sender, KeyRoutedEventArgs e)
        {
            AutoSuggestBox autoSuggestBox = sender as AutoSuggestBox;
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Frame.Navigate(typeof(FoodMenu), autoSuggestBox.Text, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private void SearchInFoodMenu(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FoodMenu), SearchBar.Text, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void AccountInfoRedirect(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AccountInfo), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void AddNewAccount(object sender, RoutedEventArgs e)
        {
            if (AccountsList.Accounts.Count < 1)
            {
                AddAccount(sender, e);
                
                //Debug.WriteLine($"User: {User.Name}");
            }

        }

        private async void AddAccount(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Olá! Vamos começar?";
            dialog.PrimaryButtonText = "Adicionar usuário";
            
            //dialog.PrimaryButtonClick += delegate { UpdateUser(); };
            //dialog.PrimaryButtonClick += delegate { Frame.Navigate(typeof(MainPage)); };
            dialog.CloseButtonText = "Cancelar";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new AddAccountModal();
            dialog.PrimaryButtonClick += delegate { CreateAccount(dialog.Content); };
            var result = await dialog.ShowAsync();
        }

        public void CreateAccount(object sender)
        {
            AddAccountModal aam = sender as AddAccountModal;
            (string name, string cpf) newUserInfo;

            newUserInfo = aam.CreateNewAccount();

            Account NewUser = new Account(newUserInfo.name, newUserInfo.cpf);
            CurrentUsername.Content = AccountsList.SelectedAccount.Name;
            UpdateUser(AccountsList.SelectedAccount);
            Debug.WriteLine($"Users list length: {AccountsList.Accounts.Count}");
            Debug.WriteLine($"Current User: {AccountsList.SelectedAccount.Name}");
        }

    }
}

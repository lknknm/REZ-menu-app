using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace REZ
{
    public sealed partial class ItemDialogModal : Page
    {
        public string ImageSource { get; }
        public static int ItemQuantity;

        public ItemDialogModal(Product item)
        {
            this.InitializeComponent();
            DataContext = this;
            Description.Text = item.Description.ToString();
            Price.Text = "R$ " + item.Price.ToString();
            ImageSource = item.ImageSource;
            
        }

        public static void SetItemQuantity(Product item, int newValue)
        {
            item.Quantity += newValue;
        }

        private async void SetQuantityValue(object sender, NumberBoxValueChangedEventArgs e)
        {
            NumberBox Qty = (NumberBox)sender;
            if (Qty != null && Qty.Value > 0)
            {
                Debug.WriteLine($"Quantity: {Qty.Value}"); // Log do valor da quantidade

                try
                {
                    ItemQuantity = (int)Qty.Value;
                    Debug.WriteLine("Quantity assigned to Item successfully."); // Log de sucesso
                    Debug.WriteLine($"ItemQuantity: {ItemQuantity}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}"); // Log da exceção
                    ContentDialog erro = new ContentDialog();
                    erro.Content = ex;

                    var result = await erro.ShowAsync();
                }
            }
            else
            {
                Debug.WriteLine("Quantity is null or not greater than 0."); // Log de condição não satisfeita
            }

            Debug.WriteLine("SetQuantityValue event handler completed."); // Log de conclusão
        }

        

    }

    
}

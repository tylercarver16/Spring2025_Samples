using Microsoft.Maui.Storage;

namespace Maui.eCommerce.Views;

public partial class TaxConfigPage : ContentPage
{
    public TaxConfigPage()
    {
        InitializeComponent();
        var currentTax = Preferences.Get("SalesTaxRate", 7.0);
        TaxEntry.Text = currentTax.ToString("F2");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (double.TryParse(TaxEntry.Text, out double newRate))
        {
            Preferences.Set("SalesTaxRate", newRate);
            await DisplayAlert("Saved", $"Tax rate set to {newRate:F2}%", "OK");
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            await DisplayAlert("Invalid", "Please enter a valid number.", "OK");
        }
    }

}

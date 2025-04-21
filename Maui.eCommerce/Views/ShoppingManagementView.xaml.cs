using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
    public ShoppingManagementView()
    {
        InitializeComponent();
        BindingContext = new ShoppingManagementViewModel();
    }

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }

    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }

    private void InlineAddClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Library.eCommerce.Models.Item item)
        {
            var vm = BindingContext as ShoppingManagementViewModel;
            vm?.PurchaseItem(item, item.QuantityToAdd);
        }
    }

    private void GoToHomeClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).Checkout();
    }

    private void SortPickerChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selected = picker.SelectedIndex;

        if (BindingContext is ShoppingManagementViewModel vm)
        {
            vm.SortBy = selected == 1 ? "Price" : "Name";
            vm.RefreshUX();
        }
    }

}

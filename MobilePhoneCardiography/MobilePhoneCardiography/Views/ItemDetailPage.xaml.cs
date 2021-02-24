using MobilePhoneCardiography.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MobilePhoneCardiography.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
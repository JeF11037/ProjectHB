using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectHB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lv.ItemsSource = await App.Database.GetAsyncMessage();
            OnAppearing();
        }
        public Message()
        {
            InitializeComponent();
            lv.Margin = new Thickness(20, 90, 20, 90);
            OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(messageEntry.Text))
            {
                await App.Database.SaveAsyncMessage(new Data.MessagePreset
                {
                    MsgContent = messageEntry.Text
                });

                messageEntry.Text = string.Empty;
            }
        }

        private async void lv_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var message = (Data.MessagePreset)e.Item;
            Random rnd = new Random();
            string action = await DisplayActionSheet("Actions", "Cancel", null, "Delete");
            switch (action)
            {
                case "Delete":
                    await App.Database.DeleteAsyncMessage(message);
                    break;
                default:
                    break;
            }
        }
    }
}
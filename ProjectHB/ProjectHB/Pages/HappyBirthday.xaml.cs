using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectHB.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HappyBirthday : ContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lv.ItemsSource = await App.Database.GetAsyncContact();
        }
        public HappyBirthday()
        {
            InitializeComponent();
            lv.Margin = new Thickness(20, 90, 20, 90);
            OnAppearing();
        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                };
                await Email.ComposeAsync(message);
                await DisplayAlert("Alert", "successful", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                Console.WriteLine("Email not supported on this device");
                await DisplayAlert("Alert", "Email not supported on this device", "OK");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong !");
                await DisplayAlert("Alert", "Something went wrong !", "OK");
            }
        }

        public async Task SendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine("Email not supported on this device");
                await DisplayAlert("Alert", "Email not supported on this device", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong !");
                await DisplayAlert("Alert", "Something went wrong !", "OK");
            }
        }

        private async void lv_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var contact = (Data.ContactPreset)e.Item;
            var message = await App.Database.GetAsyncMessage();
            Random rnd = new Random();
            string action = await DisplayActionSheet("Send to ?", "Cancel", null, "Email", "Message", "Delete");
            switch (action)
            {
                case "Email":
                    await SendEmail("Happy Birthday !", message[rnd.Next(0, message.Count)].MsgContent, new List<string>() { contact.Email });
                    break;
                case "Message":
                    await SendSms(message[rnd.Next(0, message.Count)].MsgContent, contact.Phone);
                    break;
                case "Delete":
                    await App.Database.DeleteAsyncContact(contact);
                    break;
                default:
                    break;
            }
        }
    }
}
using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using IdeaAnchor.Helper;
using Plugin.InAppBilling;

namespace IdeaAnchor.ViewModels
{
	public class SubscriptionViewModel : BindableObject
    {
		public bool IsBusy { get; set; }

        private bool _isButtonSelected;

        public bool IsButtonSelected
        {
            get => _isButtonSelected;
            set
            {
                _isButtonSelected = value;

                OnPropertyChanged(nameof(IsButtonSelected));
                OnPropertyChanged(nameof(ButtonBackgroundColor));
            }
        }

        public Color ButtonBackgroundColor => IsButtonSelected ? ThemeColors.Primary : ThemeColors.ColorDarkGrey;

        private async Task PurchaseSubscription()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {

                // check internet first with Essentials
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    return;

                // connect to the app store api
                var connected = await CrossInAppBilling.Current.ConnectAsync();
                if (!connected)
                    return;


                var productIdSub = "mysubscriptionid";

                //try to make purchase, this will return a purchase, empty, or throw an exception
                var purchase = await CrossInAppBilling.Current.PurchaseAsync(productIdSub, ItemType.Subscription);

                if (purchase == null)
                {
                    //nothing was purchased
                    return;
                }

                if (purchase.State == PurchaseState.Purchased)
                {
                    Preferences.Set("SubExpirationDate", AddSubTime(DateTime.UtcNow));
                    Preferences.Set("HasPurchasedSub", true);
                    Preferences.Set("CheckSubStatus", true);

                    // Update UI if necessary
                    //SetPro();

                    try
                    {
                        //commented out this cos the acknowledge method is missing

                        // It is required to acknowledge the purchase, else it will be refunded
                        //if (DeviceInfo.Platform == DevicePlatform.Android)
                        //    await CrossInAppBilling.Current.AcknowledgePurchaseAsync(purchase.PurchaseToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to acknowledge purchase: " + ex);
                    }
                }
                else
                {
                    throw new InAppBillingPurchaseException(PurchaseError.GeneralError);
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                // Handle all the different error codes that can occure and do a pop up
            }
            catch (Exception ex)
            {
                // Handle a generic exception as something really went wrong
            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
                IsBusy = false;
            }
        }

        //subscription time = 1 month, with 2 days margin
        private static DateTime AddSubTime(DateTime dateTime) => dateTime.AddMonths(1).AddDays(2);

        public ICommand GetSubscriptionCommand => new AsyncRelayCommand(GetSubscription);

        private async Task GetSubscription()
        {
            IsButtonSelected = true;

            await PurchaseSubscription();

            IsButtonSelected = false;
        }
    }
}


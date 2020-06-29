using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidWTVersus.DBEntities;
using System.Reactive.Linq;
using Akavache;
using Plugin.Connectivity;

namespace AndroidWTVersus
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Startup:AppCompatActivity
    {
        ArrayOfPlanes arrayOfPlanes;
        Button button11;
        TextView textView11;
        Context context;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StartupLayout);
            context = Application.Context;

            button11 = FindViewById<Button>(Resource.Id.button11);
            textView11 = FindViewById<TextView>(Resource.Id.textView11);
            textView11.SetText("Waiting...", TextView.BufferType.Normal);

            CheckInternetConnection();
            Task.Run(() => CheckIfDBCached().Wait());
        }

        private void CheckInternetConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                textView11.SetText("no internet", TextView.BufferType.Normal);
                AlertDialogWhenNoInternet();
            }
        }

        private void AlertDialogWhenNoInternet()
        {
            string title = context.Resources.GetString(Resource.String.alertDialogConnectTitle);
            string message = context.Resources.GetString(Resource.String.alertDialogConnectMessage);
            string retry = context.Resources.GetString(Resource.String.alertDialogConnectRetry);

            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetCancelable(false);
            alert.SetPositiveButton(retry, (senderAlert, args) =>
            {
                base.Recreate();
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private async Task CheckIfDBCached()
        {
            try
            {
                RunOnUiThread(() => {
                    textView11.SetText("try", TextView.BufferType.Normal);
                });
                var arrayOfPlanesCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
            }
            catch (KeyNotFoundException ex)
            {
                RunOnUiThread(() => {
                    textView11.SetText("catch", TextView.BufferType.Normal);
                });
                await GetPlanesListFromApiAsync();
            }

        }

        private async Task GetPlanesListFromApiAsync()
        {
            RunOnUiThread(() => {
                textView11.SetText("apistart", TextView.BufferType.Normal);
            });
            string URL = context.Resources.GetString(Resource.String.apiPlanesUrl);
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfPlanes));
            arrayOfPlanes = (ArrayOfPlanes)serializer.Deserialize(xReader);

            Registrations.Start("AkavacheExperiment");

            await BlobCache.UserAccount.InsertObject("cachedArrayOfPlanes", arrayOfPlanes);
            RunOnUiThread(() => {
                textView11.SetText("apiend", TextView.BufferType.Normal);
            });
        }


    }
}
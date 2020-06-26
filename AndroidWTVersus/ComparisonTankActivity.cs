using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using System.Collections.Generic;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidWTVersus.DBEntities;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AndroidWTVersus
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ComparisonTankActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        #region initialization interface values
        TextView textMessage;
        #endregion

        #region initialization interface values
        Context context;
        
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization View, context and Bottom Menu

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ComparisonTankLayout);
            context = Application.Context;
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            #endregion

            ArrayOfPlanes planes = GetPlanesListFromApi();

            string s = planes.PlanesList[120].Name;
            textMessage = FindViewById<TextView>(Resource.Id.message);
            textMessage.SetText(s, TextView.BufferType.Normal);
        }


        private ArrayOfPlanes GetPlanesListFromApi()
        {
            string URL = context.Resources.GetString(Resource.String.apiPlanesUrl);
            ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
            XmlReader xReader = initial.ApiXmlReader(URL);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfPlanes));
            ArrayOfPlanes planes = (ArrayOfPlanes)serializer.Deserialize(xReader);
            return planes;
        }


        #region Menu navigation items

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_compare:
                    //textMessage.SetText(Resource.String.title_compare);
                    return true;
                case Resource.Id.navigation_statistics:
                    return true;
                case Resource.Id.navigation_feedback:
                    return true;
            }
            return false;
        }
        #endregion

        #region Permission

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
        #endregion
    }
}


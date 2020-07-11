using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Gms.Ads;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System;

namespace AndroidWTVersus
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class StatisticsActivity:AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        Context context;

        /// <summary>
        /// Base Android OnCreate method. Entry point for app
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization required elements
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StatisticsLayout);
            context = Application.Context;
            #endregion

            #region Ads initializer
            MobileAds.Initialize(context);
            var adView = FindViewById<AdView>(Resource.Id.adViewStatistics);
            adView.LoadAd(new AdRequest.Builder().Build());
            //var requestbuilder = new AdRequest.Builder().AddTestDevice("46CCAB8BBCE5B5FFA79C22BEB98029AC");
            //adView.LoadAd(requestbuilder.Build());
            #endregion

            BottomMenuInitializer();

            //Code here
        }

        /// <summary>
        /// Initialization of Bottom Menu
        /// </summary>
        private void BottomMenuInitializer()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            BottomNavigationItemView itemCompare = navigation.FindViewById<BottomNavigationItemView>(Resource.Id.navigation_compare);
            BottomNavigationItemView itemStatistics = navigation.FindViewById<BottomNavigationItemView>(Resource.Id.navigation_statistics);
            BottomNavigationItemView itemFeedback = navigation.FindViewById<BottomNavigationItemView>(Resource.Id.navigation_feedback);

            itemCompare.SetIconTintList(ColorStateList.ValueOf(Color.ParseColor("#707070")));
            itemStatistics.SetIconTintList(ColorStateList.ValueOf(Color.ParseColor("#dc3546")));
            itemFeedback.SetIconTintList(ColorStateList.ValueOf(Color.ParseColor("#707070")));
        }

        /// <summary>
        /// Menu navigation method
        /// </summary>
        /// <param name="item">Menu item</param>
        /// <returns></returns>
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_compare:
                    var intentTank = new Intent(this, typeof(ComparisonTankActivity));
                    intentTank.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentTank);
                    return true;
                case Resource.Id.navigation_statistics:
                    return true;
                case Resource.Id.navigation_feedback:
                    var intentFeedback = new Intent(this, typeof(FeedbackActivity));
                    intentFeedback.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentFeedback);
                    return true;
            }
            return false;
        }

    }
}
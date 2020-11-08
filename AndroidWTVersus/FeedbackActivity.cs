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
    public class FeedbackActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        Context context;
        EditText editMessage;
        Button buttonSend, buttonReddit, buttonVK;
        ImageButton buttonRefWT, buttonRefWoT;
        RatingBar ratingBar;

        /// <summary>
        /// Base Android OnCreate method. Entry point for app
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization required elements
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FeedbackLayout);
            context = Application.Context;
            BottomMenuInitializer();
            #endregion

            #region Ads initializer
            MobileAds.Initialize(context);
            var adView = FindViewById<AdView>(Resource.Id.adViewFeedback);
            adView.LoadAd(new AdRequest.Builder().Build());
            //var requestbuilder = new AdRequest.Builder().AddTestDevice("46CCAB8BBCE5B5FFA79C22BEB98029AC");
            //adView.LoadAd(requestbuilder.Build());
            #endregion

            BindingInterfaceElementsToCode();
            buttonSend.Click += ButtonSend_Click;
            buttonReddit.Click += ButtonReddit_Click;
            buttonVK.Click += ButtonVK_Click;
            ratingBar.RatingBarChange += RatingBar_RatingBarChange;
            buttonRefWT.Click += ButtonRefWT_Click;
            buttonRefWoT.Click += ButtonRefWoT_Click;
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            Intent email = new Intent(Intent.ActionSend);
            email.PutExtra(Intent.ExtraEmail, new string[] { "waveappfeedback@gmail.com" });
            email.PutExtra(Intent.ExtraSubject, "From Android Versus");
            email.PutExtra(Intent.ExtraText, editMessage.Text.ToString());
            email.SetType("message/rfc822");
            StartActivity(Intent.CreateChooser(email, context.Resources.GetString(Resource.String.chooseEmailClient)));
        }

        private void ButtonVK_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Intent.ActionView, Android.Net.Uri
                .Parse("https://www.vk.com/wave_app/")));
        }

        private void ButtonReddit_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Intent.ActionView, Android.Net.Uri
                 .Parse("https://www.reddit.com/r/wave_app/")));
        }

        private void RatingBar_RatingBarChange(object sender, RatingBar.RatingBarChangeEventArgs e)
        {
            StartActivity(new Intent(Intent.ActionView, Android.Net.Uri
                .Parse("https://play.google.com/store/apps/details?id=com.wave.wtversus")));
        }

        private void ButtonRefWT_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Intent.ActionView, Android.Net.Uri
              .Parse("https://play.google.com/store/apps/details?id=com.wave.wtquiz")));
        }

        private void ButtonRefWoT_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Intent.ActionView, Android.Net.Uri
              .Parse("https://play.google.com/store/apps/details?id=com.wave.wotquiz")));
        }

        /// <summary>
        /// Binding xml elements to code
        /// </summary>
        private void BindingInterfaceElementsToCode()
        {
            editMessage = FindViewById<EditText>(Resource.Id.editMessage);
            buttonSend = FindViewById<Button>(Resource.Id.buttonSend);
            buttonReddit = FindViewById<Button>(Resource.Id.buttonReddit);
            buttonVK = FindViewById<Button>(Resource.Id.buttonVK);
            ratingBar = FindViewById<RatingBar>(Resource.Id.ratingBar);
            buttonRefWT = FindViewById<ImageButton>(Resource.Id.buttonRefWT);
            buttonRefWoT = FindViewById<ImageButton>(Resource.Id.buttonRefWoT);
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
            itemStatistics.SetIconTintList(ColorStateList.ValueOf(Color.ParseColor("#707070")));
            itemFeedback.SetIconTintList(ColorStateList.ValueOf(Color.ParseColor("#dc3546")));
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
                    var intentStatistics = new Intent(this, typeof(StatisticsActivity));
                    intentStatistics.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentStatistics);
                    return true;
                case Resource.Id.navigation_feedback:
                    return true;
            }
            return false;
        }
    }
}
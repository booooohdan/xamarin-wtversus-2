using Akavache;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidWTVersus.Adapters;
using AndroidWTVersus.Helpers;
using AndroidWTVersus.XmlHandler;
using FFImageLoading;
using FFImageLoading.Work;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static Android.Views.View;
using String = System.String;

namespace AndroidWTVersus
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class ComparisonHeliActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {
        #region initialization View values
        ImageView ivH_Heli1;
        ImageView ivH_Heli2;

        Button searchableHeliButton1;
        Button searchableHeliButton2;
        Button topMenuAircraftsButton;
        Button topMenuTanksButton;
        Button topMenuHeliButton;
        Button topMenuShipsButton;

        TextView tvH_BattleRating1;
        TextView tvH_BattleRating2;
        TextView tvH_FirstYear1;
        TextView tvH_FirstYear2;
        TextView tvH_RepairCost1;
        TextView tvH_RepairCost2;

        ImageView ivH_AutoAGMissile1;
        ImageView ivH_AGMissile1;
        ImageView ivH_ASMissile1;
        ImageView ivH_AAMissile1;
        ImageView ivH_HBomb1;
        ImageView ivH_TurningCannon1;
        ImageView ivH_FixedCannon1;
        ImageView ivH_AutoAGMissile2;
        ImageView ivH_AGMissile2;
        ImageView ivH_ASMissile2;
        ImageView ivH_AAMissile2;
        ImageView ivH_HBomb2;
        ImageView ivH_TurningCannon2;
        ImageView ivH_FixedCannon2;

        TextView tvH_Cannon1;
        TextView tvH_Cannon2;
        TextView tvH_agmCount1;
        TextView tvH_agmCount2;
        TextView tvH_agmPenetration1;
        TextView tvH_agmPenetration2;
        TextView tvH_agmSpeed1;
        TextView tvH_agmSpeed2;
        TextView tvH_agmRange1;
        TextView tvH_agmRange2;
        TextView tvH_aamCount1;
        TextView tvH_aamCount2;
        TextView tvH_asmCount1;
        TextView tvH_asmCount2;
        TextView tvH_bombPayload1;
        TextView tvH_bombPayload2;

        ImageView ivH_RWR1;
        ImageView ivH_IRCM1;
        ImageView ivH_Flares1;
        ImageView ivH_HIRSS1;
        ImageView ivH_LaserDesignator1;
        ImageView ivH_OpticalTracking1;
        ImageView ivH_CCIP1;
        ImageView ivH_ThermalGunner1;
        ImageView ivH_AirRadar1;
        ImageView ivH_GroundRadar1;

        ImageView ivH_RWR2;
        ImageView ivH_IRCM2;
        ImageView ivH_Flares2;
        ImageView ivH_HIRSS2;
        ImageView ivH_LaserDesignator2;
        ImageView ivH_OpticalTracking2;
        ImageView ivH_CCIP2;
        ImageView ivH_ThermalGunner2;
        ImageView ivH_AirRadar2;
        ImageView ivH_GroundRadar2;

        TextView tvH_maxSpeed1;
        TextView tvH_maxSpeed2;
        TextView tvH_climbTo10001;
        TextView tvH_climbTo10002;
        TextView tvH_TurnTime1;
        TextView tvH_TurnTime2;
        TextView tvH_EnginePower1;
        TextView tvH_EnginePower2;
        TextView tvH_TakeOffWeight1;
        TextView tvH_TakeOffWeight2;

        #endregion

        #region Initialization non View values

        Context context;
        ArrayOfHelis arrayOfHelis;
        SearchView searchView;
        ListView listView;
        HeliAdapter heliAdapter;
        Dialog dialog;
        LayoutInflater inflater;
        View view;
        TextView.BufferType TextNormal;

        string mm;
        string s;
        string m_s;
        string degree;
        string km_h;
        string h_p;
        string t;
        string br;
        string kg_s;
        string kg;
        string meters;
        #endregion

        /// <summary>
        /// Base Android OnCreate method. Entry point for app
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization required elements
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ComparisonHeliLayout);
            context = Application.Context;
            #endregion

            #region Ads initializer
            MobileAds.Initialize(context);
            var adView = FindViewById<AdView>(Resource.Id.adViewComparerHeli);
            adView.LoadAd(new AdRequest.Builder().Build());
            //var requestbuilder = new AdRequest.Builder().AddTestDevice("46CCAB8BBCE5B5FFA79C22BEB98029AC");
            //adView.LoadAd(requestbuilder.Build());
            #endregion

            BindingInterfaceElementsToCode();
            FillListFromCacheAsync().ConfigureAwait(false);
            TopMenuInitializer();
            BottomMenuInitializer();
            LetsCompare();

            searchableHeliButton1.Click += SearchableButton1_Click;
            searchableHeliButton2.Click += SearchableButton2_Click;
        }

        /// <summary>
        /// Initialize Custom Top Navigation Menu
        /// </summary>
        private void TopMenuInitializer()
        {
            topMenuHeliButton.SetBackgroundResource(Resource.Drawable.ButtonAsTabShape);
            topMenuHeliButton.SetTextColor(Color.ParseColor("#dc3546"));
            topMenuAircraftsButton.SetOnClickListener(this);
            topMenuTanksButton.SetOnClickListener(this);
            topMenuHeliButton.SetOnClickListener(this);
            topMenuShipsButton.SetOnClickListener(this);
        }

        /// <summary>
        /// Initialize Android Bottom Navigation Menu
        /// </summary>
        private void BottomMenuInitializer()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        /// <summary>
        /// Load cached List of helis
        /// </summary>
        private async Task FillListFromCacheAsync()
        {
            arrayOfHelis = await BlobCache.UserAccount.GetObject<ArrayOfHelis>("cachedArrayOfHelis");
        }

        /// <summary>
        /// Search Dialog Initialization
        /// </summary>
        private void SearchDialogInitialization()
        {
            dialog = new Dialog(this);
            inflater = LayoutInflater.From(this);
            view = inflater.Inflate(Resource.Layout._searchDialog, null);

            listView = view.FindViewById<ListView>(Resource.Id.listView);
            searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
            searchView.SetQueryHint("AH-64..");

            heliAdapter = new HeliAdapter(this, arrayOfHelis.HelisListApi);
            listView.Adapter = heliAdapter;
        }

        private void SearchableButton1_Click(object sender, EventArgs e)
        {
            SearchDialogInitialization();

            searchView.QueryTextChange += SearchView_QueryTextChange;
            listView.ItemClick += ListView1_ItemClick;

            //scroll List to 3 position
            listView.SetSelection(2);

            dialog.SetContentView(view);
            dialog.Show();
        }

        private void SearchableButton2_Click(object sender, EventArgs e)
        {
            SearchDialogInitialization();

            searchView.QueryTextChange += SearchView_QueryTextChange;
            listView.ItemClick += ListView2_ItemClick;

            //scroll List to 3 position
            listView.SetSelection(2);

            dialog.SetContentView(view);
            dialog.Show();
        }

        private void SearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //Filtering SearchView text
            heliAdapter.Filter.InvokeFilter(e.NewText);
        }

        private void ListView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            dialog.Dismiss();

            SetTextToLeft(e);
            SetImageToLeft(e);
            LetsCompare();
        }

        private void ListView2_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            dialog.Dismiss();

            SetTextToRight(e);
            SetImageToRight(e);
            LetsCompare();
            //CompareLeftAndRight();
        }

        /// <summary>
        /// Set data from List helis to left part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToLeft(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(heliAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivH_Heli1);

            ivH_RWR1.Visibility = heliAdapter[e.Position].RWR ? ViewStates.Visible : ViewStates.Gone;
            ivH_IRCM1.Visibility = heliAdapter[e.Position].IRCM ? ViewStates.Visible : ViewStates.Gone;
            ivH_Flares1.Visibility = heliAdapter[e.Position].Flares ? ViewStates.Visible : ViewStates.Gone;
            ivH_HIRSS1.Visibility = heliAdapter[e.Position].HIRSS ? ViewStates.Visible : ViewStates.Gone;
            ivH_LaserDesignator1.Visibility = heliAdapter[e.Position].LaserDesignator ? ViewStates.Visible : ViewStates.Gone;
            ivH_OpticalTracking1.Visibility = heliAdapter[e.Position].OpticalTracking ? ViewStates.Visible : ViewStates.Gone;
            ivH_CCIP1.Visibility = heliAdapter[e.Position].CCIP ? ViewStates.Visible : ViewStates.Gone;
            ivH_ThermalGunner1.Visibility = heliAdapter[e.Position].ThermalGunner ? ViewStates.Visible : ViewStates.Gone;
            ivH_AirRadar1.Visibility = heliAdapter[e.Position].AirRadar ? ViewStates.Visible : ViewStates.Gone;
            ivH_GroundRadar1.Visibility = heliAdapter[e.Position].GroundRadar ? ViewStates.Visible : ViewStates.Gone;
            ivH_AutoAGMissile1.Visibility = heliAdapter[e.Position].AutoAGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivH_AGMissile1.Visibility = heliAdapter[e.Position].AGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivH_AAMissile1.Visibility = heliAdapter[e.Position].AAMissile ? ViewStates.Visible : ViewStates.Gone;
            ivH_HBomb1.Visibility = heliAdapter[e.Position].Bomb ? ViewStates.Visible : ViewStates.Gone;
            ivH_TurningCannon1.Visibility = heliAdapter[e.Position].TurningCannon ? ViewStates.Visible : ViewStates.Gone;
            ivH_FixedCannon1.Visibility = heliAdapter[e.Position].FixedCannon ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List helis to right part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToRight(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(heliAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivH_Heli2);

            ivH_RWR2.Visibility = heliAdapter[e.Position].RWR ? ViewStates.Visible : ViewStates.Gone;
            ivH_IRCM2.Visibility = heliAdapter[e.Position].IRCM ? ViewStates.Visible : ViewStates.Gone;
            ivH_Flares2.Visibility = heliAdapter[e.Position].Flares ? ViewStates.Visible : ViewStates.Gone;
            ivH_HIRSS2.Visibility = heliAdapter[e.Position].HIRSS ? ViewStates.Visible : ViewStates.Gone;
            ivH_LaserDesignator2.Visibility = heliAdapter[e.Position].LaserDesignator ? ViewStates.Visible : ViewStates.Gone;
            ivH_OpticalTracking2.Visibility = heliAdapter[e.Position].OpticalTracking ? ViewStates.Visible : ViewStates.Gone;
            ivH_CCIP2.Visibility = heliAdapter[e.Position].CCIP ? ViewStates.Visible : ViewStates.Gone;
            ivH_ThermalGunner2.Visibility = heliAdapter[e.Position].ThermalGunner ? ViewStates.Visible : ViewStates.Gone;
            ivH_AirRadar2.Visibility = heliAdapter[e.Position].AirRadar ? ViewStates.Visible : ViewStates.Gone;
            ivH_GroundRadar2.Visibility = heliAdapter[e.Position].GroundRadar ? ViewStates.Visible : ViewStates.Gone;
            ivH_AutoAGMissile2.Visibility = heliAdapter[e.Position].AutoAGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivH_AGMissile2.Visibility = heliAdapter[e.Position].AGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivH_AAMissile2.Visibility = heliAdapter[e.Position].AAMissile ? ViewStates.Visible : ViewStates.Gone;
            ivH_HBomb2.Visibility = heliAdapter[e.Position].Bomb ? ViewStates.Visible : ViewStates.Gone;
            ivH_TurningCannon2.Visibility = heliAdapter[e.Position].TurningCannon ? ViewStates.Visible : ViewStates.Gone;
            ivH_FixedCannon2.Visibility = heliAdapter[e.Position].FixedCannon ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List helis to left part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToLeft(AdapterView.ItemClickEventArgs e)
        {
            searchableHeliButton1.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchableHeliButton1.SetText(heliAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", heliAdapter[e.Position].BR);
            tvH_BattleRating1.SetText(br, TextNormal);
            tvH_FirstYear1.SetText(heliAdapter[e.Position].FirstFlyYear + "", TextNormal);
            tvH_RepairCost1.SetText(heliAdapter[e.Position].RepairCost + "", TextNormal);

            tvH_Cannon1.SetText(heliAdapter[e.Position].OffensiveWeapon.Replace("<br>", "\n") + "", TextNormal);
            tvH_agmCount1.SetText(heliAdapter[e.Position].AGMCount + "", TextNormal);
            tvH_agmPenetration1.SetText(heliAdapter[e.Position].AGMArmorPenetration + mm, TextNormal);
            tvH_agmSpeed1.SetText(heliAdapter[e.Position].AGMSpeed + m_s, TextNormal);
            tvH_agmRange1.SetText(heliAdapter[e.Position].AGMRange + meters, TextNormal);
            tvH_aamCount1.SetText(heliAdapter[e.Position].AAMCount + "", TextNormal);
            tvH_asmCount1.SetText(heliAdapter[e.Position].ASMCount + "", TextNormal);
            tvH_bombPayload1.SetText(heliAdapter[e.Position].BombLoad + kg, TextNormal);
            tvH_maxSpeed1.SetText(heliAdapter[e.Position].MaxSpeed + km_h, TextNormal);
            tvH_climbTo10001.SetText(heliAdapter[e.Position].ClimbTo1000 + s, TextNormal);
            tvH_TurnTime1.SetText(heliAdapter[e.Position].Turn360 + s, TextNormal);
            tvH_EnginePower1.SetText(heliAdapter[e.Position].EnginePower + "", TextNormal);
            tvH_TakeOffWeight1.SetText(heliAdapter[e.Position].Weight + kg, TextNormal);
        }

        /// <summary>
        /// Set data from List helis to right part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToRight(AdapterView.ItemClickEventArgs e)
        {
            searchableHeliButton2.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchableHeliButton2.SetText(heliAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", heliAdapter[e.Position].BR);
            tvH_BattleRating2.SetText(br, TextNormal);
            tvH_FirstYear2.SetText(heliAdapter[e.Position].FirstFlyYear + "", TextNormal);
            tvH_RepairCost2.SetText(heliAdapter[e.Position].RepairCost + "", TextNormal);

            tvH_Cannon2.SetText(heliAdapter[e.Position].OffensiveWeapon.Replace("<br>", "\n") + "", TextNormal);
            tvH_agmCount2.SetText(heliAdapter[e.Position].AGMCount + "", TextNormal);
            tvH_agmPenetration2.SetText(heliAdapter[e.Position].AGMArmorPenetration + mm, TextNormal);
            tvH_agmSpeed2.SetText(heliAdapter[e.Position].AGMSpeed + m_s, TextNormal);
            tvH_agmRange2.SetText(heliAdapter[e.Position].AGMRange + meters, TextNormal);
            tvH_aamCount2.SetText(heliAdapter[e.Position].AAMCount + "", TextNormal);
            tvH_asmCount2.SetText(heliAdapter[e.Position].ASMCount + "", TextNormal);
            tvH_bombPayload2.SetText(heliAdapter[e.Position].BombLoad + kg, TextNormal);
            tvH_maxSpeed2.SetText(heliAdapter[e.Position].MaxSpeed + km_h, TextNormal);
            tvH_climbTo10002.SetText(heliAdapter[e.Position].ClimbTo1000 + s, TextNormal);
            tvH_TurnTime2.SetText(heliAdapter[e.Position].Turn360 + s, TextNormal);
            tvH_EnginePower2.SetText(heliAdapter[e.Position].EnginePower + "", TextNormal);
            tvH_TakeOffWeight2.SetText(heliAdapter[e.Position].Weight + kg, TextNormal);
        }

        /// <summary>
        /// Describes all strings to compare
        /// </summary>
        private void LetsCompare()
        {
            var comparer = new CompareHelper();

            comparer.CompareWhenLowIsGood(tvH_RepairCost1, tvH_RepairCost2);
            comparer.CompareWhenHighIsGood(tvH_FirstYear1, tvH_FirstYear2);
            comparer.CompareWhenLowIsGood(tvH_BattleRating1, tvH_BattleRating2);
            comparer.CompareWhenHighIsGood(tvH_agmCount1, tvH_agmCount2);
            comparer.CompareWhenHighIsGood(tvH_agmPenetration1, tvH_agmPenetration2);
            comparer.CompareWhenHighIsGood(tvH_agmSpeed1, tvH_agmSpeed2);
            comparer.CompareWhenHighIsGood(tvH_agmRange1, tvH_agmRange2);
            comparer.CompareWhenHighIsGood(tvH_aamCount1, tvH_aamCount2);
            comparer.CompareWhenHighIsGood(tvH_asmCount1, tvH_asmCount2);
            comparer.CompareWhenHighIsGood(tvH_bombPayload1, tvH_bombPayload2);
            comparer.CompareWhenHighIsGood(tvH_maxSpeed1, tvH_maxSpeed2);
            comparer.CompareWhenLowIsGood(tvH_climbTo10001, tvH_climbTo10002);
            comparer.CompareWhenLowIsGood(tvH_TurnTime1, tvH_TurnTime2);
            comparer.CompareWhenHighIsGood(tvH_EnginePower1, tvH_EnginePower2);
            comparer.CompareWhenLowIsGood(tvH_TakeOffWeight1, tvH_TakeOffWeight2);
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
                    return true;
                case Resource.Id.navigation_statistics:
                    var intentStatistics = new Intent(this, typeof(StatisticsActivity));
                    intentStatistics.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentStatistics);
                    return true;
                case Resource.Id.navigation_feedback:
                    var intentFeedback = new Intent(this, typeof(FeedbackActivity));
                    intentFeedback.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentFeedback);
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Top Tab Menu navigation method
        /// </summary>
        /// <param name="v">Menu item</param>
        /// <returns></returns>
        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.topMenuAircraftsButton:
                    var intentAvia = new Intent(this, typeof(ComparisonPlaneActivity));
                    intentAvia.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentAvia);
                    break;
                case Resource.Id.topMenuTanksButton:
                    var intentTank = new Intent(this, typeof(ComparisonTankActivity));
                    intentTank.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentTank);
                    break;
                case Resource.Id.topMenuHeliButton:
                    break;
                case Resource.Id.topMenuShipsButton:
                    break;
            }
        }

        /// <summary>
        /// Bind all views from xml to code
        /// </summary>
        private void BindingInterfaceElementsToCode()
        {
            TextNormal = TextView.BufferType.Normal;
            mm = context.Resources.GetString(Resource.String.mm);
            s = context.Resources.GetString(Resource.String.s);
            m_s = context.Resources.GetString(Resource.String.m_s);
            degree = context.Resources.GetString(Resource.String.degree);
            km_h = context.Resources.GetString(Resource.String.km_h);
            h_p = context.Resources.GetString(Resource.String.h_p);
            t = context.Resources.GetString(Resource.String.t);
            kg_s = context.Resources.GetString(Resource.String.kg_s);
            kg = context.Resources.GetString(Resource.String.kg);
            meters = context.Resources.GetString(Resource.String.meters);

            ivH_Heli1 = FindViewById<ImageView>(Resource.Id.ivH_Heli1);
            ivH_Heli2 = FindViewById<ImageView>(Resource.Id.ivH_Heli2);
            ivH_AutoAGMissile1 = FindViewById<ImageView>(Resource.Id.ivH_AutoAGMissile1);
            ivH_AGMissile1 = FindViewById<ImageView>(Resource.Id.ivH_AGMissile1);
            ivH_ASMissile1 = FindViewById<ImageView>(Resource.Id.ivH_ASMissile1);
            ivH_AAMissile1 = FindViewById<ImageView>(Resource.Id.ivH_AAMissile1);
            ivH_HBomb1 = FindViewById<ImageView>(Resource.Id.ivH_HBomb1);
            ivH_TurningCannon1 = FindViewById<ImageView>(Resource.Id.ivH_TurningCannon1);
            ivH_FixedCannon1 = FindViewById<ImageView>(Resource.Id.ivH_FixedCannon1);
            ivH_AutoAGMissile2 = FindViewById<ImageView>(Resource.Id.ivH_AutoAGMissile2);
            ivH_AGMissile2 = FindViewById<ImageView>(Resource.Id.ivH_AGMissile2);
            ivH_ASMissile2 = FindViewById<ImageView>(Resource.Id.ivH_ASMissile2);
            ivH_AAMissile2 = FindViewById<ImageView>(Resource.Id.ivH_AAMissile2);
            ivH_HBomb2 = FindViewById<ImageView>(Resource.Id.ivH_HBomb2);
            ivH_TurningCannon2 = FindViewById<ImageView>(Resource.Id.ivH_TurningCannon2);
            ivH_FixedCannon2 = FindViewById<ImageView>(Resource.Id.ivH_FixedCannon2);
            ivH_RWR1 = FindViewById<ImageView>(Resource.Id.ivH_RWR1);
            ivH_IRCM1 = FindViewById<ImageView>(Resource.Id.ivH_IRCM1);
            ivH_Flares1 = FindViewById<ImageView>(Resource.Id.ivH_Flares1);
            ivH_HIRSS1 = FindViewById<ImageView>(Resource.Id.ivH_HIRSS1);
            ivH_LaserDesignator1 = FindViewById<ImageView>(Resource.Id.ivH_LaserDesignator1);
            ivH_OpticalTracking1 = FindViewById<ImageView>(Resource.Id.ivH_OpticalTracking1);
            ivH_CCIP1 = FindViewById<ImageView>(Resource.Id.ivH_CCIP1);
            ivH_ThermalGunner1 = FindViewById<ImageView>(Resource.Id.ivH_ThermalGunner1);
            ivH_AirRadar1 = FindViewById<ImageView>(Resource.Id.ivH_AirRadar1);
            ivH_GroundRadar1 = FindViewById<ImageView>(Resource.Id.ivH_GroundRadar1);
            ivH_RWR2 = FindViewById<ImageView>(Resource.Id.ivH_RWR2);
            ivH_IRCM2 = FindViewById<ImageView>(Resource.Id.ivH_IRCM2);
            ivH_Flares2 = FindViewById<ImageView>(Resource.Id.ivH_Flares2);
            ivH_HIRSS2 = FindViewById<ImageView>(Resource.Id.ivH_HIRSS2);
            ivH_LaserDesignator2 = FindViewById<ImageView>(Resource.Id.ivH_LaserDesignator2);
            ivH_OpticalTracking2 = FindViewById<ImageView>(Resource.Id.ivH_OpticalTracking2);
            ivH_CCIP2 = FindViewById<ImageView>(Resource.Id.ivH_CCIP2);
            ivH_ThermalGunner2 = FindViewById<ImageView>(Resource.Id.ivH_ThermalGunner2);
            ivH_AirRadar2 = FindViewById<ImageView>(Resource.Id.ivH_AirRadar2);
            ivH_GroundRadar2 = FindViewById<ImageView>(Resource.Id.ivH_GroundRadar2);

            tvH_BattleRating1 = FindViewById<TextView>(Resource.Id.tvH_BattleRating1);
           tvH_FirstYear1 = FindViewById<TextView>(Resource.Id.tvH_FirstYear1);
           tvH_RepairCost1 = FindViewById<TextView>(Resource.Id.tvH_RepairCost1);
           tvH_maxSpeed1 = FindViewById<TextView>(Resource.Id.tvH_maxSpeed1);
           tvH_climbTo10001 = FindViewById<TextView>(Resource.Id.tvH_climbTo10001);
           tvH_TurnTime1 = FindViewById<TextView>(Resource.Id.tvH_TurnTime1);
           tvH_EnginePower1 = FindViewById<TextView>(Resource.Id.tvH_EnginePower1);
           tvH_TakeOffWeight1 = FindViewById<TextView>(Resource.Id.tvH_TakeOffWeight1);
           tvH_Cannon1 = FindViewById<TextView>(Resource.Id.tvH_Cannon1);
           tvH_agmCount1 = FindViewById<TextView>(Resource.Id.tvH_agmCount1);
           tvH_agmPenetration1 = FindViewById<TextView>(Resource.Id.tvH_agmPenetration1);
           tvH_agmSpeed1 = FindViewById<TextView>(Resource.Id.tvH_agmSpeed1);
           tvH_agmRange1 = FindViewById<TextView>(Resource.Id.tvH_agmRange1);
           tvH_aamCount1 = FindViewById<TextView>(Resource.Id.tvH_aamCount1);
           tvH_asmCount1 = FindViewById<TextView>(Resource.Id.tvH_asmCount1);
           tvH_bombPayload1 = FindViewById<TextView>(Resource.Id.tvH_bombPayload1);

            tvH_BattleRating2 = FindViewById<TextView>(Resource.Id.tvH_BattleRating2);
            tvH_FirstYear2 = FindViewById<TextView>(Resource.Id.tvH_FirstYear2);
            tvH_RepairCost2 = FindViewById<TextView>(Resource.Id.tvH_RepairCost2);
            tvH_maxSpeed2 = FindViewById<TextView>(Resource.Id.tvH_maxSpeed2);
            tvH_climbTo10002 = FindViewById<TextView>(Resource.Id.tvH_climbTo10002);
            tvH_TurnTime2 = FindViewById<TextView>(Resource.Id.tvH_TurnTime2);
            tvH_EnginePower2 = FindViewById<TextView>(Resource.Id.tvH_EnginePower2);
            tvH_TakeOffWeight2 = FindViewById<TextView>(Resource.Id.tvH_TakeOffWeight2);
            tvH_Cannon2 = FindViewById<TextView>(Resource.Id.tvH_Cannon2);
            tvH_agmCount2 = FindViewById<TextView>(Resource.Id.tvH_agmCount2);
            tvH_agmPenetration2 = FindViewById<TextView>(Resource.Id.tvH_agmPenetration2);
            tvH_agmSpeed2 = FindViewById<TextView>(Resource.Id.tvH_agmSpeed2);
            tvH_agmRange2 = FindViewById<TextView>(Resource.Id.tvH_agmRange2);
            tvH_aamCount2 = FindViewById<TextView>(Resource.Id.tvH_aamCount2);
            tvH_asmCount2 = FindViewById<TextView>(Resource.Id.tvH_asmCount2);
            tvH_bombPayload2 = FindViewById<TextView>(Resource.Id.tvH_bombPayload2);

            searchableHeliButton1 = FindViewById<Button>(Resource.Id.searchableHeliButton1);
           searchableHeliButton2 = FindViewById<Button>(Resource.Id.searchableHeliButton2);
           topMenuAircraftsButton = FindViewById<Button>(Resource.Id.topMenuAircraftsButton);
           topMenuTanksButton = FindViewById<Button>(Resource.Id.topMenuTanksButton);
           topMenuHeliButton = FindViewById<Button>(Resource.Id.topMenuHeliButton);
           topMenuShipsButton = FindViewById<Button>(Resource.Id.topMenuShipsButton);
        }
    }
}
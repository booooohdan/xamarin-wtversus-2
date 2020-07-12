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
    public class ComparisonShipActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {
        #region initialization View values

        ImageView ivS_Ship1;
        ImageView ivS_Ship2;

        Button searchableShipButton1;
        Button searchableShipButton2;
        Button topMenuAircraftsButton;
        Button topMenuTanksButton;
        Button topMenuHeliButton;
        Button topMenuShipsButton;

        TextView tvS_BattleRating1;
        TextView tvS_FirstYear1;
        TextView tvS_RepairCost1;
        TextView tvS_MainCaliber1;
        TextView tvS_MainCaliberReload1;
        TextView tvS_AUCaliber1;
        TextView tvS_AAACaliber1;
        TextView tvS_torpedoCount1;
        TextView tvS_torpedoMaxSpeed1;
        TextView tvS_torpedoExplosiveMass1; 
        TextView tvS_ArmorTower1;
        TextView tvS_ArmorCitadel1;
        TextView tvS_MaxSpeed1;
        TextView tvS_MaxReverseSpeed1;
        TextView tvS_Acceleration1;
        TextView tvS_BrakingTime1;
        TextView tvS_TurnTime1;
        TextView tvS_Displacement1;
        TextView tvS_Crew1;

        ImageView ivS_MC_AP1;
        ImageView ivS_MC_APHE1;
        ImageView ivS_MC_HE1;
        ImageView ivS_MC_SAP1;
        ImageView ivS_MC_APCR1;
        ImageView ivS_MC_HEVT1;
        ImageView ivS_MC_HEDF1;
        ImageView ivS_MC_Shrapnel1;  
        ImageView ivS_AU_AP1;
        ImageView ivS_AU_APHE1;
        ImageView ivS_AU_HE1;
        ImageView ivS_AU_SAP1;
        ImageView ivS_AU_APCR1;
        ImageView ivS_AU_HEVT1;
        ImageView ivS_AU_HEDF1;
        ImageView ivS_AU_Shrapnel1;
        ImageView ivS_AAA_AP1;
        ImageView ivS_AAA_APHE1;
        ImageView ivS_AAA_HE1;
        ImageView ivS_AAA_SAP1;
        ImageView ivS_AAA_APCR1;
        ImageView ivS_AAA_HEVT1;
        ImageView ivS_AAA_HEDF1;
        ImageView ivS_AAA_Shrapnel1;
        ImageView ivS_AirRadar1;
        ImageView ivS_ShipRadar1;
        ImageView ivS_DepthCharge1;
        ImageView ivS_Mine1;
        ImageView ivS_Rocket1;
        ImageView ivS_Torpedo1;

        TextView tvS_BattleRating2;
        TextView tvS_FirstYear2;
        TextView tvS_RepairCost2;
        TextView tvS_MainCaliber2;
        TextView tvS_MainCaliberReload2;
        TextView tvS_AUCaliber2;
        TextView tvS_AAACaliber2;
        TextView tvS_torpedoCount2;
        TextView tvS_torpedoMaxSpeed2;
        TextView tvS_torpedoExplosiveMass2;
        TextView tvS_ArmorTower2;
        TextView tvS_ArmorCitadel2;
        TextView tvS_MaxSpeed2;
        TextView tvS_MaxReverseSpeed2;
        TextView tvS_Acceleration2;
        TextView tvS_BrakingTime2;
        TextView tvS_TurnTime2;
        TextView tvS_Displacement2;
        TextView tvS_Crew2;

        ImageView ivS_MC_AP2;
        ImageView ivS_MC_APHE2;
        ImageView ivS_MC_HE2;
        ImageView ivS_MC_SAP2;
        ImageView ivS_MC_APCR2;
        ImageView ivS_MC_HEVT2;
        ImageView ivS_MC_HEDF2;
        ImageView ivS_MC_Shrapnel2;
        ImageView ivS_AU_AP2;
        ImageView ivS_AU_APHE2;
        ImageView ivS_AU_HE2;
        ImageView ivS_AU_SAP2;
        ImageView ivS_AU_APCR2;
        ImageView ivS_AU_HEVT2;
        ImageView ivS_AU_HEDF2;
        ImageView ivS_AU_Shrapnel2;
        ImageView ivS_AAA_AP2;
        ImageView ivS_AAA_APHE2;
        ImageView ivS_AAA_HE2;
        ImageView ivS_AAA_SAP2;
        ImageView ivS_AAA_APCR2;
        ImageView ivS_AAA_HEVT2;
        ImageView ivS_AAA_HEDF2;
        ImageView ivS_AAA_Shrapnel2;
        ImageView ivS_AirRadar2;
        ImageView ivS_ShipRadar2;
        ImageView ivS_DepthCharge2;
        ImageView ivS_Mine2;
        ImageView ivS_Rocket2;
        ImageView ivS_Torpedo2;

        #endregion

        #region Initialization non View values

        Context context;
        ArrayOfShips arrayOfShips;
        SearchView searchView;
        ListView listView;
        ShipAdapter shipAdapter;
        Dialog dialog;
        LayoutInflater inflater;
        View view;
        TextView.BufferType TextNormal;

        string s;
        string km_h;
        string br;
        string kg_s;
        string kg;
        string meters;
        string mm;
        string m_s;
        string h_p;
        string t;

        #endregion

        /// <summary>
        /// Base Android OnCreate method. Entry point for app
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization required elements
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ComparisonShipLayout);
            context = Application.Context;
            #endregion

            #region Ads initializer
            MobileAds.Initialize(context);
            var adView = FindViewById<AdView>(Resource.Id.adViewComparerShip);
            adView.LoadAd(new AdRequest.Builder().Build());
            //var requestbuilder = new AdRequest.Builder().AddTestDevice("46CCAB8BBCE5B5FFA79C22BEB98029AC");
            //adView.LoadAd(requestbuilder.Build());
            #endregion

            BindingInterfaceElementsToCode();
            FillListFromCacheAsync().ConfigureAwait(false);
            TopMenuInitializer();
            BottomMenuInitializer();
            LetsCompare();

            searchableShipButton1.Click += SearchableButton1_Click;
            searchableShipButton2.Click += SearchableButton2_Click;
        }

        /// <summary>
        /// Initialize Custom Top Navigation Menu
        /// </summary>
        private void TopMenuInitializer()
        {
            topMenuShipsButton.SetBackgroundResource(Resource.Drawable.ButtonAsTabShape);
            topMenuShipsButton.SetTextColor(Color.ParseColor("#dc3546"));
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
        /// Load cached List of ships
        /// </summary>
        private async Task FillListFromCacheAsync()
        {
            arrayOfShips = await BlobCache.UserAccount.GetObject<ArrayOfShips>("cachedArrayOfShips");
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
            searchView.SetQueryHint("HMS..");

            shipAdapter = new ShipAdapter(this, arrayOfShips.ShipsListApi);
            listView.Adapter = shipAdapter;
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
            shipAdapter.Filter.InvokeFilter(e.NewText);
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
        /// Set data from List ships to left part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToLeft(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(shipAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivS_Ship1);

             ivS_MC_AP1.Visibility = shipAdapter[e.Position].MC_AP ? ViewStates.Visible : ViewStates.Gone;
             ivS_MC_APHE1.Visibility = shipAdapter[e.Position].MC_APHE ? ViewStates.Visible : ViewStates.Gone;
             ivS_MC_HE1.Visibility = shipAdapter[e.Position].MC_HE ? ViewStates.Visible : ViewStates.Gone;
             ivS_MC_SAP1.Visibility = shipAdapter[e.Position].MC_SAP ? ViewStates.Visible : ViewStates.Gone;
             ivS_MC_APCR1.Visibility = shipAdapter[e.Position].MC_APCR ? ViewStates.Visible : ViewStates.Gone;
             ivS_MC_HEVT1.Visibility = shipAdapter[e.Position].MC_HEVT ? ViewStates.Visible : ViewStates.Gone;
             ivS_MC_HEDF1.Visibility = shipAdapter[e.Position].MC_HEDF ? ViewStates.Visible : ViewStates.Gone;
             ivS_MC_Shrapnel1.Visibility = shipAdapter[e.Position].MC_Shrapnel ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_AP1.Visibility = shipAdapter[e.Position].AU_AP ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_APHE1.Visibility = shipAdapter[e.Position].AU_APHE ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_HE1.Visibility = shipAdapter[e.Position].AU_HE ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_SAP1.Visibility = shipAdapter[e.Position].AU_SAP ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_APCR1.Visibility = shipAdapter[e.Position].AU_APCR ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_HEVT1.Visibility = shipAdapter[e.Position].AU_HEVT ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_HEDF1.Visibility = shipAdapter[e.Position].AU_HEDF ? ViewStates.Visible : ViewStates.Gone;
             ivS_AU_Shrapnel1.Visibility = shipAdapter[e.Position].AU_Shrapnel ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_AP1.Visibility = shipAdapter[e.Position].AAA_AP ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_APHE1.Visibility = shipAdapter[e.Position].AAA_APHE ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_HE1.Visibility = shipAdapter[e.Position].AAA_HE ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_SAP1.Visibility = shipAdapter[e.Position].AAA_SAP ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_APCR1.Visibility = shipAdapter[e.Position].AAA_APCR ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_HEVT1.Visibility = shipAdapter[e.Position].AAA_HEVT ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_HEDF1.Visibility = shipAdapter[e.Position].AAA_HEDF ? ViewStates.Visible : ViewStates.Gone;
             ivS_AAA_Shrapnel1.Visibility = shipAdapter[e.Position].AAA_Shrapnel ? ViewStates.Visible : ViewStates.Gone;
             ivS_AirRadar1.Visibility = shipAdapter[e.Position].IsHaveAirRadar ? ViewStates.Visible : ViewStates.Gone;
             ivS_ShipRadar1.Visibility = shipAdapter[e.Position].IsHaveShipRadar ? ViewStates.Visible : ViewStates.Gone;
             ivS_DepthCharge1.Visibility = shipAdapter[e.Position].IsHaveDepthCharge ? ViewStates.Visible : ViewStates.Gone;
             ivS_Mine1.Visibility = shipAdapter[e.Position].IsHaveMine ? ViewStates.Visible : ViewStates.Gone;
             ivS_Rocket1.Visibility = shipAdapter[e.Position].IsHaveRocket ? ViewStates.Visible : ViewStates.Gone;
             ivS_Torpedo1.Visibility = shipAdapter[e.Position].IsHaveTorpedo ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List ships to right part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToRight(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(shipAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivS_Ship2);

            ivS_MC_AP2.Visibility = shipAdapter[e.Position].MC_AP ? ViewStates.Visible : ViewStates.Gone;
            ivS_MC_APHE2.Visibility = shipAdapter[e.Position].MC_APHE ? ViewStates.Visible : ViewStates.Gone;
            ivS_MC_HE2.Visibility = shipAdapter[e.Position].MC_HE ? ViewStates.Visible : ViewStates.Gone;
            ivS_MC_SAP2.Visibility = shipAdapter[e.Position].MC_SAP ? ViewStates.Visible : ViewStates.Gone;
            ivS_MC_APCR2.Visibility = shipAdapter[e.Position].MC_APCR ? ViewStates.Visible : ViewStates.Gone;
            ivS_MC_HEVT2.Visibility = shipAdapter[e.Position].MC_HEVT ? ViewStates.Visible : ViewStates.Gone;
            ivS_MC_HEDF2.Visibility = shipAdapter[e.Position].MC_HEDF ? ViewStates.Visible : ViewStates.Gone;
            ivS_MC_Shrapnel2.Visibility = shipAdapter[e.Position].MC_Shrapnel ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_AP2.Visibility = shipAdapter[e.Position].AU_AP ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_APHE2.Visibility = shipAdapter[e.Position].AU_APHE ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_HE2.Visibility = shipAdapter[e.Position].AU_HE ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_SAP2.Visibility = shipAdapter[e.Position].AU_SAP ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_APCR2.Visibility = shipAdapter[e.Position].AU_APCR ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_HEVT2.Visibility = shipAdapter[e.Position].AU_HEVT ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_HEDF2.Visibility = shipAdapter[e.Position].AU_HEDF ? ViewStates.Visible : ViewStates.Gone;
            ivS_AU_Shrapnel2.Visibility = shipAdapter[e.Position].AU_Shrapnel ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_AP2.Visibility = shipAdapter[e.Position].AAA_AP ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_APHE2.Visibility = shipAdapter[e.Position].AAA_APHE ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_HE2.Visibility = shipAdapter[e.Position].AAA_HE ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_SAP2.Visibility = shipAdapter[e.Position].AAA_SAP ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_APCR2.Visibility = shipAdapter[e.Position].AAA_APCR ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_HEVT2.Visibility = shipAdapter[e.Position].AAA_HEVT ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_HEDF2.Visibility = shipAdapter[e.Position].AAA_HEDF ? ViewStates.Visible : ViewStates.Gone;
            ivS_AAA_Shrapnel2.Visibility = shipAdapter[e.Position].AAA_Shrapnel ? ViewStates.Visible : ViewStates.Gone;
            ivS_AirRadar2.Visibility = shipAdapter[e.Position].IsHaveAirRadar ? ViewStates.Visible : ViewStates.Gone;
            ivS_ShipRadar2.Visibility = shipAdapter[e.Position].IsHaveShipRadar ? ViewStates.Visible : ViewStates.Gone;
            ivS_DepthCharge2.Visibility = shipAdapter[e.Position].IsHaveDepthCharge ? ViewStates.Visible : ViewStates.Gone;
            ivS_Mine2.Visibility = shipAdapter[e.Position].IsHaveMine ? ViewStates.Visible : ViewStates.Gone;
            ivS_Rocket2.Visibility = shipAdapter[e.Position].IsHaveRocket ? ViewStates.Visible : ViewStates.Gone;
            ivS_Torpedo2.Visibility = shipAdapter[e.Position].IsHaveTorpedo ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List ships to left part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToLeft(AdapterView.ItemClickEventArgs e)
        {
            searchableShipButton1.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchableShipButton1.SetText(shipAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", shipAdapter[e.Position].BR);
            tvS_BattleRating1.SetText(br, TextNormal);
            tvS_FirstYear1.SetText(shipAdapter[e.Position].FirstLaunchYear + "", TextNormal);
            tvS_RepairCost1.SetText(shipAdapter[e.Position].RepairCost + "", TextNormal);

            tvS_MainCaliber1.SetText(shipAdapter[e.Position].MainCaliberName.Replace("<br>", "\n") + "", TextNormal);
            tvS_MainCaliberReload1.SetText(shipAdapter[e.Position].MainCaliberReload + s, TextNormal);
            tvS_AUCaliber1.SetText(shipAdapter[e.Position].AUCaliberName.Replace("<br>", "\n") + "", TextNormal);
            tvS_AAACaliber1.SetText(shipAdapter[e.Position].AACaliberName.Replace("<br>", "\n") + "", TextNormal);
            tvS_torpedoCount1.SetText(shipAdapter[e.Position].TorpedoItem + "", TextNormal);
            tvS_torpedoMaxSpeed1.SetText(shipAdapter[e.Position].TorpedoMaxSpeed + km_h, TextNormal);
            tvS_torpedoExplosiveMass1.SetText(shipAdapter[e.Position].TorpedoTNT + kg, TextNormal);
            tvS_ArmorTower1.SetText(shipAdapter[e.Position].ArmorTower + mm, TextNormal);
            tvS_ArmorCitadel1.SetText(shipAdapter[e.Position].ArmorHull + mm, TextNormal);
            tvS_MaxSpeed1.SetText(shipAdapter[e.Position].MaxSpeed + km_h, TextNormal);
            tvS_MaxReverseSpeed1.SetText(shipAdapter[e.Position].ReverseSpeed + km_h, TextNormal);
            tvS_Acceleration1.SetText(shipAdapter[e.Position].Acceleration + s, TextNormal);
            tvS_BrakingTime1.SetText(shipAdapter[e.Position].BrakingTime + s, TextNormal);
            tvS_TurnTime1.SetText(shipAdapter[e.Position].Turn360 + s, TextNormal);
            tvS_Displacement1.SetText(shipAdapter[e.Position].Displacement + t, TextNormal);
            tvS_Crew1.SetText(shipAdapter[e.Position].CrewCount + "", TextNormal);

        }

        /// <summary>
        /// Set data from List ships to right part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToRight(AdapterView.ItemClickEventArgs e)
        {
            searchableShipButton2.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchableShipButton2.SetText(shipAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", shipAdapter[e.Position].BR);
            tvS_BattleRating2.SetText(br, TextNormal);
            tvS_FirstYear2.SetText(shipAdapter[e.Position].FirstLaunchYear + "", TextNormal);
            tvS_RepairCost2.SetText(shipAdapter[e.Position].RepairCost + "", TextNormal);

            tvS_MainCaliber2.SetText(shipAdapter[e.Position].MainCaliberName.Replace("<br>", "\n") + "", TextNormal);
            tvS_MainCaliberReload2.SetText(shipAdapter[e.Position].MainCaliberReload + s, TextNormal);
            tvS_AUCaliber2.SetText(shipAdapter[e.Position].AUCaliberName.Replace("<br>", "\n") + "", TextNormal);
            tvS_AAACaliber2.SetText(shipAdapter[e.Position].AACaliberName.Replace("<br>", "\n") + "", TextNormal);
            tvS_torpedoCount2.SetText(shipAdapter[e.Position].TorpedoItem + "", TextNormal);
            tvS_torpedoMaxSpeed2.SetText(shipAdapter[e.Position].TorpedoMaxSpeed + km_h, TextNormal);
            tvS_torpedoExplosiveMass2.SetText(shipAdapter[e.Position].TorpedoTNT + kg, TextNormal);
            tvS_ArmorTower2.SetText(shipAdapter[e.Position].ArmorTower + mm, TextNormal);
            tvS_ArmorCitadel2.SetText(shipAdapter[e.Position].ArmorHull + mm, TextNormal);
            tvS_MaxSpeed2.SetText(shipAdapter[e.Position].MaxSpeed + km_h, TextNormal);
            tvS_MaxReverseSpeed2.SetText(shipAdapter[e.Position].ReverseSpeed + km_h, TextNormal);
            tvS_Acceleration2.SetText(shipAdapter[e.Position].Acceleration + s, TextNormal);
            tvS_BrakingTime2.SetText(shipAdapter[e.Position].BrakingTime + s, TextNormal);
            tvS_TurnTime2.SetText(shipAdapter[e.Position].Turn360 + s, TextNormal);
            tvS_Displacement2.SetText(shipAdapter[e.Position].Displacement + t, TextNormal);
            tvS_Crew2.SetText(shipAdapter[e.Position].CrewCount + "", TextNormal);
        }

        /// <summary>
        /// Describes all strings to compare
        /// </summary>
        private void LetsCompare()
        {
            var comparer = new CompareHelper();

            comparer.CompareWhenLowIsGood(tvS_RepairCost1, tvS_RepairCost2);
            comparer.CompareWhenHighIsGood(tvS_FirstYear1, tvS_FirstYear2);
            comparer.CompareWhenLowIsGood(tvS_BattleRating1, tvS_BattleRating2);
            comparer.CompareWhenLowIsGood(tvS_MainCaliberReload1, tvS_MainCaliberReload2);
            comparer.CompareWhenHighIsGood(tvS_torpedoCount1, tvS_torpedoCount2);
            comparer.CompareWhenHighIsGood(tvS_torpedoMaxSpeed1, tvS_torpedoMaxSpeed2);
            comparer.CompareWhenHighIsGood(tvS_torpedoExplosiveMass1, tvS_torpedoExplosiveMass2);
            comparer.CompareWhenHighIsGood(tvS_ArmorTower1, tvS_ArmorTower2);
            comparer.CompareWhenHighIsGood(tvS_ArmorCitadel1, tvS_ArmorCitadel2);
            comparer.CompareWhenHighIsGood(tvS_MaxSpeed1, tvS_MaxSpeed2);
            comparer.CompareWhenHighIsGood(tvS_MaxReverseSpeed1, tvS_MaxReverseSpeed2);
            comparer.CompareWhenLowIsGood(tvS_Acceleration1, tvS_Acceleration2); 
            comparer.CompareWhenLowIsGood(tvS_BrakingTime1, tvS_BrakingTime2);
            comparer.CompareWhenLowIsGood(tvS_TurnTime1, tvS_TurnTime2);
            comparer.CompareWhenHighIsGood(tvS_Displacement1, tvS_Displacement2);
            comparer.CompareWhenHighIsGood(tvS_Crew1, tvS_Crew2);
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
                    var intentPlane = new Intent(this, typeof(ComparisonPlaneActivity));
                    intentPlane.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentPlane);
                    break;
                case Resource.Id.topMenuTanksButton:
                    var intentTank = new Intent(this, typeof(ComparisonTankActivity));
                    intentTank.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentTank);
                    break;
                case Resource.Id.topMenuHeliButton:
                    var intentHeli = new Intent(this, typeof(ComparisonHeliActivity));
                    intentHeli.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentHeli);
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
            s = context.Resources.GetString(Resource.String.s);
            km_h = context.Resources.GetString(Resource.String.km_h);
            kg_s = context.Resources.GetString(Resource.String.kg_s);
            kg = context.Resources.GetString(Resource.String.kg);
            meters = context.Resources.GetString(Resource.String.meters);
            mm = context.Resources.GetString(Resource.String.mm);
            m_s = context.Resources.GetString(Resource.String.m_s);
            h_p = context.Resources.GetString(Resource.String.h_p);
            t = context.Resources.GetString(Resource.String.t);

            ivS_Ship1 = FindViewById<ImageView>(Resource.Id.ivS_Ship1);
            ivS_Ship2 = FindViewById<ImageView>(Resource.Id.ivS_Ship2);
            searchableShipButton1 = FindViewById<Button>(Resource.Id.searchableShipButton1);
            searchableShipButton2 = FindViewById<Button>(Resource.Id.searchableShipButton2);
            topMenuAircraftsButton = FindViewById<Button>(Resource.Id.topMenuAircraftsButton);
            topMenuTanksButton = FindViewById<Button>(Resource.Id.topMenuTanksButton);
            topMenuHeliButton = FindViewById<Button>(Resource.Id.topMenuHeliButton);
            topMenuShipsButton = FindViewById<Button>(Resource.Id.topMenuShipsButton);

            tvS_BattleRating1 = FindViewById<TextView>(Resource.Id.tvS_BattleRating1);
            tvS_BattleRating2 = FindViewById<TextView>(Resource.Id.tvS_BattleRating2);
            tvS_FirstYear1 = FindViewById<TextView>(Resource.Id.tvS_FirstYear1);
            tvS_FirstYear2 = FindViewById<TextView>(Resource.Id.tvS_FirstYear2);
            tvS_RepairCost1 = FindViewById<TextView>(Resource.Id.tvS_RepairCost1);
            tvS_RepairCost2 = FindViewById<TextView>(Resource.Id.tvS_RepairCost2);

            tvS_MainCaliber1 = FindViewById<TextView>(Resource.Id.tvS_MainCaliber1);
            tvS_MainCaliberReload1 = FindViewById<TextView>(Resource.Id.tvS_MainCaliberReload1);
            tvS_AUCaliber1 = FindViewById<TextView>(Resource.Id.tvS_AUCaliber1);
            tvS_AAACaliber1 = FindViewById<TextView>(Resource.Id.tvS_AAACaliber1);
            tvS_torpedoCount1 = FindViewById<TextView>(Resource.Id.tvS_torpedoCount1);
            tvS_torpedoMaxSpeed1 = FindViewById<TextView>(Resource.Id.tvS_torpedoMaxSpeed1);
            tvS_torpedoExplosiveMass1 = FindViewById<TextView>(Resource.Id.tvS_torpedoExplosiveMass1);
            tvS_ArmorTower1 = FindViewById<TextView>(Resource.Id.tvS_ArmorTower1);
            tvS_ArmorCitadel1 = FindViewById<TextView>(Resource.Id.tvS_ArmorCitadel1);
            tvS_MaxSpeed1 = FindViewById<TextView>(Resource.Id.tvS_MaxSpeed1);
            tvS_MaxReverseSpeed1 = FindViewById<TextView>(Resource.Id.tvS_MaxReverseSpeed1);
            tvS_Acceleration1 = FindViewById<TextView>(Resource.Id.tvS_Acceleration1);
            tvS_BrakingTime1 = FindViewById<TextView>(Resource.Id.tvS_BrakingTime1);
            tvS_TurnTime1 = FindViewById<TextView>(Resource.Id.tvS_TurnTime1);
            tvS_Displacement1 = FindViewById<TextView>(Resource.Id.tvS_Displacement1);
            tvS_Crew1 = FindViewById<TextView>(Resource.Id.tvS_Crew1);

            ivS_MC_AP1 = FindViewById<ImageView>(Resource.Id.ivS_MC_AP1);
            ivS_MC_APHE1 = FindViewById<ImageView>(Resource.Id.ivS_MC_APHE1);
            ivS_MC_HE1 = FindViewById<ImageView>(Resource.Id.ivS_MC_HE1);
            ivS_MC_SAP1 = FindViewById<ImageView>(Resource.Id.ivS_MC_SAP1);
            ivS_MC_APCR1 = FindViewById<ImageView>(Resource.Id.ivS_MC_APCR1);
            ivS_MC_HEVT1 = FindViewById<ImageView>(Resource.Id.ivS_MC_HEVT1);
            ivS_MC_HEDF1 = FindViewById<ImageView>(Resource.Id.ivS_MC_HEDF1);
            ivS_MC_Shrapnel1 = FindViewById<ImageView>(Resource.Id.ivS_MC_Shrapnel1);
            ivS_AU_AP1 = FindViewById<ImageView>(Resource.Id.ivS_AU_AP1);
            ivS_AU_APHE1 = FindViewById<ImageView>(Resource.Id.ivS_AU_APHE1);
            ivS_AU_HE1 = FindViewById<ImageView>(Resource.Id.ivS_AU_HE1);
            ivS_AU_SAP1 = FindViewById<ImageView>(Resource.Id.ivS_AU_SAP1);
            ivS_AU_APCR1 = FindViewById<ImageView>(Resource.Id.ivS_AU_APCR1);
            ivS_AU_HEVT1 = FindViewById<ImageView>(Resource.Id.ivS_AU_HEVT1);
            ivS_AU_HEDF1 = FindViewById<ImageView>(Resource.Id.ivS_AU_HEDF1);
            ivS_AU_Shrapnel1 = FindViewById<ImageView>(Resource.Id.ivS_AU_Shrapnel1);
            ivS_AAA_AP1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_AP1);
            ivS_AAA_APHE1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_APHE1);
            ivS_AAA_HE1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_HE1);
            ivS_AAA_SAP1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_SAP1);
            ivS_AAA_APCR1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_APCR1);
            ivS_AAA_HEVT1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_HEVT1);
            ivS_AAA_HEDF1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_HEDF1);
            ivS_AAA_Shrapnel1 = FindViewById<ImageView>(Resource.Id.ivS_AAA_Shrapnel1);
            ivS_AirRadar1 = FindViewById<ImageView>(Resource.Id.ivS_AirRadar1);
            ivS_ShipRadar1 = FindViewById<ImageView>(Resource.Id.ivS_ShipRadar1);
            ivS_DepthCharge1 = FindViewById<ImageView>(Resource.Id.ivS_DepthCharge1);
            ivS_Mine1 = FindViewById<ImageView>(Resource.Id.ivS_Mine1);
            ivS_Rocket1 = FindViewById<ImageView>(Resource.Id.ivS_Rocket1);
            ivS_Torpedo1 = FindViewById<ImageView>(Resource.Id.ivS_Torpedo1);

            tvS_MainCaliber2 = FindViewById<TextView>(Resource.Id.tvS_MainCaliber2);
            tvS_MainCaliberReload2 = FindViewById<TextView>(Resource.Id.tvS_MainCaliberReload2);
            tvS_AUCaliber2 = FindViewById<TextView>(Resource.Id.tvS_AUCaliber2);
            tvS_AAACaliber2 = FindViewById<TextView>(Resource.Id.tvS_AAACaliber2);
            tvS_torpedoCount2 = FindViewById<TextView>(Resource.Id.tvS_torpedoCount2);
            tvS_torpedoMaxSpeed2 = FindViewById<TextView>(Resource.Id.tvS_torpedoMaxSpeed2);
            tvS_torpedoExplosiveMass2 = FindViewById<TextView>(Resource.Id.tvS_torpedoExplosiveMass2);
            tvS_ArmorTower2 = FindViewById<TextView>(Resource.Id.tvS_ArmorTower2);
            tvS_ArmorCitadel2 = FindViewById<TextView>(Resource.Id.tvS_ArmorCitadel2);
            tvS_MaxSpeed2 = FindViewById<TextView>(Resource.Id.tvS_MaxSpeed2);
            tvS_MaxReverseSpeed2 = FindViewById<TextView>(Resource.Id.tvS_MaxReverseSpeed2);
            tvS_Acceleration2 = FindViewById<TextView>(Resource.Id.tvS_Acceleration2);
            tvS_BrakingTime2 = FindViewById<TextView>(Resource.Id.tvS_BrakingTime2);
            tvS_TurnTime2 = FindViewById<TextView>(Resource.Id.tvS_TurnTime2);
            tvS_Displacement2 = FindViewById<TextView>(Resource.Id.tvS_Displacement2);
            tvS_Crew2 = FindViewById<TextView>(Resource.Id.tvS_Crew2);

            ivS_MC_AP2 = FindViewById<ImageView>(Resource.Id.ivS_MC_AP2);
            ivS_MC_APHE2 = FindViewById<ImageView>(Resource.Id.ivS_MC_APHE2);
            ivS_MC_HE2 = FindViewById<ImageView>(Resource.Id.ivS_MC_HE2);
            ivS_MC_SAP2 = FindViewById<ImageView>(Resource.Id.ivS_MC_SAP2);
            ivS_MC_APCR2 = FindViewById<ImageView>(Resource.Id.ivS_MC_APCR2);
            ivS_MC_HEVT2 = FindViewById<ImageView>(Resource.Id.ivS_MC_HEVT2);
            ivS_MC_HEDF2 = FindViewById<ImageView>(Resource.Id.ivS_MC_HEDF2);
            ivS_MC_Shrapnel2 = FindViewById<ImageView>(Resource.Id.ivS_MC_Shrapnel2);
            ivS_AU_AP2 = FindViewById<ImageView>(Resource.Id.ivS_AU_AP2);
            ivS_AU_APHE2 = FindViewById<ImageView>(Resource.Id.ivS_AU_APHE2);
            ivS_AU_HE2 = FindViewById<ImageView>(Resource.Id.ivS_AU_HE2);
            ivS_AU_SAP2 = FindViewById<ImageView>(Resource.Id.ivS_AU_SAP2);
            ivS_AU_APCR2 = FindViewById<ImageView>(Resource.Id.ivS_AU_APCR2);
            ivS_AU_HEVT2 = FindViewById<ImageView>(Resource.Id.ivS_AU_HEVT2);
            ivS_AU_HEDF2 = FindViewById<ImageView>(Resource.Id.ivS_AU_HEDF2);
            ivS_AU_Shrapnel2 = FindViewById<ImageView>(Resource.Id.ivS_AU_Shrapnel2);
            ivS_AAA_AP2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_AP2);
            ivS_AAA_APHE2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_APHE2);
            ivS_AAA_HE2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_HE2);
            ivS_AAA_SAP2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_SAP2);
            ivS_AAA_APCR2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_APCR2);
            ivS_AAA_HEVT2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_HEVT2);
            ivS_AAA_HEDF2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_HEDF2);
            ivS_AAA_Shrapnel2 = FindViewById<ImageView>(Resource.Id.ivS_AAA_Shrapnel2);
            ivS_AirRadar2 = FindViewById<ImageView>(Resource.Id.ivS_AirRadar2);
            ivS_ShipRadar2 = FindViewById<ImageView>(Resource.Id.ivS_ShipRadar2);
            ivS_DepthCharge2 = FindViewById<ImageView>(Resource.Id.ivS_DepthCharge2);
            ivS_Mine2 = FindViewById<ImageView>(Resource.Id.ivS_Mine2);
            ivS_Rocket2 = FindViewById<ImageView>(Resource.Id.ivS_Rocket2);
            ivS_Torpedo2 = FindViewById<ImageView>(Resource.Id.ivS_Torpedo2);
        }
    }
}
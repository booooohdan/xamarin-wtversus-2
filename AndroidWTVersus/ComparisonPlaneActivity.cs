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
    public class ComparisonPlaneActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {
        #region initialization View values

        ImageView ivP_Plane1;
        ImageView ivP_Plane2;

        Button searchablePlaneButton1;
        Button searchablePlaneButton2;
        Button topMenuAircraftsButton;
        Button topMenuTanksButton;
        Button topMenuHeliButton;
        Button topMenuShipsButton;

        TextView tvP_BattleRating1;
        TextView tvP_BattleRating2;
        TextView tvP_FirstYear1;
        TextView tvP_FirstYear2;
        TextView tvP_RepairCost1;
        TextView tvP_RepairCost2;

        ImageView ivP_ASMissile1;
        ImageView ivP_AAMissile1;
        ImageView ivP_AGMissile1;
        ImageView ivP_HBomb1;
        ImageView ivP_HCannon1;
        ImageView ivP_HTorpedo1;
        ImageView ivP_WrongMusic1;
        ImageView ivP_ASMissile2;
        ImageView ivP_AAMissile2;
        ImageView ivP_AGMissile2;
        ImageView ivP_HBomb2;
        ImageView ivP_HCannon2;
        ImageView ivP_HTorpedo2;
        ImageView ivP_WrongMusic2;

        TextView tvP_Cannon1;
        TextView tvP_Cannon2;
        TextView tvP_BurstMass1;
        TextView tvP_BurstMass2;
        TextView tvP_BombsPayload1;
        TextView tvP_BombsPayload2;

        ImageView ivP_RWR1;
        ImageView ivP_Turrel1;
        ImageView ivP_Flares1;
        ImageView ivP_Parachute1;
        ImageView ivP_AirBrake1;
        ImageView ivP_AirRadar1;
        ImageView ivP_Tailhook1;
        ImageView ivP_GroundRadar1;
        ImageView ivP_CCIP1;
        ImageView ivP_CCRP1;
        ImageView ivP_GSuit1;
        ImageView ivP_RWR2;
        ImageView ivP_Turrel2;
        ImageView ivP_Flares2;
        ImageView ivP_Parachute2;
        ImageView ivP_AirBrake2;
        ImageView ivP_AirRadar2;
        ImageView ivP_Tailhook2;
        ImageView ivP_GroundRadar2;
        ImageView ivP_CCIP2;
        ImageView ivP_CCRP2;
        ImageView ivP_GSuit2;

        TextView tvP_MaxSpeedAt0m1;
        TextView tvP_MaxSpeedAt0m2;
        TextView tvP_MaxSpeedAt5000m1;
        TextView tvP_MaxSpeedAt5000m2;
        TextView tvP_Climb1;
        TextView tvP_Climb2;
        TextView tvP_TurnTime1;
        TextView tvP_TurnTime2;
        TextView tvP_EnginePower1;
        TextView tvP_EnginePower2;
        TextView tvP_TakeOffWeight1;
        TextView tvP_TakeOffWeight2;
        TextView tvP_Flutter1;
        TextView tvP_Flutter2;
        TextView tvP_OptimalAltitude1;
        TextView tvP_OptimalAltitude2;
        TextView tvP_OptimalElevator1;
        TextView tvP_OptimalElevator2;
        TextView tvP_OptimalAilerons1;
        TextView tvP_OptimalAilerons2;

        #endregion

        #region Initialization non View values

        Context context;
        ArrayOfPlanes arrayOfPlanes;
        SearchView searchView;
        ListView listView;
        PlaneAdapter planeAdapter;
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
        #endregion

        /// <summary>
        /// Base Android OnCreate method. Entry point for app
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization required elements
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ComparisonPlaneLayout);
            context = Application.Context;
            #endregion

            #region Ads initializer
            MobileAds.Initialize(context);
            var adView = FindViewById<AdView>(Resource.Id.adViewComparerPlane);
            adView.LoadAd(new AdRequest.Builder().Build());
            //var requestbuilder = new AdRequest.Builder().AddTestDevice("46CCAB8BBCE5B5FFA79C22BEB98029AC");
            //adView.LoadAd(requestbuilder.Build());
            #endregion

            BindingInterfaceElementsToCode();
            FillListFromCacheAsync().ConfigureAwait(false);
            TopMenuInitializer();
            BottomMenuInitializer();
            LetsCompare();

            searchablePlaneButton1.Click += SearchableButton1_Click;
            searchablePlaneButton2.Click += SearchableButton2_Click;
        }

        /// <summary>
        /// Initialize Custom Top Navigation Menu
        /// </summary>
        private void TopMenuInitializer()
        {
            topMenuAircraftsButton.SetBackgroundResource(Resource.Drawable.ButtonAsTabShape);
            topMenuAircraftsButton.SetTextColor(Color.ParseColor("#dc3546"));
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
        /// Load cached List of planes
        /// </summary>
        private async Task FillListFromCacheAsync()
        {
            arrayOfPlanes = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");
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
            searchView.SetQueryHint("F-4E..");

            planeAdapter = new PlaneAdapter(this, arrayOfPlanes.PlanesListApi);
            listView.Adapter = planeAdapter;
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
            planeAdapter.Filter.InvokeFilter(e.NewText);
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
        /// Set data from List planes to left part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToLeft(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(planeAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivP_Plane1);

            ivP_ASMissile1.Visibility = planeAdapter[e.Position].ASMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_AAMissile1.Visibility = planeAdapter[e.Position].AAMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_AGMissile1.Visibility = planeAdapter[e.Position].AGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_HBomb1.Visibility = planeAdapter[e.Position].HBomb ? ViewStates.Visible : ViewStates.Gone;
            ivP_HCannon1.Visibility = planeAdapter[e.Position].HCannon ? ViewStates.Visible : ViewStates.Gone;
            ivP_HTorpedo1.Visibility = planeAdapter[e.Position].HTorpedo ? ViewStates.Visible : ViewStates.Gone;
            ivP_WrongMusic1.Visibility = planeAdapter[e.Position].WrongMusic ? ViewStates.Visible : ViewStates.Gone;
            ivP_RWR1.Visibility = planeAdapter[e.Position].RWR ? ViewStates.Visible : ViewStates.Gone;
            ivP_Turrel1.Visibility = planeAdapter[e.Position].Turrel ? ViewStates.Visible : ViewStates.Gone;
            ivP_Flares1.Visibility = planeAdapter[e.Position].Flares ? ViewStates.Visible : ViewStates.Gone;
            ivP_Parachute1.Visibility = planeAdapter[e.Position].Parachute ? ViewStates.Visible : ViewStates.Gone;
            ivP_AirBrake1.Visibility = planeAdapter[e.Position].AirBrake ? ViewStates.Visible : ViewStates.Gone;
            ivP_AirRadar1.Visibility = planeAdapter[e.Position].AirRadar ? ViewStates.Visible : ViewStates.Gone;
            ivP_Tailhook1.Visibility = planeAdapter[e.Position].Tailhook ? ViewStates.Visible : ViewStates.Gone;
            ivP_GroundRadar1.Visibility = planeAdapter[e.Position].GroundRadar ? ViewStates.Visible : ViewStates.Gone;
            ivP_CCIP1.Visibility = planeAdapter[e.Position].CCIP ? ViewStates.Visible : ViewStates.Gone;
            ivP_CCRP1.Visibility = planeAdapter[e.Position].CCRP ? ViewStates.Visible : ViewStates.Gone;
            ivP_GSuit1.Visibility = planeAdapter[e.Position].GSuit ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List planes to right part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToRight(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(planeAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivP_Plane2);

            ivP_ASMissile2.Visibility = planeAdapter[e.Position].ASMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_AAMissile2.Visibility = planeAdapter[e.Position].AAMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_AGMissile2.Visibility = planeAdapter[e.Position].AGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_HBomb2.Visibility = planeAdapter[e.Position].HBomb ? ViewStates.Visible : ViewStates.Gone;
            ivP_HCannon2.Visibility = planeAdapter[e.Position].HCannon ? ViewStates.Visible : ViewStates.Gone;
            ivP_HTorpedo2.Visibility = planeAdapter[e.Position].HTorpedo ? ViewStates.Visible : ViewStates.Gone;
            ivP_WrongMusic2.Visibility = planeAdapter[e.Position].WrongMusic ? ViewStates.Visible : ViewStates.Gone;
            ivP_RWR2.Visibility = planeAdapter[e.Position].RWR ? ViewStates.Visible : ViewStates.Gone;
            ivP_Turrel2.Visibility = planeAdapter[e.Position].Turrel ? ViewStates.Visible : ViewStates.Gone;
            ivP_Flares2.Visibility = planeAdapter[e.Position].Flares ? ViewStates.Visible : ViewStates.Gone;
            ivP_Parachute2.Visibility = planeAdapter[e.Position].Parachute ? ViewStates.Visible : ViewStates.Gone;
            ivP_AirBrake2.Visibility = planeAdapter[e.Position].AirBrake ? ViewStates.Visible : ViewStates.Gone;
            ivP_AirRadar2.Visibility = planeAdapter[e.Position].AirRadar ? ViewStates.Visible : ViewStates.Gone;
            ivP_Tailhook2.Visibility = planeAdapter[e.Position].Tailhook ? ViewStates.Visible : ViewStates.Gone;
            ivP_GroundRadar2.Visibility = planeAdapter[e.Position].GroundRadar ? ViewStates.Visible : ViewStates.Gone;
            ivP_CCIP2.Visibility = planeAdapter[e.Position].CCIP ? ViewStates.Visible : ViewStates.Gone;
            ivP_CCRP2.Visibility = planeAdapter[e.Position].CCRP ? ViewStates.Visible : ViewStates.Gone;
            ivP_GSuit2.Visibility = planeAdapter[e.Position].GSuit ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List planes to left part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToLeft(AdapterView.ItemClickEventArgs e)
        {
            searchablePlaneButton1.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchablePlaneButton1.SetText(planeAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", planeAdapter[e.Position].BR);
            tvP_BattleRating1.SetText(br, TextNormal);
            tvP_FirstYear1.SetText(planeAdapter[e.Position].FirstFlyYear + "", TextNormal);
            tvP_RepairCost1.SetText(planeAdapter[e.Position].RepairCost + "", TextNormal);

            tvP_Cannon1.SetText(planeAdapter[e.Position].CourseWeapon.Replace("<br>", "\n") + "", TextNormal);
            tvP_BurstMass1.SetText(planeAdapter[e.Position].BurstMass + kg_s, TextNormal);
            tvP_BombsPayload1.SetText(planeAdapter[e.Position].BombLoad + kg, TextNormal);
            tvP_MaxSpeedAt0m1.SetText(planeAdapter[e.Position].MaxSpeedAt0 + km_h, TextNormal);
            tvP_MaxSpeedAt5000m1.SetText(planeAdapter[e.Position].MaxSpeedAt5000 + km_h, TextNormal);
            tvP_Climb1.SetText(planeAdapter[e.Position].Climb + s, TextNormal);
            tvP_TurnTime1.SetText(planeAdapter[e.Position].TurnAt0 + s, TextNormal);
            tvP_EnginePower1.SetText(planeAdapter[e.Position].EnginePower.ToString(), TextNormal);
            tvP_TakeOffWeight1.SetText(planeAdapter[e.Position].Weight + kg, TextNormal);
            tvP_Flutter1.SetText(planeAdapter[e.Position].Flutter + km_h, TextNormal);
            tvP_OptimalAltitude1.SetText(planeAdapter[e.Position].OptimalAlitude + meters, TextNormal);
            tvP_OptimalElevator1.SetText(planeAdapter[e.Position].OptimalElevator + km_h, TextNormal);
            tvP_OptimalAilerons1.SetText(planeAdapter[e.Position].OptimalAilerons + km_h, TextNormal);
        }

        /// <summary>
        /// Set data from List planes to right part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToRight(AdapterView.ItemClickEventArgs e)
        {
            searchablePlaneButton2.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchablePlaneButton2.SetText(planeAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", planeAdapter[e.Position].BR);
            tvP_BattleRating2.SetText(br, TextNormal);
            tvP_FirstYear2.SetText(planeAdapter[e.Position].FirstFlyYear + "", TextNormal);
            tvP_RepairCost2.SetText(planeAdapter[e.Position].RepairCost + "", TextNormal);

            tvP_Cannon2.SetText(planeAdapter[e.Position].CourseWeapon.Replace("<br>", "\n") + "", TextNormal);
            tvP_BurstMass2.SetText(planeAdapter[e.Position].BurstMass + kg_s, TextNormal);
            tvP_BombsPayload2.SetText(planeAdapter[e.Position].BombLoad + kg, TextNormal);
            tvP_MaxSpeedAt0m2.SetText(planeAdapter[e.Position].MaxSpeedAt0 + km_h, TextNormal);
            tvP_MaxSpeedAt5000m2.SetText(planeAdapter[e.Position].MaxSpeedAt5000 + km_h, TextNormal);
            tvP_Climb2.SetText(planeAdapter[e.Position].Climb + s, TextNormal);
            tvP_TurnTime2.SetText(planeAdapter[e.Position].TurnAt0 + s, TextNormal);
            tvP_EnginePower2.SetText(planeAdapter[e.Position].EnginePower.ToString(), TextNormal);
            tvP_TakeOffWeight2.SetText(planeAdapter[e.Position].Weight + kg, TextNormal);
            tvP_Flutter2.SetText(planeAdapter[e.Position].Flutter + km_h, TextNormal);
            tvP_OptimalAltitude2.SetText(planeAdapter[e.Position].OptimalAlitude + meters, TextNormal);
            tvP_OptimalElevator2.SetText(planeAdapter[e.Position].OptimalElevator + km_h, TextNormal);
            tvP_OptimalAilerons2.SetText(planeAdapter[e.Position].OptimalAilerons + km_h, TextNormal);
        }

        /// <summary>
        /// Describes all strings to compare
        /// </summary>
        private void LetsCompare()
        {
            var comparer = new CompareHelper();

            comparer.CompareWhenLowIsGood(tvP_RepairCost1, tvP_RepairCost2);
            comparer.CompareWhenHighIsGood(tvP_FirstYear1, tvP_FirstYear2);
            comparer.CompareWhenLowIsGood(tvP_BattleRating1, tvP_BattleRating2);
            comparer.CompareWhenHighIsGood(tvP_BurstMass1, tvP_BurstMass2);
            comparer.CompareWhenHighIsGood(tvP_BombsPayload1, tvP_BombsPayload2);
            comparer.CompareWhenHighIsGood(tvP_MaxSpeedAt0m1, tvP_MaxSpeedAt0m2);
            comparer.CompareWhenHighIsGood(tvP_MaxSpeedAt5000m1, tvP_MaxSpeedAt5000m2);
            comparer.CompareWhenLowIsGood(tvP_Climb1, tvP_Climb2);
            comparer.CompareWhenLowIsGood(tvP_TurnTime1, tvP_TurnTime2);
            comparer.CompareWhenHighIsGood(tvP_EnginePower1, tvP_EnginePower2);
            comparer.CompareWhenLowIsGood(tvP_TakeOffWeight1, tvP_TakeOffWeight2);
            comparer.CompareWhenHighIsGood(tvP_Flutter1, tvP_Flutter2);
            comparer.CompareWhenHighIsGood(tvP_OptimalAltitude1, tvP_OptimalAltitude2);
            comparer.CompareWhenHighIsGood(tvP_OptimalElevator1, tvP_OptimalElevator2);
            comparer.CompareWhenHighIsGood(tvP_OptimalAilerons1, tvP_OptimalAilerons2);
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

            ivP_Plane1 = FindViewById<ImageView>(Resource.Id.ivP_Plane1);
            ivP_Plane2 = FindViewById<ImageView>(Resource.Id.ivP_Plane2);
            searchablePlaneButton1 = FindViewById<Button>(Resource.Id.searchablePlaneButton1);
            searchablePlaneButton2 = FindViewById<Button>(Resource.Id.searchablePlaneButton2);
            topMenuAircraftsButton = FindViewById<Button>(Resource.Id.topMenuAircraftsButton);
            topMenuTanksButton = FindViewById<Button>(Resource.Id.topMenuTanksButton);
            topMenuHeliButton = FindViewById<Button>(Resource.Id.topMenuHeliButton);
            topMenuShipsButton = FindViewById<Button>(Resource.Id.topMenuShipsButton);

            tvP_BattleRating1 = FindViewById<TextView>(Resource.Id.tvP_BattleRating1);
            tvP_BattleRating2 = FindViewById<TextView>(Resource.Id.tvP_BattleRating2);
            tvP_FirstYear1 = FindViewById<TextView>(Resource.Id.tvP_FirstYear1);
            tvP_FirstYear2 = FindViewById<TextView>(Resource.Id.tvP_FirstYear2);
            tvP_RepairCost1 = FindViewById<TextView>(Resource.Id.tvP_RepairCost1);
            tvP_RepairCost2 = FindViewById<TextView>(Resource.Id.tvP_RepairCost2);
            tvP_Cannon1 = FindViewById<TextView>(Resource.Id.tvP_Cannon1);
            tvP_Cannon2 = FindViewById<TextView>(Resource.Id.tvP_Cannon2);

            tvP_BurstMass1 = FindViewById<TextView>(Resource.Id.tvP_BurstMass1);
            tvP_BombsPayload1 = FindViewById<TextView>(Resource.Id.tvP_BombsPayload1);
            tvP_MaxSpeedAt0m1 = FindViewById<TextView>(Resource.Id.tvP_MaxSpeedAt0m1);
            tvP_MaxSpeedAt5000m1 = FindViewById<TextView>(Resource.Id.tvP_MaxSpeedAt5000m1);
            tvP_Climb1 = FindViewById<TextView>(Resource.Id.tvP_Climb1);
            tvP_TurnTime1 = FindViewById<TextView>(Resource.Id.tvP_TurnTime1);
            tvP_EnginePower1 = FindViewById<TextView>(Resource.Id.tvP_EnginePower1);
            tvP_TakeOffWeight1 = FindViewById<TextView>(Resource.Id.tvP_TakeOffWeight1);
            tvP_Flutter1 = FindViewById<TextView>(Resource.Id.tvP_Flutter1);
            tvP_OptimalAltitude1 = FindViewById<TextView>(Resource.Id.tvP_OptimalAltitude1);
            tvP_OptimalElevator1 = FindViewById<TextView>(Resource.Id.tvP_OptimalElevator1);
            tvP_OptimalAilerons1 = FindViewById<TextView>(Resource.Id.tvP_OptimalAilerons1);
            tvP_BurstMass2 = FindViewById<TextView>(Resource.Id.tvP_BurstMass2);
            tvP_BombsPayload2 = FindViewById<TextView>(Resource.Id.tvP_BombsPayload2);
            tvP_MaxSpeedAt0m2 = FindViewById<TextView>(Resource.Id.tvP_MaxSpeedAt0m2);
            tvP_MaxSpeedAt5000m2 = FindViewById<TextView>(Resource.Id.tvP_MaxSpeedAt5000m2);
            tvP_Climb2 = FindViewById<TextView>(Resource.Id.tvP_Climb2);
            tvP_TurnTime2 = FindViewById<TextView>(Resource.Id.tvP_TurnTime2);
            tvP_EnginePower2 = FindViewById<TextView>(Resource.Id.tvP_EnginePower2);
            tvP_TakeOffWeight2 = FindViewById<TextView>(Resource.Id.tvP_TakeOffWeight2);
            tvP_Flutter2 = FindViewById<TextView>(Resource.Id.tvP_Flutter2);
            tvP_OptimalAltitude2 = FindViewById<TextView>(Resource.Id.tvP_OptimalAltitude2);
            tvP_OptimalElevator2 = FindViewById<TextView>(Resource.Id.tvP_OptimalElevator2);
            tvP_OptimalAilerons2 = FindViewById<TextView>(Resource.Id.tvP_OptimalAilerons2);

            ivP_ASMissile1 = FindViewById<ImageView>(Resource.Id.ivP_ASMissile1);
            ivP_AAMissile1 = FindViewById<ImageView>(Resource.Id.ivP_AAMissile1);
            ivP_AGMissile1 = FindViewById<ImageView>(Resource.Id.ivP_AGMissile1);
            ivP_HBomb1 = FindViewById<ImageView>(Resource.Id.ivP_HBomb1);
            ivP_HCannon1 = FindViewById<ImageView>(Resource.Id.ivP_HCannon1);
            ivP_HTorpedo1 = FindViewById<ImageView>(Resource.Id.ivP_HTorpedo1);
            ivP_WrongMusic1 = FindViewById<ImageView>(Resource.Id.ivP_WrongMusic1);
            ivP_RWR1 = FindViewById<ImageView>(Resource.Id.ivP_RWR1);
            ivP_Turrel1 = FindViewById<ImageView>(Resource.Id.ivP_Turrel1);
            ivP_Flares1 = FindViewById<ImageView>(Resource.Id.ivP_Flares1);
            ivP_Parachute1 = FindViewById<ImageView>(Resource.Id.ivP_Parachute1);
            ivP_AirBrake1 = FindViewById<ImageView>(Resource.Id.ivP_AirBrake1);
            ivP_AirRadar1 = FindViewById<ImageView>(Resource.Id.ivP_AirRadar1);
            ivP_Tailhook1 = FindViewById<ImageView>(Resource.Id.ivP_Tailhook1);
            ivP_GroundRadar1 = FindViewById<ImageView>(Resource.Id.ivP_GroundRadar1);
            ivP_CCIP1 = FindViewById<ImageView>(Resource.Id.ivP_CCIP1);
            ivP_CCRP1 = FindViewById<ImageView>(Resource.Id.ivP_CCRP1);
            ivP_GSuit1 = FindViewById<ImageView>(Resource.Id.ivP_GSuit1);

            ivP_ASMissile2 = FindViewById<ImageView>(Resource.Id.ivP_ASMissile2);
            ivP_AAMissile2 = FindViewById<ImageView>(Resource.Id.ivP_AAMissile2);
            ivP_AGMissile2 = FindViewById<ImageView>(Resource.Id.ivP_AGMissile2);
            ivP_HBomb2 = FindViewById<ImageView>(Resource.Id.ivP_HBomb2);
            ivP_HCannon2 = FindViewById<ImageView>(Resource.Id.ivP_HCannon2);
            ivP_HTorpedo2 = FindViewById<ImageView>(Resource.Id.ivP_HTorpedo2);
            ivP_WrongMusic2 = FindViewById<ImageView>(Resource.Id.ivP_WrongMusic2);
            ivP_RWR2 = FindViewById<ImageView>(Resource.Id.ivP_RWR2);
            ivP_Turrel2 = FindViewById<ImageView>(Resource.Id.ivP_Turrel2);
            ivP_Flares2 = FindViewById<ImageView>(Resource.Id.ivP_Flares2);
            ivP_Parachute2 = FindViewById<ImageView>(Resource.Id.ivP_Parachute2);
            ivP_AirBrake2 = FindViewById<ImageView>(Resource.Id.ivP_AirBrake2);
            ivP_AirRadar2 = FindViewById<ImageView>(Resource.Id.ivP_AirRadar2);
            ivP_Tailhook2 = FindViewById<ImageView>(Resource.Id.ivP_Tailhook2);
            ivP_GroundRadar2 = FindViewById<ImageView>(Resource.Id.ivP_GroundRadar2);
            ivP_CCIP2 = FindViewById<ImageView>(Resource.Id.ivP_CCIP2);
            ivP_CCRP2 = FindViewById<ImageView>(Resource.Id.ivP_CCRP2);
            ivP_GSuit2 = FindViewById<ImageView>(Resource.Id.ivP_GSuit2);
        }
    }
}
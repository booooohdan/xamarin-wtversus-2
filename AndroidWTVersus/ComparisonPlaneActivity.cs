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
using AndroidWTVersus.Models;
using AndroidWTVersus.XmlHandler;
using FFImageLoading;
using FFImageLoading.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static Android.Views.View;
using String = System.String;

namespace AndroidWTVersus
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public partial class ComparisonPlaneActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {

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
        List<Plane> planesAll;

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

            spinnerNation = view.FindViewById<Spinner>(Resource.Id.spinnerNation);
            spinnerRank = view.FindViewById<Spinner>(Resource.Id.spinnerRank);

            listView = view.FindViewById<ListView>(Resource.Id.listView);
            searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
            searchView.SetQueryHint("F-4E..");

            planeAdapter = new PlaneAdapter(this, arrayOfPlanes.PlanesListApi);
            listView.Adapter = planeAdapter;
            SpinnerInitialization();
        }

        /// <summary>
        /// Search Spinner Initialization
        /// </summary>
        private void SpinnerInitialization()
        {
            string[] nations = { "All nations", "USA", "Germany", "USSR", "Britain", "Japan", "China", "Italy", "France", "Sweden" };
            string[] ranks = { "All ranks", "I rank", "II rank", "III rank", "IV rank", "V rank", "VI rank" };
            var nationAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, nations);
            var rankAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, ranks);
            spinnerNation.Adapter = nationAdapter;
            spinnerRank.Adapter = rankAdapter;
            spinnerNation.ItemSelected += SpinnerNation_ItemSelected;
            spinnerRank.ItemSelected += SpinnerRank_ItemSelected;
        }

        private void SpinnerNation_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            planesAll = arrayOfPlanes.PlanesListApi;
            spinnerRank.SetSelection(0);
            switch (e.Position)
            {
                case 0:
                    planeAdapter = new PlaneAdapter(this, arrayOfPlanes.PlanesListApi);
                    listView.Adapter = planeAdapter;
                    break;

                case 1:
                    planesAll = planesAll.Where(x => x.Nation == "USA").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 2:
                    planesAll = planesAll.Where(x => x.Nation == "Germany").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 3:
                    planesAll = planesAll.Where(x => x.Nation == "USSR").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 4:
                    planesAll = planesAll.Where(x => x.Nation == "Britain").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 5:
                    planesAll = planesAll.Where(x => x.Nation == "Japan").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 6:
                    planesAll = planesAll.Where(x => x.Nation == "China").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 7:
                    planesAll = planesAll.Where(x => x.Nation == "Italy").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 8:
                    planesAll = planesAll.Where(x => x.Nation == "France").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;

                case 9:
                    planesAll = planesAll.Where(x => x.Nation == "Sweden").ToList();
                    planeAdapter = new PlaneAdapter(this, planesAll);
                    listView.Adapter = planeAdapter;
                    break;
            }
        }

        private void SpinnerRank_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            List<Plane> planesRank;
            switch (e.Position)
            {
                case 0:
                    planesRank = planesAll;
                    planeAdapter = new PlaneAdapter(this, planesRank);
                    listView.Adapter = planeAdapter;
                    break;

                case 1:
                    planesRank = planesAll.Where(x => x.Rank == "I").ToList();
                    planeAdapter = new PlaneAdapter(this, planesRank);
                    listView.Adapter = planeAdapter;
                    break;

                case 2:
                    planesRank = planesAll.Where(x => x.Rank == "II").ToList();
                    planeAdapter = new PlaneAdapter(this, planesRank);
                    listView.Adapter = planeAdapter;
                    break;

                case 3:
                    planesRank = planesAll.Where(x => x.Rank == "III").ToList();
                    planeAdapter = new PlaneAdapter(this, planesRank);
                    listView.Adapter = planeAdapter;
                    break;

                case 4:
                    planesRank = planesAll.Where(x => x.Rank == "IV").ToList();
                    planeAdapter = new PlaneAdapter(this, planesRank);
                    listView.Adapter = planeAdapter;
                    break;

                case 5:
                    planesRank = planesAll.Where(x => x.Rank == "V").ToList();
                    planeAdapter = new PlaneAdapter(this, planesRank);
                    listView.Adapter = planeAdapter;
                    break;

                case 6:
                    planesRank = planesAll.Where(x => x.Rank == "VI").ToList();
                    planeAdapter = new PlaneAdapter(this, planesRank);
                    listView.Adapter = planeAdapter;
                    break;

            }
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
            ivP_AAFoxOneMissile1.Visibility = planeAdapter[e.Position].AAFoxOneMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_AGMissile1.Visibility = planeAdapter[e.Position].AGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_HBomb1.Visibility = planeAdapter[e.Position].HBomb ? ViewStates.Visible : ViewStates.Gone;
            ivP_HCannon1.Visibility = planeAdapter[e.Position].HCannon ? ViewStates.Visible : ViewStates.Gone;
            ivP_HTorpedo1.Visibility = planeAdapter[e.Position].HTorpedo ? ViewStates.Visible : ViewStates.Gone;
            ivP_HMine1.Visibility = planeAdapter[e.Position].HMine ? ViewStates.Visible : ViewStates.Gone;
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
            ivP_AAFoxOneMissile2.Visibility = planeAdapter[e.Position].AAFoxOneMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_AGMissile2.Visibility = planeAdapter[e.Position].AGMissile ? ViewStates.Visible : ViewStates.Gone;
            ivP_HBomb2.Visibility = planeAdapter[e.Position].HBomb ? ViewStates.Visible : ViewStates.Gone;
            ivP_HCannon2.Visibility = planeAdapter[e.Position].HCannon ? ViewStates.Visible : ViewStates.Gone;
            ivP_HTorpedo2.Visibility = planeAdapter[e.Position].HTorpedo ? ViewStates.Visible : ViewStates.Gone;
            ivP_HMine2.Visibility = planeAdapter[e.Position].HMine ? ViewStates.Visible : ViewStates.Gone;
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

            if (planeAdapter[e.Position].EnginePower.HasValue)
            {
                double PTWLeft = Math.Round((double)planeAdapter[e.Position].EnginePower.Value / planeAdapter[e.Position].Weight.Value, 2);
                tvP_PowerToWeight1.SetText(PTWLeft.ToString(), TextNormal);
            }
            else
            {
                tvP_PowerToWeight1.SetText("0", TextNormal);
            }
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

            if (planeAdapter[e.Position].EnginePower.HasValue)
            {
                double PTWLeft = Math.Round((double)planeAdapter[e.Position].EnginePower.Value / planeAdapter[e.Position].Weight.Value, 2);
                tvP_PowerToWeight2.SetText(PTWLeft.ToString(), TextNormal);
            }
            else
            {
                tvP_PowerToWeight2.SetText("0", TextNormal);
            }
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
            comparer.CompareWhenHighIsGood(tvP_PowerToWeight1, tvP_PowerToWeight2);
        }
    }
}
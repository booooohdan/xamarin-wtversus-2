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
    public partial class ComparisonHeliActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {

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
        List<Heli> helisAll;

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

            spinnerNation = view.FindViewById<Spinner>(Resource.Id.spinnerNation);
            spinnerRank = view.FindViewById<Spinner>(Resource.Id.spinnerRank);

            listView = view.FindViewById<ListView>(Resource.Id.listView);
            searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
            searchView.SetQueryHint("AH-64..");

            heliAdapter = new HeliAdapter(this, arrayOfHelis.HelisListApi);
            listView.Adapter = heliAdapter;
            SpinnerInitialization();
        }

        /// <summary>
        /// Search Spinner Initialization
        /// </summary>
        private void SpinnerInitialization()
        {
            string[] nations = { "All nations", "USA", "Germany", "USSR", "Britain", "Japan", "China", "Italy", "France", "Sweden" };
            string[] ranks = { "All ranks","V rank", "VI rank", "VII rank" };
            var nationAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, nations);
            var rankAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, ranks);
            spinnerNation.Adapter = nationAdapter;
            spinnerRank.Adapter = rankAdapter;
            spinnerNation.ItemSelected += SpinnerNation_ItemSelected;
            spinnerRank.ItemSelected += SpinnerRank_ItemSelected;
        }

        private void SpinnerNation_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            helisAll = arrayOfHelis.HelisListApi;
            spinnerRank.SetSelection(0);
            switch (e.Position)
            {
                case 0:
                    heliAdapter = new HeliAdapter(this, arrayOfHelis.HelisListApi);
                    listView.Adapter = heliAdapter;
                    break;

                case 1:
                    helisAll = helisAll.Where(x => x.Nation == "USA").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 2:
                    helisAll = helisAll.Where(x => x.Nation == "Germany").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 3:
                    helisAll = helisAll.Where(x => x.Nation == "USSR").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 4:
                    helisAll = helisAll.Where(x => x.Nation == "Britain").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 5:
                    helisAll = helisAll.Where(x => x.Nation == "Japan").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 6:
                    helisAll = helisAll.Where(x => x.Nation == "China").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 7:
                    helisAll = helisAll.Where(x => x.Nation == "Italy").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 8:
                    helisAll = helisAll.Where(x => x.Nation == "France").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;

                case 9:
                    helisAll = helisAll.Where(x => x.Nation == "Sweden").ToList();
                    heliAdapter = new HeliAdapter(this, helisAll);
                    listView.Adapter = heliAdapter;
                    break;
            }
        }

        private void SpinnerRank_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            List<Heli> helisRank;
            switch (e.Position)
            {
                case 0:
                    helisRank = helisAll;
                    heliAdapter = new HeliAdapter(this, helisRank);
                    listView.Adapter = heliAdapter;
                    break;

                case 1:
                    helisRank = helisAll.Where(x => x.Rank == "V").ToList();
                    heliAdapter = new HeliAdapter(this, helisRank);
                    listView.Adapter = heliAdapter;
                    break;

                case 2:
                    helisRank = helisAll.Where(x => x.Rank == "VI").ToList();
                    heliAdapter = new HeliAdapter(this, helisRank);
                    listView.Adapter = heliAdapter;
                    break;

                case 3:
                    helisRank = helisAll.Where(x => x.Rank == "VII").ToList();
                    heliAdapter = new HeliAdapter(this, helisRank);
                    listView.Adapter = heliAdapter;
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

            if (heliAdapter[e.Position].EnginePower.HasValue)
            {
                double PTWLeft = Math.Round((double)heliAdapter[e.Position].EnginePower.Value / heliAdapter[e.Position].Weight.Value, 2);
                tvH_PowerToWeight1.SetText(PTWLeft.ToString(), TextNormal);
            }
            else
            {
                tvH_PowerToWeight1.SetText("0", TextNormal);
            }
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
            if (heliAdapter[e.Position].EnginePower.HasValue)
            {
                double PTWLeft = Math.Round((double)heliAdapter[e.Position].EnginePower.Value / heliAdapter[e.Position].Weight.Value, 2);
                tvH_PowerToWeight2.SetText(PTWLeft.ToString(), TextNormal);
            }
            else
            {
                tvH_PowerToWeight2.SetText("0", TextNormal);
            }
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
            comparer.CompareWhenHighIsGood(tvH_PowerToWeight1, tvH_PowerToWeight2);
        }
    }
}
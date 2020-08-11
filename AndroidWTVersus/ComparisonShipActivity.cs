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
    public partial class ComparisonShipActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {

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
        List<Ship> shipsAll;

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

            spinnerNation = view.FindViewById<Spinner>(Resource.Id.spinnerNation);
            spinnerRank = view.FindViewById<Spinner>(Resource.Id.spinnerRank);

            listView = view.FindViewById<ListView>(Resource.Id.listView);
            searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
            searchView.SetQueryHint("HMS..");

            shipAdapter = new ShipAdapter(this, arrayOfShips.ShipsListApi);
            listView.Adapter = shipAdapter;
            SpinnerInitialization();
        }

        /// <summary>
        /// Search Spinner Initialization
        /// </summary>
        private void SpinnerInitialization()
        {
            string[] nations = { "All nations", "USA", "Germany", "USSR", "Britain", "Japan", "China", "Italy", "France", "Sweden" };
            string[] ranks = { "All ranks","III rank", "IV rank", "V rank" };
            var nationAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, nations);
            var rankAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, ranks);
            spinnerNation.Adapter = nationAdapter;
            spinnerRank.Adapter = rankAdapter;
            spinnerNation.ItemSelected += SpinnerNation_ItemSelected;
            spinnerRank.ItemSelected += SpinnerRank_ItemSelected;
        }

        private void SpinnerNation_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            shipsAll = arrayOfShips.ShipsListApi;
            spinnerRank.SetSelection(0);
            switch (e.Position)
            {
                case 0:
                    shipAdapter = new ShipAdapter(this, arrayOfShips.ShipsListApi);
                    listView.Adapter = shipAdapter;
                    break;

                case 1:
                    shipsAll = shipsAll.Where(x => x.Nation == "USA").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 2:
                    shipsAll = shipsAll.Where(x => x.Nation == "Germany").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 3:
                    shipsAll = shipsAll.Where(x => x.Nation == "USSR").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 4:
                    shipsAll = shipsAll.Where(x => x.Nation == "Britain").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 5:
                    shipsAll = shipsAll.Where(x => x.Nation == "Japan").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 6:
                    shipsAll = shipsAll.Where(x => x.Nation == "China").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 7:
                    shipsAll = shipsAll.Where(x => x.Nation == "Italy").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 8:
                    shipsAll = shipsAll.Where(x => x.Nation == "France").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;

                case 9:
                    shipsAll = shipsAll.Where(x => x.Nation == "Sweden").ToList();
                    shipAdapter = new ShipAdapter(this, shipsAll);
                    listView.Adapter = shipAdapter;
                    break;
            }
        }

        private void SpinnerRank_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            List<Ship> shipsRank;
            switch (e.Position)
            {
                case 0:
                    shipsRank = shipsAll;
                    shipAdapter = new ShipAdapter(this, shipsRank);
                    listView.Adapter = shipAdapter;
                    break;

                case 1:
                    shipsRank = shipsAll.Where(x => x.Rank == "III").ToList();
                    shipAdapter = new ShipAdapter(this, shipsRank);
                    listView.Adapter = shipAdapter;
                    break;

                case 2:
                    shipsRank = shipsAll.Where(x => x.Rank == "IV").ToList();
                    shipAdapter = new ShipAdapter(this, shipsRank);
                    listView.Adapter = shipAdapter;
                    break;

                case 3:
                    shipsRank = shipsAll.Where(x => x.Rank == "V").ToList();
                    shipAdapter = new ShipAdapter(this, shipsRank);
                    listView.Adapter = shipAdapter;
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
    }
}
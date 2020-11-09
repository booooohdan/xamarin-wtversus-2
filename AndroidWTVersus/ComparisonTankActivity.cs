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
    public partial class ComparisonTankActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener, IOnClickListener
    {

        #region Initialization non View values

        Context context;
        ArrayOfTanks arrayOfTanks;
        SearchView searchView;
        ListView listView;
        TankAdapter tankAdapter;
        Dialog dialog;
        LayoutInflater inflater;
        View view;
        TextView.BufferType TextNormal;
        List<Tank> tanksAll;

        string mm;
        string s;
        string m_s;
        string degree;
        string km_h;
        string h_p;
        string t;
        string br;

        protected InterstitialAd mInterstitialAd;
        int adsCount = 0;
        int adsNum = 4;
        #endregion

        /// <summary>
        /// Base Android OnCreate method. Entry point for app
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization required elements
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ComparisonTankLayout);
            context = Application.Context;
            #endregion

            #region Ads initializer
            MobileAds.Initialize(context);
            var adView = FindViewById<AdView>(Resource.Id.adViewComparerTank);
            adView.LoadAd(new AdRequest.Builder().Build());
            //var requestbuilder = new AdRequest.Builder().AddTestDevice("46CCAB8BBCE5B5FFA79C22BEB98029AC");
            //adView.LoadAd(requestbuilder.Build());

            mInterstitialAd = new InterstitialAd(this);
            mInterstitialAd.AdUnitId = GetString(Resource.String.adsIntersitialTank);
            mInterstitialAd.LoadAd(new AdRequest.Builder().Build());
            #endregion

            BindingInterfaceElementsToCode();
            FillListFromCacheAsync().ConfigureAwait(false);
            TopMenuInitializer();
            BottomMenuInitializer();
            LetsCompare();

            searchableButton1.Click += SearchableButton1_Click;
            searchableButton2.Click += SearchableButton2_Click;
        }


        /// <summary>
        /// Load cached List of tanks
        /// </summary>
        private async Task FillListFromCacheAsync()
        {
            arrayOfTanks = await BlobCache.UserAccount.GetObject<ArrayOfTanks>("cachedArrayOfTanks");
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
            searchView.SetQueryHint("M1A2..");

            tankAdapter = new TankAdapter(this, arrayOfTanks.TanksListApi);
            listView.Adapter = tankAdapter;
            SpinnerInitialization();
        }

        /// <summary>
        /// Search Spinner Initialization
        /// </summary>
        private void SpinnerInitialization()
        {
            string[] nations = { "All nations", "USA", "Germany", "USSR", "Britain", "Japan", "China", "Italy", "France", "Sweden" };
            string[] ranks = { "All ranks", "I rank", "II rank", "III rank", "IV rank", "V rank", "VI rank", "VII rank" };
            var nationAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, nations);
            var rankAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, ranks);
            spinnerNation.Adapter = nationAdapter;
            spinnerRank.Adapter = rankAdapter;
            spinnerNation.ItemSelected += SpinnerNation_ItemSelected;
            spinnerRank.ItemSelected += SpinnerRank_ItemSelected;
        }

        private void SpinnerNation_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            tanksAll = arrayOfTanks.TanksListApi;
            spinnerRank.SetSelection(0);
            switch (e.Position)
            {
                case 0:
                    tankAdapter = new TankAdapter(this, arrayOfTanks.TanksListApi);
                    listView.Adapter = tankAdapter;
                    break;

                case 1:
                    tanksAll = tanksAll.Where(x => x.Nation == "USA").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 2:
                    tanksAll = tanksAll.Where(x => x.Nation == "Germany").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 3:
                    tanksAll = tanksAll.Where(x => x.Nation == "USSR").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 4:
                    tanksAll = tanksAll.Where(x => x.Nation == "Britain").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 5:
                    tanksAll = tanksAll.Where(x => x.Nation == "Japan").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 6:
                    tanksAll = tanksAll.Where(x => x.Nation == "China").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 7:
                    tanksAll = tanksAll.Where(x => x.Nation == "Italy").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 8:
                    tanksAll = tanksAll.Where(x => x.Nation == "France").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;

                case 9:
                    tanksAll = tanksAll.Where(x => x.Nation == "Sweden").ToList();
                    tankAdapter = new TankAdapter(this, tanksAll);
                    listView.Adapter = tankAdapter;
                    break;
            }
        }

        private void SpinnerRank_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            List<Tank> tanksRank;
            switch (e.Position)
            {
                case 0:
                    tanksRank = tanksAll;
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
                    break;

                case 1:
                    tanksRank = tanksAll.Where(x => x.Rank == "I").ToList();
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
                    break;

                case 2:
                    tanksRank = tanksAll.Where(x => x.Rank == "II").ToList();
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
                    break;

                case 3:
                    tanksRank = tanksAll.Where(x => x.Rank == "III").ToList();
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
                    break;

                case 4:
                    tanksRank = tanksAll.Where(x => x.Rank == "IV").ToList();
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
                    break;

                case 5:
                    tanksRank = tanksAll.Where(x => x.Rank == "V").ToList();
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
                    break;

                case 6:
                    tanksRank = tanksAll.Where(x => x.Rank == "VI").ToList();
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
                    break;

                case 7:
                    tanksRank = tanksAll.Where(x => x.Rank == "VII").ToList();
                    tankAdapter = new TankAdapter(this, tanksRank);
                    listView.Adapter = tankAdapter;
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
            tankAdapter.Filter.InvokeFilter(e.NewText);
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
        /// Set data from List tanks to left part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToLeft(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(tankAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivT_Tank1);

            ivT_ShellAP1.Visibility = tankAdapter[e.Position].ShellAP ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPHE1.Visibility = tankAdapter[e.Position].ShellAPHE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHE1.Visibility = tankAdapter[e.Position].ShellHE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPCR1.Visibility = tankAdapter[e.Position].ShellAPCR ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPDS1.Visibility = tankAdapter[e.Position].ShellAPDS ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPFSDS1.Visibility = tankAdapter[e.Position].ShellAPFSDS ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEAT1.Visibility = tankAdapter[e.Position].ShellHEAT ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEATFS1.Visibility = tankAdapter[e.Position].ShellHEATFS ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellShrapnel1.Visibility = tankAdapter[e.Position].ShellShrapnel ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHESH1.Visibility = tankAdapter[e.Position].ShellHESH ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGM1.Visibility = tankAdapter[e.Position].ShellATGM ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGMHE1.Visibility = tankAdapter[e.Position].ShellATGMHE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGMHEVT1.Visibility = tankAdapter[e.Position].ShellATGMHEVT ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGMTandem1.Visibility = tankAdapter[e.Position].ShellATGMTandem ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellVOG1.Visibility = tankAdapter[e.Position].ShellVOG ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellSSM1.Visibility = tankAdapter[e.Position].ShellSSM ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEATGRENADE1.Visibility = tankAdapter[e.Position].ShellHEATGRENADE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEVT1.Visibility = tankAdapter[e.Position].ShellHEVT ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellSAM1.Visibility = tankAdapter[e.Position].ShellSAM ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellSmoke1.Visibility = tankAdapter[e.Position].ShellSmoke ? ViewStates.Visible : ViewStates.Gone;

            ivT_NVDCommander1.Visibility = tankAdapter[e.Position].NVDCommander ? ViewStates.Visible : ViewStates.Gone;
            ivT_ThermalCommander1.Visibility = tankAdapter[e.Position].ThermalCommander ? ViewStates.Visible : ViewStates.Gone;
            ivT_NVDGunner1.Visibility = tankAdapter[e.Position].NVDGunner ? ViewStates.Visible : ViewStates.Gone;
            ivT_ThermalGunner1.Visibility = tankAdapter[e.Position].ThermalGunner ? ViewStates.Visible : ViewStates.Gone;
            ivT_IRSpotlight1.Visibility = tankAdapter[e.Position].IRSpotlight ? ViewStates.Visible : ViewStates.Gone;
            ivT_ExhaustSmoke1.Visibility = tankAdapter[e.Position].ExhaustSmoke ? ViewStates.Visible : ViewStates.Gone;
            ivT_GrenadeSmoke1.Visibility = tankAdapter[e.Position].GrenadeSmoke ? ViewStates.Visible : ViewStates.Gone;
            ivT_AddOnArmor1.Visibility = tankAdapter[e.Position].AddOnArmor ? ViewStates.Visible : ViewStates.Gone;
            ivT_ReactiveArmor1.Visibility = tankAdapter[e.Position].ReactiveArmor ? ViewStates.Visible : ViewStates.Gone;
            ivT_AutoLoader1.Visibility = tankAdapter[e.Position].AutoLoader ? ViewStates.Visible : ViewStates.Gone;
            ivT_AAMachineGun1.Visibility = tankAdapter[e.Position].AAMachineGun ? ViewStates.Visible : ViewStates.Gone;
            ivT_Stabilizer1.Visibility = tankAdapter[e.Position].Stabilizer ? ViewStates.Visible : ViewStates.Gone;
            ivT_HullBreak1.Visibility = tankAdapter[e.Position].HullBreak ? ViewStates.Visible : ViewStates.Gone;
            ivT_Hydropneumatic1.Visibility = tankAdapter[e.Position].Hydropneumatic ? ViewStates.Visible : ViewStates.Gone;
            ivT_Amphibious1.Visibility = tankAdapter[e.Position].Amphibious ? ViewStates.Visible : ViewStates.Gone;
            ivT_AirSearchRadar1.Visibility = tankAdapter[e.Position].AirSearchRadar ? ViewStates.Visible : ViewStates.Gone;
            ivT_RWR1.Visibility = tankAdapter[e.Position].RWR ? ViewStates.Visible : ViewStates.Gone;
            ivT_AirLockRadar1.Visibility = tankAdapter[e.Position].AirLockRadar ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List tanks to right part images
        /// </summary>
        /// <param name="e"></param>
        private void SetImageToRight(AdapterView.ItemClickEventArgs e)
        {
            ImageService.Instance.LoadUrl(tankAdapter[e.Position].Image)
            .LoadingPlaceholder("Loading", ImageSource.CompiledResource)
            .ErrorPlaceholder("Error", ImageSource.CompiledResource)
            .Retry(3, 500).Into(ivT_Tank2);

            ivT_ShellAP2.Visibility = tankAdapter[e.Position].ShellAP ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPHE2.Visibility = tankAdapter[e.Position].ShellAPHE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHE2.Visibility = tankAdapter[e.Position].ShellHE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPCR2.Visibility = tankAdapter[e.Position].ShellAPCR ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPDS2.Visibility = tankAdapter[e.Position].ShellAPDS ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellAPFSDS2.Visibility = tankAdapter[e.Position].ShellAPFSDS ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEAT2.Visibility = tankAdapter[e.Position].ShellHEAT ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEATFS2.Visibility = tankAdapter[e.Position].ShellHEATFS ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellShrapnel2.Visibility = tankAdapter[e.Position].ShellShrapnel ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHESH2.Visibility = tankAdapter[e.Position].ShellHESH ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGM2.Visibility = tankAdapter[e.Position].ShellATGM ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGMHE2.Visibility = tankAdapter[e.Position].ShellATGMHE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGMHEVT2.Visibility = tankAdapter[e.Position].ShellATGMHEVT ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellATGMTandem2.Visibility = tankAdapter[e.Position].ShellATGMTandem ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellVOG2.Visibility = tankAdapter[e.Position].ShellVOG ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellSSM2.Visibility = tankAdapter[e.Position].ShellSSM ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEATGRENADE2.Visibility = tankAdapter[e.Position].ShellHEATGRENADE ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellHEVT2.Visibility = tankAdapter[e.Position].ShellHEVT ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellSAM2.Visibility = tankAdapter[e.Position].ShellSAM ? ViewStates.Visible : ViewStates.Gone;
            ivT_ShellSmoke2.Visibility = tankAdapter[e.Position].ShellSmoke ? ViewStates.Visible : ViewStates.Gone;

            ivT_NVDCommander2.Visibility = tankAdapter[e.Position].NVDCommander ? ViewStates.Visible : ViewStates.Gone;
            ivT_ThermalCommander2.Visibility = tankAdapter[e.Position].ThermalCommander ? ViewStates.Visible : ViewStates.Gone;
            ivT_NVDGunner2.Visibility = tankAdapter[e.Position].NVDGunner ? ViewStates.Visible : ViewStates.Gone;
            ivT_ThermalGunner2.Visibility = tankAdapter[e.Position].ThermalGunner ? ViewStates.Visible : ViewStates.Gone;
            ivT_IRSpotlight2.Visibility = tankAdapter[e.Position].IRSpotlight ? ViewStates.Visible : ViewStates.Gone;
            ivT_ExhaustSmoke2.Visibility = tankAdapter[e.Position].ExhaustSmoke ? ViewStates.Visible : ViewStates.Gone;
            ivT_GrenadeSmoke2.Visibility = tankAdapter[e.Position].GrenadeSmoke ? ViewStates.Visible : ViewStates.Gone;
            ivT_AddOnArmor2.Visibility = tankAdapter[e.Position].AddOnArmor ? ViewStates.Visible : ViewStates.Gone;
            ivT_ReactiveArmor2.Visibility = tankAdapter[e.Position].ReactiveArmor ? ViewStates.Visible : ViewStates.Gone;
            ivT_AutoLoader2.Visibility = tankAdapter[e.Position].AutoLoader ? ViewStates.Visible : ViewStates.Gone;
            ivT_AAMachineGun2.Visibility = tankAdapter[e.Position].AAMachineGun ? ViewStates.Visible : ViewStates.Gone;
            ivT_Stabilizer2.Visibility = tankAdapter[e.Position].Stabilizer ? ViewStates.Visible : ViewStates.Gone;
            ivT_HullBreak2.Visibility = tankAdapter[e.Position].HullBreak ? ViewStates.Visible : ViewStates.Gone;
            ivT_Hydropneumatic2.Visibility = tankAdapter[e.Position].Hydropneumatic ? ViewStates.Visible : ViewStates.Gone;
            ivT_Amphibious2.Visibility = tankAdapter[e.Position].Amphibious ? ViewStates.Visible : ViewStates.Gone;
            ivT_AirSearchRadar2.Visibility = tankAdapter[e.Position].AirSearchRadar ? ViewStates.Visible : ViewStates.Gone;
            ivT_RWR2.Visibility = tankAdapter[e.Position].RWR ? ViewStates.Visible : ViewStates.Gone;
            ivT_AirLockRadar2.Visibility = tankAdapter[e.Position].AirLockRadar ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List tanks to left part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToLeft(AdapterView.ItemClickEventArgs e)
        {
            searchableButton1.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchableButton1.SetText(tankAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", tankAdapter[e.Position].BR);
            tvT_BattleRating1.SetText(br, TextNormal);
            tvT_FirstYear1.SetText(tankAdapter[e.Position].FirstRideYear + "", TextNormal);
            tvT_RepairCost1.SetText(tankAdapter[e.Position].RepairCost + "", TextNormal);

            tvT_Cannon1.SetText(tankAdapter[e.Position].Cannon.Replace("<br>", "\n") + "", TextNormal);
            tvT_Penetration1.SetText(tankAdapter[e.Position].Penetration + mm, TextNormal);
            tvT_MuzzleVelocity1.SetText(tankAdapter[e.Position].ShellSpeed + m_s, TextNormal);
            tvT_ReloadTime1.SetText(tankAdapter[e.Position].ReloadTime + s, TextNormal);
            tvT_GunDepression1.SetText(tankAdapter[e.Position].DownAimAngle + degree, TextNormal);

            tvT_ReducedArmorFrontTurret1.SetText(tankAdapter[e.Position].ReducedArmorFrontTurret + mm, TextNormal);
            tvT_ReducedArmorTopSheet1.SetText(tankAdapter[e.Position].ReducedArmorTopSheet + mm, TextNormal);
            tvT_ReducedArmorBottomSheet1.SetText(tankAdapter[e.Position].ReducedArmorBottomSheet + mm, TextNormal);

            tvT_MaxSpeedAtRoad1.SetText(tankAdapter[e.Position].MaxSpeedAtRoad + km_h, TextNormal);
            tvT_MaxSpeedAtTerrain1.SetText(tankAdapter[e.Position].MaxSpeedAtTerrain + km_h, TextNormal);
            tvT_MaxReverseSpeed1.SetText(tankAdapter[e.Position].MaxReverseSpeed + km_h, TextNormal);
            tvT_AccelerationTo1001.SetText(tankAdapter[e.Position].AccelerationTo100 + s, TextNormal);
            tvT_TurnTurretTime1.SetText(tankAdapter[e.Position].TurnTurretTime + s, TextNormal);
            tvT_TurnHullTime1.SetText(tankAdapter[e.Position].TurnHullTime + s, TextNormal);
            tvT_EnginePower1.SetText(tankAdapter[e.Position].EnginePower + h_p, TextNormal);
            tvT_Weight1.SetText(tankAdapter[e.Position].Weight + t, TextNormal);
            if (tankAdapter[e.Position].EnginePower.HasValue)
            {
                double PTWLeft = Math.Round((double)tankAdapter[e.Position].EnginePower.Value / tankAdapter[e.Position].Weight.Value, 2);
                tvT_PowerToWeight1.SetText(PTWLeft.ToString(), TextNormal);
            }
            else
            {
                tvT_PowerToWeight1.SetText("0", TextNormal);
            }
        }

        /// <summary>
        /// Set data from List tanks to right part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToRight(AdapterView.ItemClickEventArgs e)
        {
            searchableButton2.Typeface = Typeface.CreateFromAsset(Assets, "symbols.ttf");
            searchableButton2.SetText(tankAdapter[e.Position].Name + "", TextNormal);

            br = String.Format("{0:F1}", tankAdapter[e.Position].BR);
            tvT_BattleRating2.SetText(br, TextNormal);
            tvT_FirstYear2.SetText(tankAdapter[e.Position].FirstRideYear + "", TextNormal);
            tvT_RepairCost2.SetText(tankAdapter[e.Position].RepairCost + "", TextNormal);

            tvT_Cannon2.SetText(tankAdapter[e.Position].Cannon.Replace("<br>", "\n") + "", TextNormal);
            tvT_Penetration2.SetText(tankAdapter[e.Position].Penetration + mm, TextNormal);
            tvT_MuzzleVelocity2.SetText(tankAdapter[e.Position].ShellSpeed + m_s, TextNormal);
            tvT_ReloadTime2.SetText(tankAdapter[e.Position].ReloadTime + s, TextNormal);
            tvT_GunDepression2.SetText(tankAdapter[e.Position].DownAimAngle + degree, TextNormal);

            tvT_ReducedArmorFrontTurret2.SetText(tankAdapter[e.Position].ReducedArmorFrontTurret + mm, TextNormal);
            tvT_ReducedArmorTopSheet2.SetText(tankAdapter[e.Position].ReducedArmorTopSheet + mm, TextNormal);
            tvT_ReducedArmorBottomSheet2.SetText(tankAdapter[e.Position].ReducedArmorBottomSheet + mm, TextNormal);

            tvT_MaxSpeedAtRoad2.SetText(tankAdapter[e.Position].MaxSpeedAtRoad + km_h, TextNormal);
            tvT_MaxSpeedAtTerrain2.SetText(tankAdapter[e.Position].MaxSpeedAtTerrain + km_h, TextNormal);
            tvT_MaxReverseSpeed2.SetText(tankAdapter[e.Position].MaxReverseSpeed + km_h, TextNormal);
            tvT_AccelerationTo1002.SetText(tankAdapter[e.Position].AccelerationTo100 + s, TextNormal);
            tvT_TurnTurretTime2.SetText(tankAdapter[e.Position].TurnTurretTime + s, TextNormal);
            tvT_TurnHullTime2.SetText(tankAdapter[e.Position].TurnHullTime + s, TextNormal);
            tvT_EnginePower2.SetText(tankAdapter[e.Position].EnginePower + h_p, TextNormal);
            tvT_Weight2.SetText(tankAdapter[e.Position].Weight + t, TextNormal);
            if (tankAdapter[e.Position].EnginePower.HasValue)
            {
                double PTWLeft = Math.Round((double)tankAdapter[e.Position].EnginePower.Value / tankAdapter[e.Position].Weight.Value, 2);
                tvT_PowerToWeight2.SetText(PTWLeft.ToString(), TextNormal);
            }
            else
            {
                tvT_PowerToWeight2.SetText("0", TextNormal);
            }
        }

        /// <summary>
        /// Describes all strings to compare
        /// </summary>
        private void LetsCompare()
        {
            var comparer = new CompareHelper();


            adsCount++;
            if ((adsCount % adsNum) == 0)
            {
                if (mInterstitialAd.IsLoaded)
                {
                    mInterstitialAd.Show();
                }
                else
                {
                    var adRequest = new AdRequest.Builder().Build();
                    mInterstitialAd.LoadAd(adRequest);
                }
            }

            comparer.CompareWhenLowIsGood(tvT_RepairCost1, tvT_RepairCost2);
            comparer.CompareWhenHighIsGood(tvT_FirstYear1, tvT_FirstYear2);
            comparer.CompareWhenLowIsGood(tvT_BattleRating1, tvT_BattleRating2);
            comparer.CompareWhenHighIsGood(tvT_Penetration1, tvT_Penetration2);
            comparer.CompareWhenHighIsGood(tvT_MuzzleVelocity1, tvT_MuzzleVelocity2);
            comparer.CompareWhenLowIsGood(tvT_ReloadTime1, tvT_ReloadTime2);
            comparer.CompareWhenHighIsGood(tvT_GunDepression1, tvT_GunDepression2);
            comparer.CompareWhenHighIsGood(tvT_ReducedArmorFrontTurret1, tvT_ReducedArmorFrontTurret2);
            comparer.CompareWhenHighIsGood(tvT_ReducedArmorTopSheet1, tvT_ReducedArmorTopSheet2);
            comparer.CompareWhenHighIsGood(tvT_ReducedArmorBottomSheet1, tvT_ReducedArmorBottomSheet2);
            comparer.CompareWhenHighIsGood(tvT_MaxSpeedAtRoad1, tvT_MaxSpeedAtRoad2);
            comparer.CompareWhenHighIsGood(tvT_MaxSpeedAtTerrain1, tvT_MaxSpeedAtTerrain2);
            comparer.CompareWhenHighIsGood(tvT_MaxReverseSpeed1, tvT_MaxReverseSpeed2);
            comparer.CompareWhenLowIsGood(tvT_AccelerationTo1001, tvT_AccelerationTo1002);
            comparer.CompareWhenLowIsGood(tvT_TurnTurretTime1, tvT_TurnTurretTime2);
            comparer.CompareWhenLowIsGood(tvT_TurnHullTime1, tvT_TurnHullTime2);
            comparer.CompareWhenHighIsGood(tvT_EnginePower1, tvT_EnginePower2);
            comparer.CompareWhenLowIsGood(tvT_Weight1, tvT_Weight2);
            comparer.CompareWhenHighIsGood(tvT_PowerToWeight1, tvT_PowerToWeight2);
        }
    }
}
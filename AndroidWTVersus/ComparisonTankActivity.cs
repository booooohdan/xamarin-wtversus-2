using Akavache;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidWTVersus.Adapters;
using AndroidWTVersus.Comparison;
using AndroidWTVersus.XmlHandler;
using FFImageLoading;
using FFImageLoading.Work;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using String = System.String;

namespace AndroidWTVersus
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class ComparisonTankActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        #region initialization View values

        ImageView ivT_Tank1;
        ImageView ivT_Tank2;

        Button searchableButton1;
        Button searchableButton2;

        TextView tvT_BattleRating1;
        TextView tvT_BattleRating2;
        TextView tvT_FirstYear1;
        TextView tvT_FirstYear2;
        TextView tvT_RepairCost1;
        TextView tvT_RepairCost2;
        TextView tvT_Cannon1;
        TextView tvT_Cannon2;
        TextView tvT_Penetration1;
        TextView tvT_Penetration2;

        ImageView ivT_ShellAP1;
        ImageView ivT_ShellAPHE1;
        ImageView ivT_ShellHE1;
        ImageView ivT_ShellAPCR1;
        ImageView ivT_ShellAPDS1;
        ImageView ivT_ShellAPFSDS1;
        ImageView ivT_ShellHEAT1;
        ImageView ivT_ShellHEATFS1;
        ImageView ivT_ShellShrapnel1;
        ImageView ivT_ShellHESH1;
        ImageView ivT_ShellATGM1;
        ImageView ivT_ShellATGMHE1;
        ImageView ivT_ShellATGMHEVT1;
        ImageView ivT_ShellATGMTandem1;
        ImageView ivT_ShellVOG1;
        ImageView ivT_ShellSSM1;
        ImageView ivT_ShellHEATGRENADE1;
        ImageView ivT_ShellHEVT1;
        ImageView ivT_ShellSAM1;
        ImageView ivT_ShellSmoke1;

        ImageView ivT_ShellAP2;
        ImageView ivT_ShellAPHE2;
        ImageView ivT_ShellHE2;
        ImageView ivT_ShellAPCR2;
        ImageView ivT_ShellAPDS2;
        ImageView ivT_ShellAPFSDS2;
        ImageView ivT_ShellHEAT2;
        ImageView ivT_ShellHEATFS2;
        ImageView ivT_ShellShrapnel2;
        ImageView ivT_ShellHESH2;
        ImageView ivT_ShellATGM2;
        ImageView ivT_ShellATGMHE2;
        ImageView ivT_ShellATGMHEVT2;
        ImageView ivT_ShellATGMTandem2;
        ImageView ivT_ShellVOG2;
        ImageView ivT_ShellSSM2;
        ImageView ivT_ShellHEATGRENADE2;
        ImageView ivT_ShellHEVT2;
        ImageView ivT_ShellSAM2;
        ImageView ivT_ShellSmoke2;

        TextView tvT_MuzzleVelocity1;
        TextView tvT_MuzzleVelocity2;
        TextView tvT_ReloadTime1;
        TextView tvT_ReloadTime2;
        TextView tvT_GunDepression1;
        TextView tvT_GunDepression2;

        ImageView ivT_NVDCommander1;
        ImageView ivT_ThermalCommander1;
        ImageView ivT_NVDGunner1;
        ImageView ivT_ThermalGunner1;
        ImageView ivT_IRSpotlight1;
        ImageView ivT_ExhaustSmoke1;
        ImageView ivT_GrenadeSmoke1;
        ImageView ivT_AddOnArmor1;
        ImageView ivT_ReactiveArmor1;
        ImageView ivT_AutoLoader1;
        ImageView ivT_AAMachineGun1;
        ImageView ivT_Stabilizer1;
        ImageView ivT_HullBreak1;
        ImageView ivT_Hydropneumatic1;
        ImageView ivT_Amphibious1;
        ImageView ivT_AirSearchRadar1;
        ImageView ivT_AirLockRadar1;
        ImageView ivT_TankSearchRadar1;

        ImageView ivT_NVDCommander2;
        ImageView ivT_ThermalCommander2;
        ImageView ivT_NVDGunner2;
        ImageView ivT_ThermalGunner2;
        ImageView ivT_IRSpotlight2;
        ImageView ivT_ExhaustSmoke2;
        ImageView ivT_GrenadeSmoke2;
        ImageView ivT_AddOnArmor2;
        ImageView ivT_ReactiveArmor2;
        ImageView ivT_AutoLoader2;
        ImageView ivT_AAMachineGun2;
        ImageView ivT_Stabilizer2;
        ImageView ivT_HullBreak2;
        ImageView ivT_Hydropneumatic2;
        ImageView ivT_Amphibious2;
        ImageView ivT_AirSearchRadar2;
        ImageView ivT_AirLockRadar2;
        ImageView ivT_TankSearchRadar2;


        TextView tvT_ReducedArmorFrontTurret1;
        TextView tvT_ReducedArmorFrontTurret2;
        TextView tvT_ReducedArmorTopSheet1;
        TextView tvT_ReducedArmorTopSheet2;
        TextView tvT_ReducedArmorBottomSheet1;
        TextView tvT_ReducedArmorBottomSheet2;

        TextView tvT_MaxSpeedAtRoad1;
        TextView tvT_MaxSpeedAtRoad2;
        TextView tvT_MaxSpeedAtTerrain1;
        TextView tvT_MaxSpeedAtTerrain2;
        TextView tvT_MaxReverseSpeed1;
        TextView tvT_MaxReverseSpeed2;
        TextView tvT_AccelerationTo1001;
        TextView tvT_AccelerationTo1002;
        TextView tvT_TurnTurretTime1;
        TextView tvT_TurnTurretTime2;
        TextView tvT_TurnHullTime1;
        TextView tvT_TurnHullTime2;
        TextView tvT_EnginePower1;
        TextView tvT_EnginePower2;
        TextView tvT_Weight1;
        TextView tvT_Weight2;

        #endregion

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

        string mm;
        string s;
        string m_s;
        string degree;
        string km_h;
        string h_p;
        string t;
        string br;
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
            //Bottom menu initialization
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            #endregion

            BindingInterfaceElementsToCode();
            FillListFromCacheAsync().ConfigureAwait(false);
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

            listView = view.FindViewById<ListView>(Resource.Id.listView);
            searchView = view.FindViewById<SearchView>(Resource.Id.searchView);
            searchView.SetQueryHint("M1A2..");

            tankAdapter = new TankAdapter(this, arrayOfTanks.TanksListApi);
            listView.Adapter = tankAdapter;
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
            ivT_AirLockRadar2.Visibility = tankAdapter[e.Position].AirLockRadar ? ViewStates.Visible : ViewStates.Gone;
        }

        /// <summary>
        /// Set data from List tanks to left part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToLeft(AdapterView.ItemClickEventArgs e)
        {
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
        }

        /// <summary>
        /// Set data from List tanks to right part texts
        /// </summary>
        /// <param name="e"></param>
        private void SetTextToRight(AdapterView.ItemClickEventArgs e)
        {
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
        }

        /// <summary>
        /// Describes all strings to compare
        /// </summary>
        private void LetsCompare()
        {
            var comparer = new Comparer();

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
        }

        /// <summary>
        /// Closing cache connection in OnStop method
        /// </summary>
        protected override void OnStop()
        {
            base.OnStop();
            BlobCache.Shutdown().Wait();
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
                    return true;
                case Resource.Id.navigation_feedback:
                    return true;
            }
            return false;
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

            ivT_Tank1 = FindViewById<ImageView>(Resource.Id.ivT_Tank1);
            ivT_Tank2 = FindViewById<ImageView>(Resource.Id.ivT_Tank2);
            searchableButton1 = FindViewById<Button>(Resource.Id.searchableButton1);
            searchableButton2 = FindViewById<Button>(Resource.Id.searchableButton2);

            tvT_BattleRating1 = FindViewById<TextView>(Resource.Id.tvT_BattleRating1);
            tvT_BattleRating2 = FindViewById<TextView>(Resource.Id.tvT_BattleRating2);
            tvT_FirstYear1 = FindViewById<TextView>(Resource.Id.tvT_FirstYear1);
            tvT_FirstYear2 = FindViewById<TextView>(Resource.Id.tvT_FirstYear2);
            tvT_RepairCost1 = FindViewById<TextView>(Resource.Id.tvT_RepairCost1);
            tvT_RepairCost2 = FindViewById<TextView>(Resource.Id.tvT_RepairCost2);
            tvT_Cannon1 = FindViewById<TextView>(Resource.Id.tvT_Cannon1);
            tvT_Cannon2 = FindViewById<TextView>(Resource.Id.tvT_Cannon2);
            tvT_Penetration1 = FindViewById<TextView>(Resource.Id.tvT_Penetration1);
            tvT_Penetration2 = FindViewById<TextView>(Resource.Id.tvT_Penetration2);

            ivT_ShellAP1 = FindViewById<ImageView>(Resource.Id.ivT_ShellAP1);
            ivT_ShellAPHE1 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPHE1);
            ivT_ShellHE1 = FindViewById<ImageView>(Resource.Id.ivT_ShellHE1);
            ivT_ShellAPCR1 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPCR1);
            ivT_ShellAPDS1 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPDS1);
            ivT_ShellAPFSDS1 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPFSDS1);
            ivT_ShellHEAT1 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEAT1);
            ivT_ShellHEATFS1 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEATFS1);
            ivT_ShellShrapnel1 = FindViewById<ImageView>(Resource.Id.ivT_ShellShrapnel1);
            ivT_ShellHESH1 = FindViewById<ImageView>(Resource.Id.ivT_ShellHESH1);
            ivT_ShellATGM1 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGM1);
            ivT_ShellATGMHE1 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGMHE1);
            ivT_ShellATGMHEVT1 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGMHEVT1);
            ivT_ShellATGMTandem1 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGMTandem1);
            ivT_ShellVOG1 = FindViewById<ImageView>(Resource.Id.ivT_ShellVOG1);
            ivT_ShellSSM1 = FindViewById<ImageView>(Resource.Id.ivT_ShellSSM1);
            ivT_ShellHEATGRENADE1 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEATGRENADE1);
            ivT_ShellHEVT1 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEVT1);
            ivT_ShellSAM1 = FindViewById<ImageView>(Resource.Id.ivT_ShellSAM1);
            ivT_ShellSmoke1 = FindViewById<ImageView>(Resource.Id.ivT_ShellSmoke1);

            ivT_ShellAP2 = FindViewById<ImageView>(Resource.Id.ivT_ShellAP2);
            ivT_ShellAPHE2 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPHE2);
            ivT_ShellHE2 = FindViewById<ImageView>(Resource.Id.ivT_ShellHE2);
            ivT_ShellAPCR2 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPCR2);
            ivT_ShellAPDS2 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPDS2);
            ivT_ShellAPFSDS2 = FindViewById<ImageView>(Resource.Id.ivT_ShellAPFSDS2);
            ivT_ShellHEAT2 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEAT2);
            ivT_ShellHEATFS2 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEATFS2);
            ivT_ShellShrapnel2 = FindViewById<ImageView>(Resource.Id.ivT_ShellShrapnel2);
            ivT_ShellHESH2 = FindViewById<ImageView>(Resource.Id.ivT_ShellHESH2);
            ivT_ShellATGM2 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGM2);
            ivT_ShellATGMHE2 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGMHE2);
            ivT_ShellATGMHEVT2 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGMHEVT2);
            ivT_ShellATGMTandem2 = FindViewById<ImageView>(Resource.Id.ivT_ShellATGMTandem2);
            ivT_ShellVOG2 = FindViewById<ImageView>(Resource.Id.ivT_ShellVOG2);
            ivT_ShellSSM2 = FindViewById<ImageView>(Resource.Id.ivT_ShellSSM2);
            ivT_ShellHEATGRENADE2 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEATGRENADE2);
            ivT_ShellHEVT2 = FindViewById<ImageView>(Resource.Id.ivT_ShellHEVT2);
            ivT_ShellSAM2 = FindViewById<ImageView>(Resource.Id.ivT_ShellSAM2);
            ivT_ShellSmoke2 = FindViewById<ImageView>(Resource.Id.ivT_ShellSmoke2);

            tvT_MuzzleVelocity1 = FindViewById<TextView>(Resource.Id.tvT_MuzzleVelocity1);
            tvT_MuzzleVelocity2 = FindViewById<TextView>(Resource.Id.tvT_MuzzleVelocity2);
            tvT_ReloadTime1 = FindViewById<TextView>(Resource.Id.tvT_ReloadTime1);
            tvT_ReloadTime2 = FindViewById<TextView>(Resource.Id.tvT_ReloadTime2);
            tvT_GunDepression1 = FindViewById<TextView>(Resource.Id.tvT_GunDepression1);
            tvT_GunDepression2 = FindViewById<TextView>(Resource.Id.tvT_GunDepression2);

            ivT_NVDCommander1 = FindViewById<ImageView>(Resource.Id.ivT_NVDCommander1);
            ivT_ThermalCommander1 = FindViewById<ImageView>(Resource.Id.ivT_ThermalCommander1);
            ivT_NVDGunner1 = FindViewById<ImageView>(Resource.Id.ivT_NVDGunner1);
            ivT_ThermalGunner1 = FindViewById<ImageView>(Resource.Id.ivT_ThermalGunner1);
            ivT_IRSpotlight1 = FindViewById<ImageView>(Resource.Id.ivT_IRSpotlight1);
            ivT_ExhaustSmoke1 = FindViewById<ImageView>(Resource.Id.ivT_ExhaustSmoke1);
            ivT_GrenadeSmoke1 = FindViewById<ImageView>(Resource.Id.ivT_GrenadeSmoke1);
            ivT_AddOnArmor1 = FindViewById<ImageView>(Resource.Id.ivT_AddOnArmor1);
            ivT_ReactiveArmor1 = FindViewById<ImageView>(Resource.Id.ivT_ReactiveArmor1);
            ivT_AutoLoader1 = FindViewById<ImageView>(Resource.Id.ivT_AutoLoader1);
            ivT_AAMachineGun1 = FindViewById<ImageView>(Resource.Id.ivT_AAMachineGun1);
            ivT_Stabilizer1 = FindViewById<ImageView>(Resource.Id.ivT_Stabilizer1);
            ivT_HullBreak1 = FindViewById<ImageView>(Resource.Id.ivT_HullBreak1);
            ivT_Hydropneumatic1 = FindViewById<ImageView>(Resource.Id.ivT_Hydropneumatic1);
            ivT_Amphibious1 = FindViewById<ImageView>(Resource.Id.ivT_Amphibious1);
            ivT_AirSearchRadar1 = FindViewById<ImageView>(Resource.Id.ivT_AirSearchRadar1);
            ivT_AirLockRadar1 = FindViewById<ImageView>(Resource.Id.ivT_AirLockRadar1);
            ivT_TankSearchRadar1 = FindViewById<ImageView>(Resource.Id.ivT_TankSearchRadar1);

            ivT_NVDCommander2 = FindViewById<ImageView>(Resource.Id.ivT_NVDCommander2);
            ivT_ThermalCommander2 = FindViewById<ImageView>(Resource.Id.ivT_ThermalCommander2);
            ivT_NVDGunner2 = FindViewById<ImageView>(Resource.Id.ivT_NVDGunner2);
            ivT_ThermalGunner2 = FindViewById<ImageView>(Resource.Id.ivT_ThermalGunner2);
            ivT_IRSpotlight2 = FindViewById<ImageView>(Resource.Id.ivT_IRSpotlight2);
            ivT_ExhaustSmoke2 = FindViewById<ImageView>(Resource.Id.ivT_ExhaustSmoke2);
            ivT_GrenadeSmoke2 = FindViewById<ImageView>(Resource.Id.ivT_GrenadeSmoke2);
            ivT_AddOnArmor2 = FindViewById<ImageView>(Resource.Id.ivT_AddOnArmor2);
            ivT_ReactiveArmor2 = FindViewById<ImageView>(Resource.Id.ivT_ReactiveArmor2);
            ivT_AutoLoader2 = FindViewById<ImageView>(Resource.Id.ivT_AutoLoader2);
            ivT_AAMachineGun2 = FindViewById<ImageView>(Resource.Id.ivT_AAMachineGun2);
            ivT_Stabilizer2 = FindViewById<ImageView>(Resource.Id.ivT_Stabilizer2);
            ivT_HullBreak2 = FindViewById<ImageView>(Resource.Id.ivT_HullBreak2);
            ivT_Hydropneumatic2 = FindViewById<ImageView>(Resource.Id.ivT_Hydropneumatic2);
            ivT_Amphibious2 = FindViewById<ImageView>(Resource.Id.ivT_Amphibious2);
            ivT_AirSearchRadar2 = FindViewById<ImageView>(Resource.Id.ivT_AirSearchRadar2);
            ivT_AirLockRadar2 = FindViewById<ImageView>(Resource.Id.ivT_AirLockRadar2);
            ivT_TankSearchRadar2 = FindViewById<ImageView>(Resource.Id.ivT_TankSearchRadar2);

            tvT_ReducedArmorFrontTurret1 = FindViewById<TextView>(Resource.Id.tvT_ReducedArmorFrontTurret1);
            tvT_ReducedArmorFrontTurret2 = FindViewById<TextView>(Resource.Id.tvT_ReducedArmorFrontTurret2);
            tvT_ReducedArmorTopSheet1 = FindViewById<TextView>(Resource.Id.tvT_ReducedArmorTopSheet1);
            tvT_ReducedArmorTopSheet2 = FindViewById<TextView>(Resource.Id.tvT_ReducedArmorTopSheet2);
            tvT_ReducedArmorBottomSheet1 = FindViewById<TextView>(Resource.Id.tvT_ReducedArmorBottomSheet1);
            tvT_ReducedArmorBottomSheet2 = FindViewById<TextView>(Resource.Id.tvT_ReducedArmorBottomSheet2);

            tvT_MaxSpeedAtRoad1 = FindViewById<TextView>(Resource.Id.tvT_MaxSpeedAtRoad1);
            tvT_MaxSpeedAtRoad2 = FindViewById<TextView>(Resource.Id.tvT_MaxSpeedAtRoad2);
            tvT_MaxSpeedAtTerrain1 = FindViewById<TextView>(Resource.Id.tvT_MaxSpeedAtTerrain1);
            tvT_MaxSpeedAtTerrain2 = FindViewById<TextView>(Resource.Id.tvT_MaxSpeedAtTerrain2);
            tvT_MaxReverseSpeed1 = FindViewById<TextView>(Resource.Id.tvT_MaxReverseSpeed1);
            tvT_MaxReverseSpeed2 = FindViewById<TextView>(Resource.Id.tvT_MaxReverseSpeed2);
            tvT_AccelerationTo1001 = FindViewById<TextView>(Resource.Id.tvT_AccelerationTo1001);
            tvT_AccelerationTo1002 = FindViewById<TextView>(Resource.Id.tvT_AccelerationTo1002);
            tvT_TurnTurretTime1 = FindViewById<TextView>(Resource.Id.tvT_TurnTurretTime1);
            tvT_TurnTurretTime2 = FindViewById<TextView>(Resource.Id.tvT_TurnTurretTime2);
            tvT_TurnHullTime1 = FindViewById<TextView>(Resource.Id.tvT_TurnHullTime1);
            tvT_TurnHullTime2 = FindViewById<TextView>(Resource.Id.tvT_TurnHullTime2);
            tvT_EnginePower1 = FindViewById<TextView>(Resource.Id.tvT_EnginePower1);
            tvT_EnginePower2 = FindViewById<TextView>(Resource.Id.tvT_EnginePower2);
            tvT_Weight1 = FindViewById<TextView>(Resource.Id.tvT_Weight1);
            tvT_Weight2 = FindViewById<TextView>(Resource.Id.tvT_Weight2);
        }
    }
}
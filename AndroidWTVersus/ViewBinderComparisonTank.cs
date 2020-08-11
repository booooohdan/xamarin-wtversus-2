using Android.Content;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace AndroidWTVersus
{
    public partial class ComparisonTankActivity
    {
        ImageView ivT_Tank1;
        ImageView ivT_Tank2;
        Spinner spinnerNation;
        Spinner spinnerRank;

        Button searchableButton1;
        Button searchableButton2;
        Button topMenuAircraftsButton;
        Button topMenuTanksButton;
        Button topMenuHeliButton;
        Button topMenuShipsButton;

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
        TextView tvT_PowerToWeight1;
        TextView tvT_PowerToWeight2;

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
            topMenuAircraftsButton = FindViewById<Button>(Resource.Id.topMenuAircraftsButton);
            topMenuTanksButton = FindViewById<Button>(Resource.Id.topMenuTanksButton);
            topMenuHeliButton = FindViewById<Button>(Resource.Id.topMenuHeliButton);
            topMenuShipsButton = FindViewById<Button>(Resource.Id.topMenuShipsButton);


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
            tvT_PowerToWeight1 = FindViewById<TextView>(Resource.Id.tvT_PowerToWeight1);
            tvT_PowerToWeight2 = FindViewById<TextView>(Resource.Id.tvT_PowerToWeight2);

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

        /// <summary>
        /// Initialize Custom Top Navigation Menu
        /// </summary>
        private void TopMenuInitializer()
        {
            topMenuTanksButton.SetBackgroundResource(Resource.Drawable.ButtonAsTabShape);
            topMenuTanksButton.SetTextColor(Color.ParseColor("#dc3546"));
            topMenuAircraftsButton.SetOnClickListener(this);
            topMenuTanksButton.SetOnClickListener(this);
            topMenuHeliButton.SetOnClickListener(this);
            topMenuShipsButton.SetOnClickListener(this);
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
                    break;
                case Resource.Id.topMenuHeliButton:
                    var intentHeli = new Intent(this, typeof(ComparisonHeliActivity));
                    intentHeli.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentHeli);
                    break;
                case Resource.Id.topMenuShipsButton:
                    var intentShip = new Intent(this, typeof(ComparisonShipActivity));
                    intentShip.AddFlags(ActivityFlags.NoAnimation);
                    StartActivity(intentShip);
                    break;
            }
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
    }
}
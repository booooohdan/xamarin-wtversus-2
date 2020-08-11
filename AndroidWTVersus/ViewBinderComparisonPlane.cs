using Android.Content;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace AndroidWTVersus
{
    public partial class ComparisonPlaneActivity
    {
        ImageView ivP_Plane1;
        ImageView ivP_Plane2;
        Spinner spinnerNation;
        Spinner spinnerRank;

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
        TextView tvP_PowerToWeight1;
        TextView tvP_PowerToWeight2;

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
            tvP_PowerToWeight1 = FindViewById<TextView>(Resource.Id.tvP_PowerToWeight1);
            tvP_PowerToWeight2 = FindViewById<TextView>(Resource.Id.tvP_PowerToWeight2);

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
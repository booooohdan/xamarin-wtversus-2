using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Widget;

namespace AndroidWTVersus.Helpers
{
    class CompareHelper
    {
        /// <summary>
        /// Change background to green when data is higher
        /// </summary>
        /// <param name="textViewLeft">Left TextView</param>
        /// <param name="textViewRight">Right TextView</param>
        public void CompareWhenHighIsGood(TextView textViewLeft, TextView textViewRight)
        {
            double digitFromTv1;
            double digitFromTv2;

            double.TryParse(string.Join("", textViewLeft.Text.Where(d => char.IsDigit(d))), out digitFromTv1);
            double.TryParse(string.Join("", textViewRight.Text.Where(d => char.IsDigit(d))), out digitFromTv2);

            if (digitFromTv1 == digitFromTv2)
            {
                textViewLeft.SetBackgroundColor(Android.Graphics.Color.Transparent);
                textViewRight.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            else
            if (digitFromTv1 > digitFromTv2)
            {
                textViewLeft.SetBackgroundResource(Resource.Drawable._LeftBest);
                textViewRight.SetBackgroundResource(Resource.Drawable._RightWorse);
            }
            else
            {
                textViewLeft.SetBackgroundResource(Resource.Drawable._LeftWorse);
                textViewRight.SetBackgroundResource(Resource.Drawable._RightBest);
            }
        }

        /// <summary>
        /// Change background to green when data is lower
        /// </summary>
        /// <param name="textViewLeft">Left TextView</param>
        /// <param name="textViewRight">Right TextView</param>
        public void CompareWhenLowIsGood(TextView textViewLeft, TextView textViewRight)
        {

            string stringLeft = Regex.Split(textViewLeft.Text, @"[^0-9\.]+")
            .Where(c => c != "." && c.Trim() != "").FirstOrDefault();
            string stringRight = Regex.Split(textViewRight.Text, @"[^0-9\.]+")
            .Where(c => c != "." && c.Trim() != "").FirstOrDefault();

            double digitFromTv1 = Convert.ToDouble(stringLeft, CultureInfo.InvariantCulture);
            double digitFromTv2 = Convert.ToDouble(stringRight, CultureInfo.InvariantCulture);


            if (digitFromTv1 == digitFromTv2)
            {
                textViewLeft.SetBackgroundColor(Android.Graphics.Color.Transparent);
                textViewRight.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
            else
            if (digitFromTv1 < digitFromTv2)
            {
                textViewLeft.SetBackgroundResource(Resource.Drawable._LeftBest);
                textViewRight.SetBackgroundResource(Resource.Drawable._RightWorse);
            }
            else
            {
                textViewLeft.SetBackgroundResource(Resource.Drawable._LeftWorse);
                textViewRight.SetBackgroundResource(Resource.Drawable._RightBest);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Object = Java.Lang.Object;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidWTVersus.DBEntities;

namespace AndroidWTVersus.Adapters
{
    class TankAdapter : BaseAdapter<Tank>, IFilterable
    {
        private List<Tank> _originalData;
        private List<Tank> _items;
        private readonly Activity _context;
        
        public TankAdapter(Activity activity, IEnumerable<Tank> tanks)
        {
            _items = tanks.OrderBy(x => x.VehicleId).ThenBy(x=>x.Nation).ToList();
            _context = activity;

            Filter = new TankFilter(this);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout._searchRow, null);

            var tanks = _items[position];

            var nameView = view.FindViewById<TextView>(Resource.Id.textViewRow);
            var imageView = view.FindViewById<ImageView>(Resource.Id.imageViewRow);

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            nameView.Text = tanks.Name;

            switch (tanks.Nation)
            {
                case "USA":
                    imageView.SetImageResource(Resource.Drawable.USA);
                    break;
                case "Germany":
                    imageView.SetImageResource(Resource.Drawable.Germany);
                    break;
                case "USSR":
                    imageView.SetImageResource(Resource.Drawable.USSR);
                    break;
                case "Britain":
                    imageView.SetImageResource(Resource.Drawable.Britain);
                    break;
                case "Japan":
                    imageView.SetImageResource(Resource.Drawable.Japan);
                    break;
                case "Italy":
                    imageView.SetImageResource(Resource.Drawable.Italy);
                    break;
                case "France":
                    imageView.SetImageResource(Resource.Drawable.France);
                    break;
                case "China":
                    imageView.SetImageResource(Resource.Drawable.China);
                    break;
                case "Sweden":
                    imageView.SetImageResource(Resource.Drawable.Sweden);
                    break;
            }

            switch (tanks.Type)
            {
                case "Premium":
                    nameView.SetBackgroundColor(Android.Graphics.Color.LightGoldenrodYellow);
                    imageView.SetBackgroundColor(Android.Graphics.Color.LightGoldenrodYellow);
                    break;
                case "Promotional":
                    nameView.SetBackgroundColor(Android.Graphics.Color.LightGoldenrodYellow);
                    imageView.SetBackgroundColor(Android.Graphics.Color.LightGoldenrodYellow);
                    break;
                case "Usual":
                    nameView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    imageView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    break;
            }

            return view;

        }
        public override int Count
        {
            get { return _items.Count; }
        }

        public override Tank this[int position]
        {
            get { return _items[position]; }
        }

        public Filter Filter { get; private set; }

        public override void NotifyDataSetChanged()
        {
            // If you are using cool stuff like sections
            // remember to update the indices here!
            base.NotifyDataSetChanged();
        }

        private class TankFilter : Filter
        {
            private readonly TankAdapter _adapter;
            public TankFilter(TankAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Tank>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter._originalData.Where(
                            chemical => chemical.Name.Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter._items = values.ToArray<Object>()
                        .Select(r => r.ToNetObject<Tank>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
        }
    }
}
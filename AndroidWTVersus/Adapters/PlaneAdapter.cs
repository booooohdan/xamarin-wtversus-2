using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Object = Java.Lang.Object;
using Android.Graphics;
using FFImageLoading;
using FFImageLoading.Work;
using Android.Media;
using AndroidWTVersus.Models;

namespace AndroidWTVersus.Adapters
{
    class PlaneAdapter:BaseAdapter<Plane>, IFilterable
    {
        private List<Plane> _originalData;
        private List<Plane> _items;
        private readonly Activity _context;

        public PlaneAdapter(Activity activity, IEnumerable<Plane> planes)
        {
            _items = planes.OrderBy(x => x.VehicleId).ThenBy(x => x.Nation).ToList();
            _context = activity;

            Filter = new PlaneFilter(this);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout._searchRow, null);

            var planes = _items[position];

            var imageView = view.FindViewById<ImageView>(Resource.Id.imageViewRow);
            var relativeViewRow = view.FindViewById<RelativeLayout>(Resource.Id.relativeViewRow);
            var nameView = view.FindViewById<TextView>(Resource.Id.nameViewRow);
            var brViewRow = view.FindViewById<TextView>(Resource.Id.brViewRow);
            var rankViewRow = view.FindViewById<TextView>(Resource.Id.rankViewRow);
            var repairViewRow = view.FindViewById<TextView>(Resource.Id.repairViewRow);

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var rank = _context.Resources.GetString(Resource.String.rank);
            var br = _context.Resources.GetString(Resource.String.br);
            var repair = _context.Resources.GetString(Resource.String.repair);

            nameView.Typeface = Typeface.CreateFromAsset(_context.Assets, "symbols.ttf");
            nameView.Text = planes.Name;
            rankViewRow.Text = rank + planes.Rank;
            brViewRow.Text = br + System.String.Format("{0:F1}", planes.BR);
            repairViewRow.Text = repair + planes.RepairCost.ToString();


            switch (planes.Nation)
            {
                case "USA":
                    //imageView.SetImageResource(Resource.Drawable.USA);
                    ImageService.Instance.LoadCompiledResource("USA").Into(imageView);
                    break;
                case "Germany":
                    //imageView.SetImageResource(Resource.Drawable.Germany);
                    ImageService.Instance.LoadCompiledResource("Germany").Into(imageView);
                    break;
                case "USSR":
                    //imageView.SetImageResource(Resource.Drawable.USSR);
                    ImageService.Instance.LoadCompiledResource("USSR").Into(imageView);
                    break;
                case "Britain":
                    //imageView.SetImageResource(Resource.Drawable.Britain);
                    ImageService.Instance.LoadCompiledResource("Britain").Into(imageView);
                    break;
                case "Japan":
                    //imageView.SetImageResource(Resource.Drawable.Japan);
                    ImageService.Instance.LoadCompiledResource("Japan").Into(imageView);
                    break;
                case "Italy":
                    //imageView.SetImageResource(Resource.Drawable.Italy);
                    ImageService.Instance.LoadCompiledResource("Italy").Into(imageView);
                    break;
                case "France":
                    //imageView.SetImageResource(Resource.Drawable.France);
                    ImageService.Instance.LoadCompiledResource("France").Into(imageView);
                    break;
                case "China":
                    //imageView.SetImageResource(Resource.Drawable.China);
                    ImageService.Instance.LoadCompiledResource("China").Into(imageView);
                    break;
                case "Sweden":
                    //imageView.SetImageResource(Resource.Drawable.Sweden);
                    ImageService.Instance.LoadCompiledResource("Sweden").Into(imageView);
                    break;
            }

            switch (planes.Type)
            {
                case "Premium":
                    relativeViewRow.SetBackgroundColor(Color.LightGoldenrodYellow);
                    relativeViewRow.SetBackgroundColor(Color.LightGoldenrodYellow);
                    break;
                case "Promotional":
                    relativeViewRow.SetBackgroundColor(Color.LightGoldenrodYellow);
                    relativeViewRow.SetBackgroundColor(Color.LightGoldenrodYellow);
                    break;
                case "Usual":
                    relativeViewRow.SetBackgroundColor(Color.Transparent);
                    relativeViewRow.SetBackgroundColor(Color.Transparent);
                    break;
            }

            return view;

        }
        public override int Count
        {
            get { return _items.Count; }
        }

        public override Plane this[int position]
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

        private class PlaneFilter : Filter
        {
            private readonly PlaneAdapter _adapter;
            public PlaneFilter(PlaneAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Plane>();
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
                        .Select(r => r.ToNetObject<Plane>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
        }
    }
}
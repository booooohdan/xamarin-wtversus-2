using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidWTVersus.DBEntities
{
    [XmlRoot(ElementName = "ArrayOfPlanes", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
    public class ArrayOfPlanes
    {
        [XmlElement(ElementName = "Planes", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
        public List<Plane> PlanesList { get; set; }
    }
}
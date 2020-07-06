using System.Collections.Generic;
using System.Xml.Serialization;

namespace AndroidWTVersus.DBEntities
{
    [XmlRoot(ElementName = "ArrayOfPlanes", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
    public class ArrayOfPlanes
    {
        [XmlElement(ElementName = "Planes", Namespace = "http://schemas.datacontract.org/2004/07/VehicleDataAccess")]
        public List<Plane> PlanesListApi { get; set; }
    }
}
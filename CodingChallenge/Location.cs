using GMap.NET;
using GMap.NET.MapProviders;
using CodingChallenge.Properties;
using System.Device.Location;

namespace CodingChallenge
{
    public class Location
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private CivicAddress CivicAddress { get; set; }
        private GeoCoordinate Coordinate { get; set; }

        /// <summary>Default constructor that generates the <see cref="CivicAddress"/> and 
        /// <see cref="Coordinate"/> properties.</summary>
        public Location()
        {
            CivicAddress = new CivicAddress();
            Coordinate = new GeoCoordinate();
        }

        /// <summary>Constructor that will populate the <see cref="CivicAddress"/> and <see cref="Coordinate"/> 
        /// properties.</summary>
        /// <param name="address">Input address of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.AddressLine1"/> property.</param>
        /// <param name="city">Input city of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.City"/> property.</param>
        /// <param name="state">Input state of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.StateProvince"/> property.</param>
        /// <param name="zip">Input zip/postal code of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.PostalCode"/> property.</param>
        /// <param name="latitude">Input latitude of type <see cref="double"/> that will populate the 
        /// <see cref="GeoCoordinate.Latitude"/> property.</param>
        /// <param name="longitude">Input longitude of type <see cref="double"/> that will populate the 
        /// <see cref="GeoCoordinate.Longitude"/> property.</param>
        public Location(string address, string city, string state, string zip, double latitude, double longitude)
        {
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>Constructor that will populate the <see cref="CivicAddress"/> and <see cref="Coordinate"/> 
        /// properties.</summary>
        /// <param name="address">Input address of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.AddressLine1"/> property.</param>
        /// <param name="city">Input city of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.City"/> property.</param>
        /// <param name="state">Input state of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.StateProvince"/> property.</param>
        /// <param name="zip">Input zip/postal code of type <see cref="string"/> that will populate the 
        /// <see cref="CivicAddress.PostalCode"/> property.</param>
        /// <remarks>NOT WORKING!!!</remarks>
        public Location(string address, string city, string state, string zip)
        {
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            GenerateCivicAddress();
            Coordinate = PopulateCoordinatesFromCivicAddress();
        }

        /// <summary>Constructor that will populate the <see cref="CivicAddress"/> and <see cref="Coordinate"/> 
        /// properties.</summary>
        /// <param name="latitude">Input latitude of type <see cref="double"/> that will populate the 
        /// <see cref="GeoCoordinate.Latitude"/> property.</param>
        /// <param name="longitude">Input longitude of type <see cref="double"/> that will populate the 
        /// <see cref="GeoCoordinate.Longitude"/> property.</param>
        /// <remarks>NOT WORKING!!! Get this working eventually.</remarks>
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            GenerateGeoCoordinate();
        }

        /// <summary>Overridden Equals method that compares all public properties to the 
        /// <paramref name="obj"/>.</summary>
        /// <param name="obj">The input <see cref="Location"/> object.</param>
        /// <returns><b>true</b> if all public properties are equal; otherwise <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Location loc = (Location)obj;
                bool isEqual = string.Equals(Address, loc.Address)
                        && string.Equals(City, loc.City)
                        && string.Equals(State, loc.State)
                        && string.Equals(Zip, loc.Zip)
                        && Latitude == loc.Latitude
                        && Longitude == loc.Longitude;
                return isEqual;
            }
        }

        /// <summary>Overridden GethashCode method.</summary>
        /// <returns>Generated HashCode based on Jon Skeet's answer on Stackoverflow.
        /// <para>https://stackoverflow.com/a/263416/1886644</para></returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                
                hash = hash * 23 + Address.GetHashCode();
                hash = hash * 23 + City.GetHashCode();
                hash = hash * 23 + State.GetHashCode();
                hash = hash * 23 + Zip.GetHashCode();
                hash = hash * 23 + Latitude.GetHashCode();
                hash = hash * 23 + Longitude.GetHashCode();
                return hash;
            }
        }
        /// <summary>Method that populates the <see cref="CivicAddress"/> object with values 
        /// from this class.</summary>
        private void GenerateCivicAddress()
        {
            CivicAddress = new CivicAddress
            {
                AddressLine1 = Address,
                City = City,
                StateProvince = State,
                PostalCode = Zip,
                CountryRegion = "US"
            };
        }

        /// <summary>Method that populates the <see cref="Coordinate"/> object with values 
        /// from this class.</summary>
        private void GenerateGeoCoordinate()
        {
            Coordinate = new GeoCoordinate
            {
                Longitude = Longitude,
                Latitude = Latitude
            };
        }

        /// <summary>This method checks to see if the object is invalid.</summary>
        /// <returns><b>true</b> if any of the properties are null, empty or out of bounds. 
        /// Latitude has a range of -90 through 90 and Longitude has a range of -180 through 180.</returns>
        public bool IsInvalid()
        {
            return string.IsNullOrEmpty(Address)
                || string.IsNullOrEmpty(City)
                || string.IsNullOrEmpty(State)
                || string.IsNullOrEmpty(Zip)
                || Latitude == 0  | Latitude > 90   | Latitude < -90
                || Longitude == 0 | Longitude > 180 | Longitude < -180;
        }

        /// <summary>This method is meant to populate the Address from the Coordinates but I was 
        /// never able to get it working correctly.</summary>
        /// <returns>A <see cref="CivicAddress"/> object.</returns>
        private CivicAddress PopulateAddressFromCoordinates()
        {
            CivicAddressResolver resolver = new CivicAddressResolver();
            var coord = resolver.ResolveAddress(Coordinate);
            if (!coord.IsUnknown)
            {
                //Not working.
            }
            return coord;
        }

        /// <summary>Creates a <see cref="GeoCoordinate"/> object from the <see cref="CivicAddress"/> property.</summary>
        /// <returns>A <see cref="GeoCoordinate"/> object.</returns>
        /// <remarks>Uses the <see cref="BingMapProvider"/> to resolve the <see cref="PointLatLng"/> 
        /// that is used to create the <see cref="GeoCoordinate"/> object.  The code was taken from a test method in: 
        /// https://github.com/judero01col/GMap.NET/blob/master/Testing/Demo.Geocoding/Form1.cs
        /// 
        /// NOT WORKING!!!  PointLatLng does not end up getting populated.</remarks>
        private GeoCoordinate PopulateCoordinatesFromCivicAddress()
        {
            GeoCoordinate output = GeoCoordinate.Unknown;
            BingMapProvider.Instance.ClientKey = Resources.BingMapsKey;
            Placemark placemark = new Placemark
            {
                CountryNameCode = CivicAddress.CountryRegion,
                StreetAddress = CivicAddress.AddressLine1,
                PostalCodeNumber = CivicAddress.PostalCode,
                LocalityName = CivicAddress.City
            };

            PointLatLng? position = GMapProviders.BingMap.GetPoint(placemark, out GeoCoderStatusCode status);
            if (position != null && status == GeoCoderStatusCode.OK) // status always returns 'EXCEPTION_IN_CODE'
            {
                output = new GeoCoordinate
                {
                    Latitude = position.Value.Lat,
                    Longitude = position.Value.Lng
                };
            }

            return output;
        }

    }
}

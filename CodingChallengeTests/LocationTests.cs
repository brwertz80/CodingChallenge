using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace CodingChallenge.Tests
{
    [TestClass()]
    public class LocationTests
    {
        [TestMethod()]
        public void PopulateCoordinatesFromCivicAddressTest()
        {
            string address = "2900 Falcon View Drive";
            string city = "Wellington";
            string state = "CO";
            string zip = "80549";
            //Location location = new Location(address, city, state, zip);
            //System.Device.Location.GeoCoordinate coord = location.Coordinate;
            //Assert.IsNotNull(coord);
        }

        [TestMethod()]
        public void PopulateAddressFromCoordinatesTest()
        {
            double lat = 40.57309;
            double lon = -105.019432;
            Location location = new Location(lat, lon);
            //Console.WriteLine(location.CivicAddress.AddressLine1);
            Thread.Sleep(10000);
            //Assert.IsTrue(!string.IsNullOrEmpty(location.CivicAddress.AddressLine1));
        }

        [TestMethod()]
        public void IsInvalid_Address_Test()
        {
            Location location = new Location("", "Lynchburg", "VA", "24504", 37.4136111, -79.1425);
            Assert.IsTrue(location.IsInvalid());
        }

        [TestMethod()]
        public void IsInvalid_City_Test()
        {
            Location location = new Location("900 Church St", "", "VA", "24504", 37.4136111, -79.1425);
            Assert.IsTrue(location.IsInvalid());
        }

        [TestMethod()]
        public void IsInvalid_State_Test()
        {
            Location location = new Location("900 Church St", "Lynchburg", "", "24504", 37.4136111, -79.1425);
            Assert.IsTrue(location.IsInvalid());
        }

        [TestMethod()]
        public void IsInvalid_Zip_Test()
        {
            Location location = new Location("900 Church St", "Lynchburg", "VA", "", 37.4136111, -79.1425);
            Assert.IsTrue(location.IsInvalid());
        }

        [TestMethod()]
        public void IsInvalid_Latitude_Low_Test()
        {
            Location location = new Location("900 Church St", "Lynchburg", "VA", "24504", double.NegativeInfinity, -79.1425);
            Assert.IsTrue(location.IsInvalid());
        }

        [TestMethod()]
        public void IsInvalid_Latitude_High_Test()
        {
            Location location = new Location("900 Church St", "Lynchburg", "VA", "24504", double.MaxValue, -79.1425);
            Assert.IsTrue(location.IsInvalid());
        }

        [TestMethod()]
        public void IsInvalid_Longitude_Low_Test()
        {
            Location location = new Location("900 Church St", "Lynchburg", "VA", "24504", 37.4136111, double.NegativeInfinity);
            Assert.IsTrue(location.IsInvalid());
        }

        [TestMethod()]
        public void IsInvalid_Longitude_High_Test()
        {
            Location location = new Location("900 Church St", "Lynchburg", "VA", "24504", 37.4136111, double.MaxValue);
            Assert.IsTrue(location.IsInvalid());
        }

    }
}
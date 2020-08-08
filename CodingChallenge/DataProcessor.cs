using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge
{
    public abstract class DataProcessor
    {
        public BindingList<Location> ImportedLocations { get; protected set; } = new BindingList<Location>();

        public SortedDictionary<double, Location> ClosestLocations { get; protected set; } = new SortedDictionary<double, Location>();

        public int DataRowCount { get; protected set; } = 0;

        public event EventHandler DataUpdated;

        protected abstract string DefaultFileName { get; }

        /// <summary>Abstract method that must be initialized.</summary>
        /// <param name="fileName">Input filename.</param>
        public abstract Task LoadDataAsync(string fileName);


        /// <summary>This method filters the data based on the <paramref name="inputFilter"/>.</summary>
        /// <param name="inputFilter">The <see cref="string"/> to filter on.</param>
        /// <returns>A filtered <see cref="BindingList{Location}"/>.</returns>
        public BindingList<Location> GetFilteredData(string inputFilter)
        {
            List<Location> filteredLocs = ImportedLocations
                .Where(loc => loc.Address.ToUpper().Contains(inputFilter.ToUpper())) // convert to upper to compare
                .ToList();

            DataRowCount = filteredLocs.Count - 1; // need to ignore headers
            OnDataUpdated(EventArgs.Empty);
            return new BindingList<Location>(filteredLocs);
        }

        /// <summary>This method gets the 10 closest surrounding locations.</summary>
        /// <param name="inputLocationList">The ref <see cref="BindingList{Location}}"/> object.</param>
        /// <param name="selectedLocation">The ref <see cref="Location"/> object.</param>
        /// <param name="selectedIndex">The index of the <paramref name="selectedLocation"/> from 
        /// the <paramref name="inputLocationList"/>.</param>
        /// <param name="numSurroundingLocations">The number of surrounding locations to retrieve. 
        /// Default is 10 but I allowed it to be passed in to provide flexibility.</param>
        /// <remarks>The comments in the code essentially document the logic used to get the locations.</remarks>
        /// <returns></returns>
        private SortedDictionary<double, Location> GetSurroundingLocations(
                                                        ref BindingList<Location> inputLocationList,
                                                        ref Location selectedLocation,
                                                        int numSurroundingLocations)
        {
            // using a Dictionary that automatically sorts the rows based on the key
            SortedDictionary<double, Location> sortedDict = new SortedDictionary<double, Location>();
            GeoCoordinate startCoordinate = new GeoCoordinate(selectedLocation.Latitude, selectedLocation.Longitude);

            // calculate the distance between the two locations and store into a dictionary.
            foreach (Location loc in inputLocationList)
            {
                GeoCoordinate compareCoordinate = new GeoCoordinate(loc.Latitude, loc.Longitude);
                double distance = startCoordinate.GetDistanceTo(compareCoordinate);
                // don't want to return duplicate locations
                if (!sortedDict.ContainsKey(distance) && !loc.Equals(selectedLocation))
                {
                    sortedDict.Add(distance, loc);
                }
            }

            SortedDictionary<double, Location> resultSortedDict = new SortedDictionary<double, Location>();
            for (int i = 0; i < numSurroundingLocations; i++) 
            {
                resultSortedDict.Add(sortedDict.ElementAt(i).Key, sortedDict.ElementAt(i).Value);
            }

            return resultSortedDict;
        }

        /// <summary>This method loads the <paramref name="numClosestLocations"/> closest locations 
        /// to the <paramref name="selectedLocation"/>.</summary>
        /// <param name="selectedLocation">The selected <see cref="Location"/>.</param>
        /// <param name="numClosestLocations">The number of locations to retrieve.
        /// Default is 10 but I allowed it to be passed in to provide flexibility.</param>
        public void LoadClosestLocations(Location selectedLocation, int numClosestLocations)
        {
            // first order by Latitude, then by Longitude
            var sortedLocationList = new BindingList<Location>(ImportedLocations
                                                               .OrderBy(x => x.Latitude)
                                                               .ThenBy(x => x.Longitude)
                                                               .ToList());

            ClosestLocations = GetSurroundingLocations(ref sortedLocationList,
                                                       ref selectedLocation,
                                                       numClosestLocations);
        }

        /// <summary>This method removes all of the duplicate <see cref="Location"/> in the 
        /// input <param name="locations"/> object.</summary>
        /// <param name="locations">The referenced input <see cref="List{Location}"/> object.</param>
        protected void MakeListUnique(ref List<Location> locations)
        {
            List<Location> uniqueLocationList = new List<Location>();
            locations.ForEach((loc) =>
            {
                if (!uniqueLocationList.Contains(loc))
                {
                    uniqueLocationList.Add(loc);
                }
            });

            locations = uniqueLocationList;
        }

        /// <summary>This method removes all of the Invalid <see cref="Location"/> in the 
        /// input <param name="locations"/> object.</summary>
        /// <param name="locations">The referenced input <see cref="List{Location}"/> object.</param>
        protected void RemoveInvalidLocations(ref List<Location> locations)
        {
            locations = locations.Where(loc => !loc.IsInvalid()).ToList();
        }

        /// <summary>Event Handler for the DataUpdated <see cref="EventHandler"/>.</summary>
        /// <param name="e">The <see cref="EventArgs"/> object.</param>
        public void OnDataUpdated(EventArgs e)
        {
            EventHandler handler = DataUpdated;
            handler?.Invoke(this, e);
        }
    }
}

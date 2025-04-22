using System.Collections.Generic;
using System.Linq;
using Group4_Web_App.Models;

namespace Group4_Web_App.Services
{
    public class ParksService
    {
        // In-memory list to store park data
        private readonly List<Park> ParksList = new List<Park>();

        // Flag to track if API data has been fetched
        private bool isDataFetched = false;

        // Method to check if the data has been fetched from the API
        public bool IsDataFetched() => isDataFetched;

        // Method to mark data as fetched
        public void MarkDataAsFetched() => isDataFetched = true;

        // Method to check if the list is empty
        public bool IsParksListEmpty() => ParksList.Count == 0;

        // Method to add parks to the list
        public void AddParks(List<Park> parks)
        {
            ParksList.AddRange(parks);
        }

        // Create: Add a new park
        public void AddPark(Park park)
        {
            ParksList.Add(park);
        }

        // Read: Get all parks
        public List<Park> GetAllParks()
        {
            return ParksList;
        }

        // Read: Get a specific park by ID
        public Park GetParkById(string id)
        {
            return ParksList.FirstOrDefault(p => p.id == id);
        }

        // Update: Update a specific park by ID
        public bool UpdatePark(string id, Park updatedPark)
        {
            var existingPark = ParksList.FirstOrDefault(p => p.id == id);
            if (existingPark != null)
            {
                existingPark.fullName = updatedPark.fullName;
                existingPark.parkCode = updatedPark.parkCode;
                existingPark.description = updatedPark.description;
                existingPark.latLong = updatedPark.latLong;
                return true;
            }
            return false;
        }

        // Delete: Remove a specific park by ID
        public bool DeletePark(string id)
        {
            var park = ParksList.FirstOrDefault(p => p.id == id);
            if (park != null)
            {
                ParksList.Remove(park);
                return true;
            }
            return false;
        }
    }
}

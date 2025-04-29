using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_EF_Start_8.Models;

namespace MVC_EF_Start_8.Services
{
    public class NuclearOutageService
    {
        private readonly List<OutageRecord> _outageData = new List<OutageRecord>();
        private bool _isDataFetched = false;

        public bool IsDataFetched() => _isDataFetched;
        public void MarkDataAsFetched() => _isDataFetched = true;

        public async Task AddOutagesAsync(List<OutageRecord> data)
        {
            await Task.Run(() => _outageData.AddRange(data));
        }

        public async Task<List<OutageRecord>> GetAllOutagesAsync()
        {
            return await Task.FromResult(_outageData);
        }

        public async Task<List<OutageRecord>> GetLatestOutagesAsync(int count)
        {
            return await Task.FromResult(_outageData
                .OrderByDescending(d => d.period)
                .Take(count)
                .ToList());
        }

        public async Task<OutageRecord> GetOutageByFacilityAsync(string facility)
        {
            return await Task.FromResult(
                _outageData.FirstOrDefault(r => r.facility == facility));
        }

        public async Task<List<OutageRecord>> SearchOutagesAsync(string query)
        {
            return await Task.FromResult(
                _outageData
                .Where(r => r.facility.Contains(query) || r.facilityName.Contains(query))
                .ToList());
        }

        public async Task AddOutageAsync(OutageRecord record)
        {
            await Task.Run(() =>
            {
                if (!_outageData.Any(r => r.facility == record.facility))
                {
                    _outageData.Add(record);
                }
            });
        }

        public async Task<bool> UpdateOutageAsync(OutageRecord updatedRecord)
        {
            return await Task.Run(() =>
            {
                var existing = _outageData.FirstOrDefault(r => r.facility == updatedRecord.facility);
                if (existing != null)
                {
                    existing.period = updatedRecord.period;
                    existing.facilityName = updatedRecord.facilityName;
                    existing.generator = updatedRecord.generator;
                    existing.capacity = updatedRecord.capacity;
                    existing.outage = updatedRecord.outage;
                    existing.percentOutage = updatedRecord.percentOutage;
                    return true;
                }
                return false;
            });
        }

        public async Task<bool> DeleteOutageAsync(string facility)
        {
            return await Task.Run(() =>
            {
                var record = _outageData.FirstOrDefault(r => r.facility == facility);
                if (record != null)
                {
                    _outageData.Remove(record);
                    return true;
                }
                return false;
            });
        }
    }
}
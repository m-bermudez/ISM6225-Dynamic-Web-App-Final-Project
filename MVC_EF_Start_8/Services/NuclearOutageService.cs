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

        public async Task<List<OutageRecord>> GetOutageDataByPeriodAsync(string period)
        {
            return await Task.FromResult(_outageData.Where(d => d.period == period).ToList());
        }
    }
}
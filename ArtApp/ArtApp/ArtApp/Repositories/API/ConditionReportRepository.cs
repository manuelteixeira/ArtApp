using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories
{
    public class ConditionReportRepository : IDisposable
    {
        private bool disposed = false;

        //COMPLETAR COM ENDEREÇO DA API
        private RestApiService<ConditionReport, string> conditionReportClient = new RestApiService<ConditionReport, string>("http://ninjatips.azurewebsites.net/tables/Orders", "");

        public async Task<List<ConditionReport>> GetConditionReportsAsync()
        {
            return await conditionReportClient.GetManyAsync();
        }

        public async Task<ConditionReport> GetConditionReportAsync(string id)
        {
            return await conditionReportClient.GetAsync(id);
        }

        public async Task<ConditionReport> PostConditionReportAsync(ConditionReport conditionReport)
        {
            return await conditionReportClient.PostAsync(conditionReport);
        }

        public async Task<ConditionReport> PutConditionReportAsync(string id, ConditionReport conditionReport)
        {
            return await conditionReportClient.PutAsync(id, conditionReport);
        }

        public async Task DeleteConditionReportAsync(string id)
        {
            await conditionReportClient.DeleteAsync(id);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtApp.Models;
using ArtApp.Services;

namespace ArtApp.Repositories
{
    public class WorkRepository : IDisposable
    {
        private bool disposed = false;

        //COMPLETAR COM ENDEREÇO DA API
        private RestApiService<Work, string> workClient = new RestApiService<Work, string>("http://ninjatips.azurewebsites.net/tables/Orders", "");

        public async Task<List<Work>> GetWorksAsync()
        {
            return await workClient.GetManyAsync();
        }

        public async Task<Work> GetWorkAsync(string id)
        {
            return await workClient.GetAsync(id);
        }

        public async Task<Work> PostWorkAsync(Work work)
        {
            return await workClient.PostAsync(work);
        }

        public async Task<Work> PutWorkAsync(string id, Work work)
        {
            return await workClient.PutAsync(id, work);
        }

        public async Task DeleteWorkAsync(string id)
        {
            await workClient.DeleteAsync(id);
        }

        public void Dispose()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtApp.Model;
using ArtApp.Services;

namespace ArtApp.Repositories
{
    public class ProjectRepository : IDisposable
    {
        private bool disposed = false;

        //COMPLETAR COM ENDEREÇO DA API
        private RestApiService<Project, string> projectClient = new RestApiService<Project, string>("http://ninjatips.azurewebsites.net/tables/Orders", "");

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await projectClient.GetManyAsync();
        }

        public async Task<Project> GetProjectAsync(string id)
        {
            return await projectClient.GetAsync(id);
        }

        public async Task<Project> PostProjectAsync(Project project)
        {
            return await projectClient.PostAsync(project);
        }

        public async Task<Project> PutProjectAsync(string id, Project project)
        {
            return await projectClient.PutAsync(id, project);
        }

        public async Task DeleteProjectAsync(string id)
        {
            await projectClient.DeleteAsync(id);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

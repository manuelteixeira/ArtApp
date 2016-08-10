using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtApp.Model;

namespace ArtApp.Repositories
{
    public class ProjectMockRepository : IDisposable
    {
        public async Task<List<Project>> GetProjectsAsync()
        {
            return new List<Project>()
            {
                new Project()
                {
                    Name = "Project 1",
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Today.AddDays(3),
                    Works = new List<Work>()
                    {
                        new Work()
                        {
                            Title = "Guernica"
                        }
                    }
                },
            };
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            Project project = new Project()
            {
                Name = "Project 1",
                BeginDate = DateTime.Now,
                EndDate = DateTime.Today.AddDays(3),
                Works = new List<Work>()
                {
                    new Work()
                    {
                        Title = "Guernica"
                    }
                }
            };

            return project;
        }

        public async Task<Project> PostProjectAsync(Project project)
        {
            if (project != null)
            {
                return new Project()
                {
                    Name = "Project 1",
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Today.AddDays(3),
                    Works = new List<Work>()
                {
                    new Work()
                    {
                        Title = "Guernica"
                    }
                }
                };
            }
            return null;

        }

        public async Task<Project> PutProjectAsync(string id, Project project)
        {
            if (id.Equals("1"))
            {
                return new Project()
                {
                    Name = "Project 1",
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Today.AddDays(3),
                    Works = new List<Work>()
                {
                    new Work()
                    {
                        Title = "Guernica"
                    }
                }
                };
            }
            return null;

        }

        public async Task DeleteProjectAsync(string id)
        {
        }

        public void Dispose()
        {
        }
    }
}

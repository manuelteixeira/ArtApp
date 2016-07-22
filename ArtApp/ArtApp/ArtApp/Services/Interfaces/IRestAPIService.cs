using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtApp.Services.Interfaces
{
    public interface IRestApiService<T, TResourceIdentifier> where T : class
    {

        Task<List<T>> GetManyAsync();
        Task<T> GetAsync(TResourceIdentifier identifier);
        Task<T> PostAsync(T model);
        Task<T> PutAsync(TResourceIdentifier identifier, T model);
        Task DeleteAsync(TResourceIdentifier identifier);

    }
}

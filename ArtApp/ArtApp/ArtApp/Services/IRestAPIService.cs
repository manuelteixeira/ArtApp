using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using ArtApp.Annotations;

namespace ArtApp.Services
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

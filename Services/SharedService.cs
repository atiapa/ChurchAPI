using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchApi.Services
{
    public interface IModelService<T>
    {
        Task<T> FindAsync(int id);
        Task<List<T>> FetchAllAsync();
        Task<long> Save(T record);
        Task<long> Update(T record);
        Task<bool> Delete(int id);


    }

}

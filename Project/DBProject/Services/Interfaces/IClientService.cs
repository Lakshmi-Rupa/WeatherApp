using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBProject.Services.Interfaces
{
    public interface IClientService
    {
        Task<T> GetAsync<T>(string url) where T : class, new();
        Task<T> PostAsync<T>(object body);

    }
}

using System;

namespace HRBN.Thesis.CRMExpert.Application.Core.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set(string key, object value, TimeSpan expirationTime);
        void Remove(string key);
    }
}
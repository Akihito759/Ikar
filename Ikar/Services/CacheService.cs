using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Services
{
    public class CacheService<T>
    {
        private List<T> cachedata = new List<T>();


        public void Add(T data)
        {
            cachedata.Add(data);
        }

        public List<T> Get()
        {
            return cachedata;
        }

        public void Clear()
        {
            cachedata.Clear();
        }

    }
}

using System;
using System.Collections.Generic;

namespace Sample.SharedKernel.Application
{
    [Serializable]
    public class Pagination<TResult> 
    {
        public List<TResult> Items { get; set; }

        public int TotalItems { get; set; }

        public Pagination()
        {
            Items = new List<TResult>();
        }
    }
}

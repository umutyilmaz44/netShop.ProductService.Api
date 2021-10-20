using System;
using System.Collections.Generic;
using netShop.Domain.Entities;

namespace netShop.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public long TotalCount { get; set; }

        public PagedResponse(T data, int currentPage, int pageSize, long totalCount) : base(data)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalCount = totalCount;
            this.TotalPages = (totalCount == 0 || pageSize == 0) 
                            ? 0 
                            : ((double)totalCount / (double)pageSize) * pageSize ==  totalCount 
                                ? (int)(totalCount / pageSize) + 1
                                : (int)(totalCount / pageSize);
            this.Succeeded = true;
        }

        public PagedResponse(string failureType, IDictionary<string, string[]> failures) : base(failureType, failures)
        {
        }
    }
}
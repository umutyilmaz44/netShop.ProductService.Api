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
            this.TotalPages = (int)(totalCount / pageSize) + 1;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }

        public PagedResponse(string message, string[] errors) : base(message, errors)
        {
        }

        public PagedResponse(string message) : base(message)
        {
        }

        public static implicit operator PagedResponse<T>(PagedResponse<IEnumerable<Product>> v)
        {
            throw new NotImplementedException();
        }
    }
}
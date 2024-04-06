using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaoJWT.Domain.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int count)
        { 
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);//o total de paginas e total de itens divido pela quantidade de intens por paginas
            PageSize = pageSize;
            TotalCount = count;

            AddRange(items);
        }

        public PagedList(IEnumerable<T> items, int currentPage, int totalPages, int pageSize, int count)
        {           
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = count;

            AddRange(items);
        }
    }
}

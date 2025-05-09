using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using MotorSolutionNet.Models;

namespace MotorSolutionNet.Services
{
    public class PaginationHelper
    {

        public  Paginator<T> Paginate<T>(IEnumerable<T> source, int pageIndex,int pageSize) {
            int totalCount = source.Count();

            if (totalCount == 0)
            {
                return new Paginator<T>
                {
                    PageIndex = 0,
                    PageSize = 0,
                    TotalCount = 0,
                    TotalPages = 0,
                    Data = new List<T>()
                };
            }

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = source.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return new Paginator<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = items
            };


        }
    }
}
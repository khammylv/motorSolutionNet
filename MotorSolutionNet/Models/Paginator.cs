using System;
using System.Collections.Generic;


namespace MotorSolutionNet.Models
{
    public class Paginator<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; }
    }
}
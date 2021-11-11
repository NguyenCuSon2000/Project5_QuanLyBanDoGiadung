using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ResponseModel
    {
        public long TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public dynamic Data { get; set; } 
    }
}

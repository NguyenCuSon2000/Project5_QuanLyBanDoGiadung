using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CartItem
    {
        public int quantity { set; get; }
        public SanPhamModel product { set; get; }
    }
}

using E_CommerceProject.Models;
using E_CommerceProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.WishList.Dto
{
  public class wishlistproducts
    {
        public int ProductId {  get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; }
        public List<ProductSize> Sizes {  get; set; } 
        public string ProductImage { get; set; }

        public decimal Price { get; set; }

    }
}

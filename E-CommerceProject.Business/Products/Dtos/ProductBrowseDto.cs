using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.Products.Dtos
{
    public class ProductBrowseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public decimal? Discount { get; set; }
        public List<SizeQuantityDto> Sizes { get; set; }
        public int BrandId { get; set; }

    }
}

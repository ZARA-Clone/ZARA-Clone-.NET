using E_CommerceProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.Products.Dtos
{
    public class SizeQuantityDto
    {
        public SizeEnum Size { get; set; }
        public int Quantity { get; set; }
    }
}

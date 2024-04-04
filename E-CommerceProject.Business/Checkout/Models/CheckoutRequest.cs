using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Business.Checkout.Models
{
    public class CheckoutRequest
    {
        public string Token { get; set; }
        public AdditionalData AdditionalData { get; set; }
    }
}

using E_CommerceProject.Models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();

    }
}

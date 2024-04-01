namespace E_CommerceProject.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Brand> Brands { get; set; } = new List<Brand>();

    }
}

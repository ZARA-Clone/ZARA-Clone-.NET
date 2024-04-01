namespace E_CommerceProject.Business.Brands.Dtos
{
    public class BrandReadOnlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int NoOfProducts { get; set; }
    }
}

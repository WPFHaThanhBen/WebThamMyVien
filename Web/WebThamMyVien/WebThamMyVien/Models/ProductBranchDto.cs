namespace WebThamMyVien.Models
{
    public class ProductBranchDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? BranchId { get; set; }
        public int? Quantity { get; set; }
    }
}

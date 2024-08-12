namespace Para.Schema.Entities.DTOs.Report;

public class OrderDetailReportDTO
{
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

namespace Wisr.Customer.Api.Database.Models
{
    public class Fee
    {
        public int Id { get; set; }
        public required int Amount { get; set; }
        public required int Threshold { get; set; }
    }
}

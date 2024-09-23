using System.ComponentModel.DataAnnotations;

namespace Wisr.Customer.Api.Database.Models
{
    public class Customer
    {
        public required int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        [Range(30000, 120000)]
        public required decimal Income { get; set; }
    }
}

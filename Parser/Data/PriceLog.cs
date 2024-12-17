using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Parser.Data
{
    [Keyless]
    public class PriceLog
    {
        public int ProductId { get; set; }
        public string DateTime { get; set; }
        public string Price {  get; set; }
    }
}

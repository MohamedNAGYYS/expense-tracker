using System.ComponentModel.DataAnnotations;

namespace Finance.Models
{
    public class Expense
    {
        public int Id {get; set;}

        [Required] // Description required
        public string Description {get; set;} = string.Empty;
        
        [Required] // Amount is required
        [Range(0.01,double.MaxValue, ErrorMessage = "Amount must be greater than Zero.")] // If Range is zero or less, display this msg
        public double Amount {get; set;}
        
        
        [Required] // Category is required
        public string Category {get; set;} = string.Empty;
        
        public DateTime Date {get; set;} = DateTime.Now;
    }
}
using System.ComponentModel.DataAnnotations;

namespace Library.Strategies.MinNotional
{
    public class MinNotionalStrategyOptions
    {
        [Required]
        public decimal MinNotional { get; set; }
    }
}
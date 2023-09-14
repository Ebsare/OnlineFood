using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Models
{
    public class KidsMeal
    {
        public int KidsMealId { get; set; }
        public string PhotoFileName { get; set; }
        public string EmriKidsMeal { get; set; }
        public decimal Cmimi { get; set; }
        public string DateOfOrder { get; set; }
    }
}

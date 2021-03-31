using System;
using System.Collections.Generic;

namespace SharpenTheSaw.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Bowlers> Bowlers { get; set; }

        public PageNumberingInfo PageNumberingInfo { get; set; }

        public string TeamName { get; set; }
    }
}

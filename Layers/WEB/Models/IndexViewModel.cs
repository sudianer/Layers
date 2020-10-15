using System.Collections.Generic;

namespace WEB.Models
{
    public class IndexViewModel
    {
        public IEnumerable<DishViewModel> dishes { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}

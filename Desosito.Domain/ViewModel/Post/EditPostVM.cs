using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.ViewModel
{
    public class EditPostVM
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime? EditDateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.ViewModel
{
    public class DeletePostCommentVM
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string? UserName { get; set; }
        public DateTime? DeleteDateTime { get; set; }
    }
}

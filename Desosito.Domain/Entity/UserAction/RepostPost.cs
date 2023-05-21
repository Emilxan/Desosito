using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.Entity.UserAction
{
    public class RepostPost
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDatetime { get; set; }
    }
}

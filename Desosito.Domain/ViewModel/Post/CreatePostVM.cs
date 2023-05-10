using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.ViewModel
{
    public class CreatePostVM
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string Body { get; set; }
        public int? LikeScore { get; set; }
        public int? CommentScore { get; set; }
        public int? RepostScore { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}

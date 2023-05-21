using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.ViewModel
{
    public class CreatePostCommentVM
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid? AnswerCommentId { get; set; }
        public string? UserName { get; set; }
        public string Body { get; set; }
        public int? LikeScore { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}

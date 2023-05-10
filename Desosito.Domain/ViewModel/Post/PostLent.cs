using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.Domain.ViewModel.Post
{
    public class PostLent
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string Body { get; set; }
        public int? LikeScore { get; set; }
        public int? CommentScore { get; set; }
        public int? RepostScore { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }

        public UserProfileVM UserProfile { get; set; }
    }
}

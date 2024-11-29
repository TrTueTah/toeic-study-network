using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.CommentDto
{
    public class CommentResponseDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [AllowNull]
        public List<string>? MediaUrls { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string UserImageUrl { get; set; }
    }
}
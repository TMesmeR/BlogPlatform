using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Dtos
{
    public class CommentDto
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}

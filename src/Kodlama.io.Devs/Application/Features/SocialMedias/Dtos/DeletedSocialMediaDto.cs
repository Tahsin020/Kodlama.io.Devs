using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMedias.Dtos
{
    public class DeletedSocialMediaDto
    {
        public int UserId { get; set; }
        public string Url { get; set; }
        public string SocialMediaType { get; set; }
    }
}

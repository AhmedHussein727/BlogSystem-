using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.DTOs.IdentityDTOs
{
    public class AuthResponseDto<Value>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public Value? Data { get; set; }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dtos {
    public class UserLoginDto : IDto {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

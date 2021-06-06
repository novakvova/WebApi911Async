using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGallery.Entities.Identity;

namespace WebGallery.Services
{
    public interface IJwtTokenService
    {
        string CreateToken(AppUser user);
    }
}

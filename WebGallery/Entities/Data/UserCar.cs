using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebGallery.Entities.Identity;

namespace WebGallery.Entities.Data
{
    [Table("tblUserCars")]
    public class UserCar
    {
        public long CarId { get; set; }
        public virtual Car Car { get; set; }
        public long UserId { get; set; }
        public virtual AppUser User { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UIHelper;
using WebGallery.Entities;
using WebGallery.Entities.Data;
using WebGallery.Models;

namespace WebGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly EFDataContext _context;

        public CarsController(EFDataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("search")]
        public IActionResult SearchCars()
        {
            var list = _context.Cars.ToList();
            return Ok(list);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCar([FromBody]CarAddViewModel car)
        {
            //_context.Cars.Add(car);
            //_context.SaveChanges();
            var dir = Directory.GetCurrentDirectory();
            var dirSave = Path.Combine(dir, "uploads");
            var imageName = Path.GetRandomFileName() + ".jpg";
            var imageSaveFolder = Path.Combine(dirSave, imageName);
            var image = car.Image.LoadBase64();
            image.Save(imageSaveFolder);
            return Ok(new { message="Додано" });
        }

    }
}

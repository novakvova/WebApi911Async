using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGallery.Models
{
    public class CarAddViewModel
    {
        [Required(ErrorMessage ="Вказіть марку автомобіля")]
        public string Mark { get; set; }
        /// <summary>
        /// модель
        /// </summary>
        [Required(ErrorMessage = "Вказіть модель автомобіля")]
        public string Model { get; set; }
        /// <summary>
        /// Рік випуску
        /// </summary>
        [Range(1950, 2021, ErrorMessage = "Не допустиме значення для року автомобіля")]
        public int Year { get; set; }
        /// <summary>
        /// Пальне
        /// </summary>
        [Required(ErrorMessage = "Вказіть тип пального")]
        public string Fuel { get; set; }
        /// <summary>
        /// Об'єм
        /// </summary>
        [Required(ErrorMessage = "Вказіть об'єм двигун")]
        public float Сapacity { get; set; }
        public string Image { get; set; }
    }
}

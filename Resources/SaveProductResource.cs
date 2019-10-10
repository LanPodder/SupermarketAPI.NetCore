using System.ComponentModel.DataAnnotations;
using Supermarket.API.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Supermarket.API.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        public short QuantityInPackage {get; set;}
        
        [Required]
        public string UnitOfMeasurement {get; set;}
        
        [Required]
        public int CategoryId {get; set;}
    }
}
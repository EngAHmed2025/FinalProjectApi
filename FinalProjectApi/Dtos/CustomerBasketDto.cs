using FinalProject.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectApi.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; }
    }
}

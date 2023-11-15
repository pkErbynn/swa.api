using System;
using System.ComponentModel.DataAnnotations;

namespace SWA.API.Dtos
{
	public class ItemAddDto
	{
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}


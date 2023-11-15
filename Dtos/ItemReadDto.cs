using System;
using System.ComponentModel.DataAnnotations;

namespace SWA.API.Dtos
{
	public class ItemReadDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int RowNumber { get; set; }
        public int FactorialResult { get; set; }
    }
}


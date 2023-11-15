using System;
using System.ComponentModel.DataAnnotations;

namespace SWA.API.Properties
{
	public class Item
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

        public int RowNumber { get; set; }
        public long FactorialResult { get; set; }
    }
}


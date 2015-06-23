using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EstudioDelFutbol.Models
{
	public class LoginModel
	{
		[Required]
		[Display(Name = "UserName")]
        public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

	}
}
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class SignUpViewModel
	{
       
		[Required(ErrorMessage = "Username is Required")]
		public string Username { get; set; }
		[Required(ErrorMessage = "First Name is Required")]

		public string Fname { get; set; }
		[Required(ErrorMessage = "Last Name is Required")]

		public string Lname { get; set; }


		[Required(ErrorMessage ="Email is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5,ErrorMessage ="min password 5")]
		[DataType(DataType.Password)]	
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword is Required")]
		[Compare(nameof(Password),ErrorMessage = "ConfirmPassword does not password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		public bool isAgree { get; set; }	
    }
}

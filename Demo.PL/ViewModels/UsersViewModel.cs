using System;
using System.Collections.Generic;
using Demo.PL.ViewModels;
namespace Demo.PL.ViewModels
{
	public class UsersViewModel
	{
		public string Id { get; set; }	

		public string Fname { get; set; }
		public string Lname { get; set; }

		public string Email { get; set; }	
		public string Phonenumber { get; set; }	
		public IEnumerable<string> Roles { get; set; }	

		public UsersViewModel()
		{
			Id= Guid.NewGuid().ToString();
		}
	}
}

﻿
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name ="Role Name")]
        public string RoleName { get; set; } // => name of role 

        public RoleViewModel()
        {
            Id = Guid.NewGuid().ToString();               
        }
    }
}
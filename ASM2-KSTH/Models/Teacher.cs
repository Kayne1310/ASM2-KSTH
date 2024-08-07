﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models;

public partial class Teacher
{
    public int TeacherId { get; set; } // Primary Key

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Display(Name = "Address")]
    [Required(ErrorMessage = "Address is required")]
    [MaxLength(30, ErrorMessage = "Maximum 30 characters")]
    public string? Address { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Incorrect email format")]
    public string? Email { get; set; }

    [Display(Name = "Phone number")]
    [Required(ErrorMessage = "Phone number is required")]
    [MaxLength(10, ErrorMessage = "Maximum 10 characters")]
    [RegularExpression(@"0[982]\d{8}", ErrorMessage = "+84 format is not correct")]
    public string? PhoneNumber { get; set; }
    public string? RandomKey { get; set; }

    [Display(Name = "Username")]
    [Required(ErrorMessage = "Username is required")]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters")]
    public string? Username { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [ForeignKey("Roles")]
    public int RoleId { get; set; }
    public virtual Roles? Roles { get; set; }

    public virtual ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    
}

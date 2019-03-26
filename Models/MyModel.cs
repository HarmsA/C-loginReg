using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginReg.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [DisplayName("First Name")]
        [MaxLength(50, ErrorMessage="Too many letters in your name")]
        [MinLength(2, ErrorMessage="Name is too short")]
        public string Fname {get;set;}

        [DisplayName("Last Name")]
        [Required]
        [MaxLength(50, ErrorMessage="Too many letters in your name")]
        [MinLength(2, ErrorMessage="Name is too short")]
        public string Lname {get;set;}

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Needs 8 characters in your password")]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }

    public class LoginUser
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [Required]
        public string Password {get;set;}
    }
}
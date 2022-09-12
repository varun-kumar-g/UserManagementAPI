using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string DateOfBirth { get; set; }
    }
}

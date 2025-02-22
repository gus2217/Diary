﻿using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Models
{
    public class DiaryEntry
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter a Title")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Title must be between 3 and 100 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        public string? UserId { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}

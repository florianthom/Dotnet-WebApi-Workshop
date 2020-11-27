using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace homepageBackend.Domain
{
    public class Tag
    {
        [Key] public string Name { get; set; }

        public string CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))] public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
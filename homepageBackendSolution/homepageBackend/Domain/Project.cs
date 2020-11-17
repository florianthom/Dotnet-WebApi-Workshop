using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace homepageBackend.Domain
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        
        
    }
}
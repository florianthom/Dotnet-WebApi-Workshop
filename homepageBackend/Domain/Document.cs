using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homepageBackend.Domain
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Topic { get; set; }

        public string Link { get; set; }

        public virtual List<DocumentTag> Tags { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }


        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdaterId { get; set; }
    }
}
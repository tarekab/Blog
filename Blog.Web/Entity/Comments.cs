namespace Blog.Web.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        public int ID { get; set; }

        public int PostID { get; set; }

        public DateTime DateT { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        public string Body { get; set; }

        public virtual Posts Posts { get; set; }
    }
}

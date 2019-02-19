using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchStore.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Brand Name")]
        public string Name { get; set; }
    }
}
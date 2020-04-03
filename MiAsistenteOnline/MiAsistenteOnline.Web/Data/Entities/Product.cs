using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiAsistenteOnline.Web.Data.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        public Decimal Price { get; set; }

        [Display(Name = "Image")]
        public String ImageUrl  { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase  { get; set; }

        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }

        [Display(Name = "Is Availabe?")]
        public bool IsAvailabe  { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public User User { get; set; }


    }
}

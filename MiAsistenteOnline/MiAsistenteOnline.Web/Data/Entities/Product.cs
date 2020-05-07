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
        [Display(Name ="Producto")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString ="{0:N2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Precio")]
        public Decimal Price { get; set; }

        [Display(Name = "Imagen")]
        public String ImageUrl  { get; set; }

        [Display(Name = "Ultima compra")]
        public DateTime? LastPurchase  { get; set; }

        [Display(Name = "Ultima venta")]
        public DateTime? LastSale { get; set; }

        [Display(Name = "Esta Disponible?")]
        public bool IsAvailabe  { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public User User { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageUrl))
                {
                    return null;
                }

                return $"https://miasistenteonlineweb.azurewebsites.net{this.ImageUrl.Substring(1)}";
            }
        }

        public virtual ICollection<ProductPresentacion> ProductPresentacions { get; set; }




    }
}

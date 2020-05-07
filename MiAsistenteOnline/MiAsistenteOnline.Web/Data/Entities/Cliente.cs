using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiAsistenteOnline.Web.Data.Entities
{
    public class Cliente : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombres { get; set; }

        [Display(Name ="Documento de identidad")]
        public string DNI { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string Facebook { get; set; }

        public virtual ICollection<Zona> Zona { get; set; }

    }
}

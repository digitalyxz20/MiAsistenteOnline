using MiAsistenteOnline.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace MiAsistenteOnline.Web.Models
{
    public class ProductViewModel :Product
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}

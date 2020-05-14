using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace MiAsistenteOnline.Common.Models
{
    public partial class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("imageUrl")]
        public object ImageUrl { get; set; }

        [JsonProperty("lastPurchase")]
        public object LastPurchase { get; set; }

        [JsonProperty("lastSale")]
        public object LastSale { get; set; }

        [JsonProperty("isAvailabe")]
        public bool IsAvailabe { get; set; }

        [JsonProperty("grupoArticulo")]
        public string GrupoArticulo { get; set; }

        [JsonProperty("stock")]
        public long Stock { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("imageFullPath")]
        public object ImageFullPath { get; set; }

        [JsonProperty("productPresentacions")]
        public object ProductPresentacions { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Price:C2}";
        }


    }
}

using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Painting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ImagePainting> Images { get; set; }
        
        public Painting()
        {
            Images = new List<ImagePainting>();
        }
    }
}
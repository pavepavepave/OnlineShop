using System;

namespace OnlineShop.Db.Models
{
    public sealed class ImagePainting
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid PaintingId { get; set; }
        public Painting Painting { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class AddPaintingVM
    {
        [Required(ErrorMessage = "Не указано название товара")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Название должно содержать от 5 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано описание товара")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Описание должно содержать от 10 до 250 символов")]
        public string Description { get; set; }
        
        public IFormFile[] UploadedFiles { get; set; }
    }
}
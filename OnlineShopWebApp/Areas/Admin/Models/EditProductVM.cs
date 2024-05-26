using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class EditProductVM
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Не указано название товара")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Название должно содержать от 5 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        [Range(10, 100000, ErrorMessage = "Цена должна быть от 10 до 100'000 рублей.")]
        [RegularExpression(@"^\d+(\.\d{0,2})?$", ErrorMessage = "Цена должна быть из цифр по образцу: 25000.5")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Не указано описание товара")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Описание должно содержать от 10 до 250 символов")]
        public string Description { get; set; }
        
        public List<string> ImagePath { get; set; }
        
        public IFormFile[] UploadedFiles { get; set; }
    }
}
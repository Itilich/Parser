﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Parser.Data
{
    public class PriceLog
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } // Добавляем название товара
        public string DateTime { get; set; }
        public double PriceDomotex { get; set; }
        public double PriceVodoparad { get; set; }
        public string LinkDomotex { get; set; }
        public string LinkVodoparad { get; set; }
    }
}

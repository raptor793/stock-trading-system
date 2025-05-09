﻿namespace OrderService.Models
{
    public class OrderRequest
    {
        public string Ticker { get; set; } = null!;
        public int Quantity { get; set; }
        public string Side { get; set; } = null!;
    }
}

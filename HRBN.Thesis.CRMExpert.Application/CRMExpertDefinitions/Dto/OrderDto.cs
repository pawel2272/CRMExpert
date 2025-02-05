﻿using System;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public Guid ContactId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }
    }
}
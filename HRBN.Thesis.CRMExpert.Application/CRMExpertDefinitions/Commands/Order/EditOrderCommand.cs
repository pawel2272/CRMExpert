using System;
using HRBN.Thesis.CRMExpert.Application.Core.Command;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Commands.Order
{
    public class EditOrderCommand : BaseCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public Guid ContactId { get; set; }
        public Guid ProductId { get; set; }
    }
}

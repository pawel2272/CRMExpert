using System;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }
    }
}
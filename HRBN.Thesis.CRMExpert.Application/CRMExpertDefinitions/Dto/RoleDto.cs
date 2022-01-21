using System;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreDate { get; set; }
        public DateTime ModDate { get; set; }
    }
}

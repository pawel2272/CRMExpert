using System;

namespace HRBN.Thesis.CRMExpert.Application.CRMExpertDefinitions.Dto;

public class PermissionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime CreDate { get; set; }
    public DateTime ModDate { get; set; }
}
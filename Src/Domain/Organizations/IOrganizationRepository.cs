namespace Domain.Organizations;

public interface IOrganizationRepository
{
	IEnumerable<Organization> GetAll();

	Task<Organization> GetById(OrganizationId id);

	Task Add(Organization organization);

	Task Update(Organization organization);

	Task Delete(Organization organization);
}

using DTO_Pagination_Filtering_Mapping;
using DTOWithSRM.DTOs;
using DTOWithSRM.Entities;
using DTOWithSRM.Filters;

namespace DTOWithSRM.Infrastucture.Services;

public interface IPersonService
{
    PaginationResponses<IEnumerable<PersonReadInfo>> GetPersons(PersonFilter filter);
    PersonReadInfo GetPersonById(int id);
    bool CreatePerson(PersonCreateInfo person);
    bool UpdatePerson(PersonUpdateInfo person);
    bool DeletePerson(int id);
}
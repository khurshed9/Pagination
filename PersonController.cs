using DTO_Pagination_Filtering_Mapping;
using DTOWithSRM.DTOs;
using DTOWithSRM.Filters;
using DTOWithSRM.Infrastucture.Services;
using Microsoft.AspNetCore.Mvc;

namespace DTOWithSRM;

[ApiController]
[Route("api/people")]

public sealed class PersonController(IPersonService personService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetPersons([FromQuery] PersonFilter filter)
        => Ok(ApiResponse<PaginationResponses<IEnumerable<PersonReadInfo>>>.Success(null, personService.GetPersons(filter)));
    
    [HttpGet("{id:int}")]
    public IActionResult GetPersonById(int id)
    {
        PersonReadInfo?  res = personService.GetPersonById(id);
        return res != null
            ? Ok(ApiResponse<PersonReadInfo?>.Success(null, res))
            : NotFound(ApiResponse<PersonReadInfo?>.Fail(null, null));
    }
    
    [HttpPost]
    public IActionResult CreatePerson(string firstName, string lastName, int age, string email, string phone)
    {
        PersonCreateInfo person = new PersonCreateInfo(firstName, lastName, age,email,phone);
        bool res = personService.CreatePerson(person);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }
    
    [HttpPut]
    public IActionResult UpdatePerson(PersonUpdateInfo updateInfo)
    {
        bool res = personService.UpdatePerson(updateInfo);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletePerson(int id)
    {
        bool res = personService.DeletePerson(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}
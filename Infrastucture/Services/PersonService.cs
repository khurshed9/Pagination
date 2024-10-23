using DTO_Pagination_Filtering_Mapping;
using DTOWithSRM.DTOs;
using DTOWithSRM.Entities;
using DTOWithSRM.Filters;

namespace DTOWithSRM.Infrastucture.Services;

public class PersonService(PersonDbContext personDbContext) : IPersonService
{
    
    public PaginationResponses<IEnumerable<PersonReadInfo>> GetPersons(PersonFilter filter)
    {
        IQueryable<Person> persons = personDbContext.Persons;
        if (filter.Age > 0)
            persons = persons.Where(x => x.Age == filter.Age);
        if (filter.Email != null)
            persons = persons.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

        IQueryable<PersonReadInfo> personsInfo = persons.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
            .Select(x => new PersonReadInfo
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            });

        int totalRecords = personDbContext.Persons.Count();
        return PaginationResponses<IEnumerable<PersonReadInfo>>.Create(filter.PageNumber, filter.PageSize, totalRecords, personsInfo);
    }

    public PersonReadInfo GetPersonById(int id)
    {
        Person? res = personDbContext.Persons.FirstOrDefault(x =>
            x.IsDeleted == false && x.Id == id);

        return res != null
            ? new PersonReadInfo(res.Id, res.FirstName, res.LastName, res.Age, res.Email, res.PhoneNumber)
            : new PersonReadInfo();
    }

    public bool CreatePerson(PersonCreateInfo person)
    {
        bool existing = personDbContext.Persons.Any(x =>
            x.Email.ToLower() == person.Email.ToLower() && x.IsDeleted == false);
        if (existing) return false;
        
        int maxId = personDbContext.Persons.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
        maxId++;
        personDbContext.Persons.Add(new()
        {
            Id = maxId,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Age = person.Age,
            Email = person.Email,
            PhoneNumber = person.PhoneNumber,
        });
        return personDbContext.SaveChanges() > 0;
    }

    public bool UpdatePerson(PersonUpdateInfo person)
    {
        Person? existingPerson = personDbContext.Persons.FirstOrDefault(x => x.IsDeleted == false && x.Id == person.Id);
        if (existingPerson == null) return false;
        
        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.Age = person.Age;
        existingPerson.Email = person.Email;
        existingPerson.PhoneNumber = person.PhoneNumber;
        existingPerson.Version += 1;
        existingPerson.UpdatedAt = DateTime.UtcNow;
        
        return personDbContext.SaveChanges() > 0;
    }

    public bool DeletePerson(int id)
    {
        Person? existingPerson = personDbContext.Persons.FirstOrDefault(x => x.IsDeleted == false && x.Id == id);
        if (existingPerson == null) return false;
        
        existingPerson.IsDeleted = true;
        existingPerson.DeletedAt = DateTime.UtcNow;
        existingPerson.Version += 1;
        existingPerson.UpdatedAt = DateTime.UtcNow;
        
        return personDbContext.SaveChanges() > 0;
    }
}
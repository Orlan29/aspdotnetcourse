using UnivAssurance.DataAccess.Models;
using UnivAssurance.DataAccess.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BusinessLogic.Services;

public class PersonService
{
    private UnivAssuranceDBContext Context;
    public PersonService(UnivAssuranceDBContext context)
    {
        Context = context;
    }

    public List<Person> FindAllPersons()
    {
        return Context.Person.ToList();
    }

    public Person? FindOnePersonById(int personId)
    {
        Person? person = Context.Person.Where(person => person.PersonID == personId).FirstOrDefault();

        return person;
    }

    public Boolean DeletOnePersonById(int personId)
    {
        var person = FindOnePersonById(personId);

        if (person == null)
        {
            throw new Exception("Cette personne n'est pas disponible");
        }

        Person DeletedPerson = Context.Remove<Person>(person).Entity;
        
        if (DeletedPerson != null)
        {
            Context.SaveChanges();
        }

        return true;
    }

    public Person UpdateOnePersonById(Person person)
    {
        Person PersonFinded = Context.Update<Person>(person).Entity;
        Context.SaveChanges();

        return PersonFinded; 
    }

    public Person CreateOnePerson(Person person)
    {
        Person Person = Context.Add<Person>(person).Entity;
        Context.SaveChanges();

        return Person;
    }
}
using BusinessLogic.Services;
using UnivAssurance.DataAccess.Data;

namespace UnivAssurance.Business.Tests;

public class PersonTest
{
    private PersonService Service;
    private UnivAssuranceDBContext DBContext;

    [SetUp]
    public void Setup()
    {
        this.DBContext = new UnivAssuranceDBContext();
        this.Service = new PersonService(this.DBContext);
    }

    [Test]
    public void Test1()
    {
        var person = this.Service.FindAllPersons();

        Assert.IsNotNull(person);
    }
}
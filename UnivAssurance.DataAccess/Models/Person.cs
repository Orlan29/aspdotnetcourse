namespace UnivAssurance.DataAccess.Models;
public class Person
{
    public int PersonID { get; set; } 
    public string TypePart { get; set; } = string.Empty;
    public string NumberTypePart  { get; set; } = string.Empty;
    public string Name  { get; set; } = string.Empty;
    public string FirstName { get; set; }   = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string MaritalStatus { get; set; } = string.Empty;
    public int NumberChildren { get; set; }
    public string Employer { get; set; } = string.Empty;

    public virtual ICollection<Subscription>? Subscription {get; set;}
}
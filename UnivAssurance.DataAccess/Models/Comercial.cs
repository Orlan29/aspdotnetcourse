namespace UnivAssurance.DataAccess.Models;

public class Comercial
{
    public  int ComercialID { get; set; }
    public string Name { get; set; } = String.Empty;
    public string SerialNumber { get; set; } = String.Empty;

    public virtual ICollection<Subscription> Subscription {get; set;}
}
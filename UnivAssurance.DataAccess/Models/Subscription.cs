namespace UnivAssurance.DataAccess.Models;

public class Subscription
{
    public int PersonID {get; set;} 
    public int ProductID {get; set;}
    public int ComercialID {get; set;}
    public int SubscriptionID {get; set;}
    public DateTime SubscriptionDate {get; set;}
    public uint SubscriptionState {get; set;}

    public virtual Person Person {get; set;}
    public virtual Product Product {get; set;}
    public virtual Comercial Comercial {get; set;}
}
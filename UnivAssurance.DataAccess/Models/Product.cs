namespace UnivAssurance.DataAccess.Models;

public class Product
{
    public int ProductID { get; set; }

    public string ProductName { get; set; } = String.Empty;

    public int Price { get; set; }

    public DateTime CampaignStartDate { get; set; }

    public DateTime CampaignEndDate { get; set; }

    public virtual ICollection<Subscription> Subscription {get; set;}
}
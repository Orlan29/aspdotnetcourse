using UnivAssurance.DataAccess.Models;
using UnivAssurance.DataAccess.Data;

namespace BusinessLogic.Services;

public class SubscriptionService
{
    private UnivAssuranceDBContext Context;
    public SubscriptionService(UnivAssuranceDBContext context)
    {
        Context = context;
    }

    public List<Subscription> FindAllSubscriptions()
    {
        return Context.Subscription.ToList();
    }

    public Subscription? FindOneSubscriptionById(int subscriptionId)
    {
        if (subscriptionId < 1)
            throw new ArgumentException("L'identifiant ne peut etre null");
        
        Subscription? subscription = Context.Subscription.Where(subscription => subscription.SubscriptionID == subscriptionId).FirstOrDefault();

        return subscription;
    }

    public Boolean DeletOnesubScriptionById(int subscriptionId)
    {
        var subscription = FindOneSubscriptionById(subscriptionId);

        if (subscription == null)
        {
            throw new Exception("Cette souscription ne n'est pas disponible");
        }

        Subscription deletedSubscription = Context.Remove<Subscription>(subscription).Entity;
        
        if (deletedSubscription != null)
        {
            Context.SaveChanges();
        }

        return true;
    }

    public Subscription UpdateOneSubscriptionById(Subscription subscription)
    {
        Subscription subscriptionFinded = Context.Update<Subscription>(subscription).Entity;
        Context.SaveChanges();

        return subscriptionFinded; 
    }

    public Subscription CreateOneSubscription(Subscription subscription)
    {
        Subscription createdSubscription = Context.Add<Subscription>(subscription).Entity;
        Context.SaveChanges();

        return createdSubscription;
    }
}
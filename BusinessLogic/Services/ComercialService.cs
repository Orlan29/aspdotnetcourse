using UnivAssurance.DataAccess.Models;
using UnivAssurance.DataAccess.Data;

namespace BusinessLogic.Services;

public class ComercialService
{
    private UnivAssuranceDBContext Context;
    public ComercialService(UnivAssuranceDBContext context)
    {
        Context = context;
    }

    public List<Comercial> FindAllComercials()
    {
        return Context.Comercial.ToList();
    }

    public Comercial? FindOneComercialById(int comercialId)
    {
        if (comercialId < 1)
            throw new ArgumentException("L'identifiant ne peut etre null");
        
        Comercial? comercial = Context.Comercial.Where(comercial => comercial.ComercialID == comercialId).FirstOrDefault();

        return comercial;
    }

    public Boolean DeletOneComercialById(int comercialId)
    {
        var comercial = FindOneComercialById(comercialId);

        if (comercial == null)
        {
            throw new Exception("Cet produit ne n'est pas disponible");
        }

        Comercial deletedComercial = Context.Remove<Comercial>(comercial).Entity;
        
        if (deletedComercial != null)
        {
            Context.SaveChanges();
        }

        return true;
    }

    public Comercial UpdateOneComercialById(Comercial comercial)
    {
        Comercial comercialFinded = Context.Update<Comercial>(comercial).Entity;
        Context.SaveChanges();

        return comercialFinded; 
    }

    public Comercial CreateOneComercial(Comercial comercial)
    {
        Comercial createdComercial = Context.Add<Comercial>(comercial).Entity;
        Context.SaveChanges();

        return createdComercial;
    }
}
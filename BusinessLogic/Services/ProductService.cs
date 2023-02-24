using UnivAssurance.DataAccess.Models;
using UnivAssurance.DataAccess.Data;

namespace BusinessLogic.Services;

public class ProductService
{
    private UnivAssuranceDBContext Context;
    public ProductService(UnivAssuranceDBContext context)
    {
        Context = context;
    }

    public List<Product> FindAllProducts()
    {
        return Context.Product.ToList();
    }

    public Product? FindOneProductById(int productId)
    {
        if (productId < 1)
            throw new ArgumentException("L'identifiant ne peut etre null");
        
        Product? product = Context.Product.Where(product => product.ProductID == productId).FirstOrDefault();

        return product;
    }

    public Boolean DeletOneProductById(int productId)
    {
        var product = FindOneProductById(productId);

        if (product == null)
        {
            throw new Exception("Cet produit ne n'est pas disponible");
        }

        Product deletedProduct = Context.Remove<Product>(product).Entity;
        
        if (deletedProduct != null)
        {
            Context.SaveChanges();
        }

        return true;
    }

    public Product UpdateOneProductById(Product product)
    {
        Product productFinded = Context.Update<Product>(product).Entity;
        Context.SaveChanges();

        return productFinded; 
    }

    public Product CreateOneProduct(Product product)
    {
        Product Product = Context.Add<Product>(product).Entity;
        Context.SaveChanges();

        return Product;
    }
}
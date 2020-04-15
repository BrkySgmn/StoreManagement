using StoreManagement.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAL
{
    static class HelperProduct
    {
        public static bool CRUDProduct(Product product, EntityState state)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                if (state == EntityState.Deleted)
                {
                    product.RegistrationStatus = (byte)RegistrationStatus.Passive;
                }
                else
                {
                    store.Entry(product).State = state;
                    product.RegistrationStatus = (byte)RegistrationStatus.Active;
                }
                if (store.SaveChanges() > 0)
                    return true;
                return false;
            }
        }
        public static Product GetProduct(int productID)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Product.Where(s => s.ID == productID &&
                (s.RegistrationStatus == (byte)RegistrationStatus.Active)).FirstOrDefault();
            }
        }
        public static Product GetProduct(string productName)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Product.Where(s => s.Name == productName &&
                (s.RegistrationStatus == (byte)RegistrationStatus.Active)).FirstOrDefault();
            }
        }
        public static List<Product> GetProducts()
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Product.ToList();
            }
        }
        public static List<Product> GetProductsFromCategory(int categoryID)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Product.Where(s => s.CategoryID == categoryID &&
                s.RegistrationStatus == (byte)RegistrationStatus.Active).ToList();
            }
        }
        public static bool DecreaseFromStock(int productID, int numberOfProducts)
        {
            using(StoreManagementEntities store = new StoreManagementEntities())
            {
                var product = store.Product.Where(s => s.ID == productID).First();
                product.Stock -= numberOfProducts;
                if (store.SaveChanges() > 0)
                    return true;
                return false;
            }
        }
    }
}

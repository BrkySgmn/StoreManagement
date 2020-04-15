using StoreManagement.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAL
{
    static class HelperCategory
    {
        public static bool CRUDCategory(Category category, EntityState state)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                if (state == EntityState.Deleted)
                {
                    category.RegistrationStatus = (byte)RegistrationStatus.Passive;
                    store.Entry(category).State = EntityState.Modified;
                }
                else
                {
                    category.RegistrationStatus = (byte)RegistrationStatus.Active;
                    store.Entry(category).State = state;
                }
                if (store.SaveChanges() > 0)
                    return true;
                return false;
            }
        }
        public static List<Category> GetGategories()
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Category.Where(s => s.RegistrationStatus == (byte)RegistrationStatus.Active).ToList();
            }
        }
        public static Category GetCategory(int categoryID)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Category.Where(s => s.ID == categoryID
                && s.RegistrationStatus == (byte)RegistrationStatus.Active).FirstOrDefault();
            }
        }
        public static Category GetCategory(string categoryName)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Category.Where(s => s.Name == categoryName && s.RegistrationStatus == (byte)RegistrationStatus.Active).FirstOrDefault();
            }
        }
        public static Category GetCategoryFromProduct(Product product)
        {
            using(StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Category.Where(s => s.ID == product.CategoryID && s.RegistrationStatus == (byte)RegistrationStatus.Active).FirstOrDefault();
            }
        }
    }
}

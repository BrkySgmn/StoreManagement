using StoreManagement.BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAL
{
    static class HelperCustomer
    {
        public static bool CRUDCustomer(Customer customer, EntityState state)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                if (state == EntityState.Deleted)
                {
                    customer.RegistrationStatus = (byte)RegistrationStatus.Passive;
                    store.Entry(customer).State = EntityState.Modified;
                }
                else
                {
                    store.Entry(customer).State = state;
                    customer.RegistrationStatus = (byte)RegistrationStatus.Active;
                }
                if (store.SaveChanges() > 0)
                    return true;
                return false;
            }
        }
        public static Customer GetCustomer(int customerID)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Customer.Where(a => a.ID == customerID).FirstOrDefault();
            }
        }
        public static Customer GetCustomer(string customerName)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Customer.Where(s => s.Name == customerName).FirstOrDefault();
            }
        }
        public static List<Customer> GetCustomers()
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Customer.Where(s => s.RegistrationStatus == (byte)RegistrationStatus.Active).ToList();
            }
        }
    }
}

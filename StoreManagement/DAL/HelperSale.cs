using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAL
{
    class HelperSale
    {
        public static bool CRUDSale(Sale sale, EntityState state)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                store.Entry(sale).State = state;
                if (store.SaveChanges() > 0)
                    return true;
                return false;
            }
        }
        public static List<Sale> GetSales()
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Sale.ToList();
            }
        }
        public static List<Sale> GetSales(DateTime dateOfSale)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Sale.Where(s => s.DateOfSale == dateOfSale).ToList();
            }
        }
        public static List<Sale> GetSalesFromCustomer(int customerID)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Sale.Where(s => s.CustomerID == customerID).ToList();
            }
        }
        public static List<Sale> GetSalesFromProduct(int productID)
        {
            using(StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Sale.Where(s => s.ProductID == productID).ToList();
            }
        }
        public static List<Sale> GetSalesBetweenSelectedDates(DateTime startDate, DateTime endDate)
        {
            using (StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.Sale.Where(s => s.DateOfSale >= startDate && s.DateOfSale <= endDate).ToList();
            }
        }
    }
}

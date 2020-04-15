using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAL
{
    static class HelperUser
    {
        public static User GetUser(int userID)
        {
            using(StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.User.Where(s => s.ID == userID).FirstOrDefault();
            }
        }
        public static List<User> GetUsers()
        {
            using(StoreManagementEntities store = new StoreManagementEntities())
            {
                return store.User.ToList();
            }
        }
        public static bool ChangeUserPassword(User user)
        {
            using(StoreManagementEntities store = new StoreManagementEntities())
            {
                store.Entry(user).State = EntityState.Modified;
                if (store.SaveChanges() > 0)
                    return true;
                return false;
            }
        }
    }
}

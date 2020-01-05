using HRManagementDataAccessLayer;
using HRManagementEntities;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class UserManager
    {
        Repository<Users> repo = new Repository<Users>();
        public List<Users> GetUsers()
        {
            return repo.List();
        }
        public Users GetUserById(int id)
        {
            return repo.Find(x => x.Id == id);
        }
        public void Update(Users user)
        {
            repo.Update(user);
        }
        public void Save()
        {
            repo.Save();
        }
        public void Delete(int id)
        {
            Users user = repo.Find(x => x.Id == id);
            repo.Delete(user);
        }
    }
}

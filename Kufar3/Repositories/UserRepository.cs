using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class UserRepository : BaseRepository
    {
        public List<User> List()
        {
            return Context.Users.ToList();
        }

        public User GetById(int id)
        {
            return Context.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Add(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void Update(User userModel)
        {
            var user = GetById(userModel.Id);

            if (user == null)
            {
                throw new Exception("Такого юзера не существует");
            }

            user.Email = userModel.Email;
            user.Name = userModel.Name;
            user.MobileNumber = userModel.MobileNumber;
            user.Password = userModel.Password;

            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var user = GetById(id);

            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
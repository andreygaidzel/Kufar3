﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using Kufar3.Controllers;
using Kufar3.Helpers;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class UserRepository : BaseRepository
    {
        public List<User> List()
        {
            return Context.Users.ToList();
        }

        public User GetById(long id)
        {
            return Context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            var res = Context.Users.FirstOrDefault(x => x.Email == email);
            return res;
        }

        public User GetBy(string email, string password)
        {
            return Context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
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

        public void Edit(User model, HttpPostedFileBase file)
        {
            var user = Context.Users.First(x => x.Id == model.Id);
            user.Email = model.Email;
            user.Name = model.Name;
            user.MobileNumber = model.MobileNumber;
            user.Password = model.Password;
            user.Icon = FilesHelper.ConvertImageinByte(file);

            Context.SaveChanges();
        }

        public void EditRole(int id, string role)
        {
            var user = Context.Users.First(x => x.Id == id);
            user.Role = role.StringToEnumRole();
            Context.SaveChanges();
        }

        public string GetAvatar(long id)
        {
            var avatar = Context.Users.First(x => x.Id == id).Icon;

            return FilesHelper.ConvertByteToImage(avatar);
        }
    }
}
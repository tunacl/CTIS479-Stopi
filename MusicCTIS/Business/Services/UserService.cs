using Business.Models;
using Business.Services.Base;
using DataAccess.Contexts;
using DataAccess.Results.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUserService
    {
        IQueryable<UserModel> Query();
        Result Add(UserModel model);
        Result Update(UserModel model);
        Result Delete(int id);

        List<UserModel> GetList() => Query().ToList();

        UserModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);
    }
    public class UserService : ServiceBase, IUserService
    {
        public UserService(Db db) : base(db)
        {
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.OrderByDescending(u => u.Role).ThenBy(u => u.BirthDate).Select(u => new UserModel
            {
                BirthDate = u.BirthDate,
                UserName = u.UserName,
                IsActive = u.IsActive,
                RoleId = u.RoleId,
                Guid = u.Guid,
                Id = u.Id,
                Password = u.Password,

                //extras
                BirthDateOutput = u.BirthDate.HasValue ? u.BirthDate.Value.ToString("MM/dd/yyyy") : string.Empty,
                IsActiveOutput = u.IsActive ? "Yes" : "No",
                RoleOutput = new RoleModel()
                {
                    Guid = u.Role.Guid,
                    Id = u.Role.Id,
                    Name = u.Role.Name
                }

            });
        }

        public Result Add(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

       

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}

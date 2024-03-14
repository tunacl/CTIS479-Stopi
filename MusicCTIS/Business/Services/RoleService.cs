using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IRoleService
    {
        IQueryable<RoleModel> Query();
        Result Add(RoleModel role);
        Result Update(RoleModel model);
        Result Delete(int id);
    }
    public class RoleService : IRoleService
    {
        private readonly Db _db;

        public RoleService(Db db)
        {
            _db = db;
        }

        public Result Add(RoleModel role)
        {
            if (_db.Roles.Any(r => r.Name.ToLower() == role.Name.ToLower().Trim()))
                return new ErrorResult("Role name is already exist!");

            Role newRole = new Role()
            {
                Guid = Guid.NewGuid().ToString(),//guid random atamak için sanırım
                Name = role.Name.Trim(),
            };

            _db.Roles.Add(newRole);
            _db.SaveChanges();
            return new SuccessResult("Role is created!");

        }

        public Result Delete(int id)
        {
            Role entity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (entity == null)
                return new ErrorResult("Role cannot exist");
            else if (entity.Users is not null && entity.Users.Count > 0)
            {
                return new ErrorResult("This role is used for more than one user!");
            }
            else
            {
                _db.Roles.Remove(entity);
                _db.SaveChanges();
                return new SuccessResult("Role deleted successfully.");
            }

        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.Include(r => r.Users).OrderBy(r => r.Name).Select(r => new RoleModel()
            {
                Name=r.Name,
                Guid = r.Guid,
                Id = r.Id,

                UserCount=r.Users.Count,
                UserRoles = string.Join("<br />", r.Users.OrderBy(r => r.UserName).Select(r => r.UserName))
            });
        }

        public Result Update(RoleModel model)
        {
            if (_db.Roles.Any(r => r.Id != model.Id && r.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Role with the same name exists!");

            Role entity = _db.Roles.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Role not found!");

            entity.Name = model.Name.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("Role updated successfully!");

                


        }
    }
}

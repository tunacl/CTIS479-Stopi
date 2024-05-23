using Business.Models;
using Business.Services.Base;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Enums;
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
			return _db.Users.Include(u => u.UserSongs).ThenInclude(us => us.Song).
				OrderByDescending(u => u.Role).ThenBy(u => u.BirthDate).Select(u => new UserModel
				{
					BirthDate = u.BirthDate,
					UserName = u.UserName,
					IsActive = u.IsActive,
					RoleId = u.RoleId,
					Guid = u.Guid,
					Id = u.Id,
					Password = u.Password,

					//extras
					BirthDateOutput = u.BirthDate.HasValue ? u.BirthDate.Value.ToString("dd/MM/yyyy") : string.Empty,
					IsActiveOutput = u.IsActive ? "Yes" : "No",
					RoleOutput = new RoleModel()
					{
						Guid = u.Role.Guid,
						Id = u.Role.Id,
						Name = u.Role.Name
					},

					SongIdsInput = u.UserSongs.Select(us => us.SongId).ToList(),
					SongNamesOutput = string.Join("<br />", u.UserSongs.Select(us => us.Song.Name))

				}) ;
		}

        public Result Add(UserModel model)
        {
			if (_db.Users.Any(u => u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive))
				return new ErrorResult("Active user with the same user name exists!");

			User entity = new User()
			{
				UserName = model.UserName.Trim(),
				Password = model.Password.Trim(),
				BirthDate = model.BirthDate,
				IsActive = model.IsActive,
				RoleId = model.RoleId ?? 0,
				UserSongs = model.SongIdsInput?.Select(songId => new UserSong()
				{
					SongId = songId

				}).ToList()
				
			};
			_db.Users.Add(entity);
			_db.SaveChanges();

			model.Id = entity.Id;

			return new SuccessResult("User added successfully.");
		}

        public Result Delete(int id)
        {
            User entity = _db.Users.Include(u => u.UserSongs).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return new ErrorResult("User not found!");

            _db.UserSongs.RemoveRange(entity.UserSongs);
            _db.Users.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("User deleted successfully.");
        }

       

        public Result Update(UserModel model)
        {
			if (_db.Users.Any(u => u.Id != model.Id && u.UserName.ToUpper() == model.UserName.ToUpper().Trim() && u.IsActive))
				return new ErrorResult("Active user with the same user name exists!");
            var entity = _db.Users.Include(u => u.UserSongs).SingleOrDefault(u => u.Id == model.Id);


            
			if (entity is null)
				return new ErrorResult("User not found!");

            _db.UserSongs.RemoveRange(entity.UserSongs);

            entity.IsActive = model.IsActive;
			entity.Password = model.Password.Trim();
			entity.RoleId = model.RoleId.Value;
			entity.BirthDate = model.BirthDate;
			entity.UserName = model.UserName.Trim();

			entity.UserSongs = model.SongIdsInput?.Select(songId => new UserSong()
			{
				SongId = songId

			}).ToList();



            _db.Users.Update(entity);
			_db.SaveChanges();

			return new SuccessResult("User updated successfully.");
		}
    }
}

using Business.Models;
using Business.Services.Base;
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
    public interface ISongService
    {
        IQueryable<SongModel> Query();
        Result Add(SongModel model);
        Result Update(SongModel model);
        Result Delete(int id);


        

    }
    public class SongService : ServiceBase, ISongService
    {
        public SongService(Db db) : base(db)
        {
        }

        public Result Add(SongModel model)
        {
            if (_db.Songs.Any(s => s.Name.ToLower() == model.Name.ToLower().Trim()))
            {
                return new ErrorResult("This song is already saved to the system!");
            }

            Song newSong = new Song()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim(),
                ArtistId = model.ArtistId,
            };
            _db.Songs.Add(newSong);
            _db.SaveChanges();

            model.Id = newSong.Id;
            return new SuccessResult("Song is saved!");
        }

        public Result Delete(int id)
        {
            Song entity = _db.Songs.SingleOrDefault(s => s.Id == id);
            if (entity == null)
            {
                return new ErrorResult("Song information does not exist");
                
            }
            else
            {
                _db.Songs.Remove(entity);
                _db.SaveChanges();
                return new SuccessResult("Song information deleted successfully.");

            }

        }

        public IQueryable<SongModel> Query()
        {
            return _db.Songs.OrderBy(s => s.Name).Select(s => new SongModel()
            {
                Name=s.Name,
                Guid = s.Guid,
                Id = s.Id,

                ArtistChoice = new ArtistModel()
                {
                    Guid= s.Artist.Guid,
                    Id = s.Artist.Id,
                    Name = s.Artist.Name
                }
            });
        }

        public Result Update(SongModel model)
        {
            if (_db.Songs.Any(s => s.Id != model.Id && s.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Song with the same name exist!");

            Song entity = _db.Songs.SingleOrDefault(s => s.Id == model.Id);
            if (entity == null)
                return new ErrorResult("Song not found!");

            entity.Name = model.Name.Trim();
            entity.ArtistId = model.ArtistId;
            _db.Songs.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("Song updated successfully!");
        }
    }
}

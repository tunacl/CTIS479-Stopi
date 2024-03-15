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
    public interface IArtistService
    {
        IQueryable<ArtistModel> Query();
        Result Add(ArtistModel model);
        Result Update(ArtistModel model);
        Result Delete(int id);
    }
    public class ArtistService : IArtistService
    {
        private readonly Db _db;

        public ArtistService(Db db)
        {
            _db = db;
        }

        public Result Add(ArtistModel model)
        {
            if(_db.Artists.Any(a => a.Name.ToLower() == model.Name.ToLower().Trim())){
                return new ErrorResult("This artist name saved in system!");
            }

            Artist newArtist = new Artist()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim(),
            };
            _db.Artists.Add(newArtist);
            _db.SaveChanges();
            return new SuccessResult("Artist is registered!");
        }

        public Result Delete(int id)
        {
            Artist entity = _db.Artists.Include(a => a.Songs).SingleOrDefault(a => a.Id == id);
            if(entity == null)
            {
                return new ErrorResult("Artist information does not exist");
            }
            else if(entity.Songs is not null && entity.Songs.Count > 0)
            {
                return new ErrorResult("Artist have more than one songs!");

            }
            else
            {
                _db.Artists.Remove(entity);
                _db.SaveChanges();
                return new SuccessResult("Artist information deleted successfully.");

            }

        }

        public IQueryable<ArtistModel> Query()
        {
            return _db.Artists.Include(a => a.Songs).OrderBy(a => a.Name).Select(a => new ArtistModel()
            {
                Name = a.Name,
                Guid = a.Guid,
                Id = a.Id,

                TotalSong=a.Songs.Count,
                Songs = string.Join("<br />", a.Songs.OrderBy(a => a.Name).Select(a => a.Name))

            });
        }

        public Result Update(ArtistModel model)
        {
            if(_db.Artists.Any(a => a.Id != model.Id && a.Name.ToLower() == model.Name.ToLower().Trim()))
             return new ErrorResult("Artist with the same name exist!");

            Artist entity = _db.Artists.Find(model.Id);
            if (entity == null)
                return new ErrorResult("Artist not found!");

            entity.Name = model.Name.Trim();
            _db.Artists.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("Artist updated successfully!");

        }
    }
}

using API.DTO_s;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _dataContext;

        public PhotoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            return await _dataContext.Photos
                .IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<IEnumerable<PhotoForApprovalDto>> GetUnaprovedPhotos()
        {
            return await _dataContext.Photos.IgnoreQueryFilters()
                .Where(p => p.isApproved == false)
                .Select(u => new PhotoForApprovalDto
                {
                    photoId = u.Id,
                    Username = u.AppUser.UserName,
                    Url = u.Url,
                    isApproved = u.isApproved
                }).ToListAsync();
        }

        public void RemovePhoto(Photo photo)
        {
            _dataContext.Photos.Remove(photo);
        }
    }
}

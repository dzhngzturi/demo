using API.DTO_s;
using API.Entities;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
        public Task<IEnumerable<PhotoForApprovalDto>> GetUnaprovedPhotos();
        public Task<Photo> GetPhotoById(int id);
        void RemovePhoto(Photo photo);
    }
}

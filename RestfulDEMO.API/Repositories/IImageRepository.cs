using RestfulDEMO.API.Models.Domain;
using System.Net;

namespace RestfulDEMO.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

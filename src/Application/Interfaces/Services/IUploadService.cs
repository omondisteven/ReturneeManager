using ReturneeManager.Application.Requests;

namespace ReturneeManager.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}
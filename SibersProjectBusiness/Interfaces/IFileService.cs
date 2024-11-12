using Microsoft.AspNetCore.Http;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<ProjectDocumentEntity>> UploadFilesAsync(long projectId, IFormFileCollection files);
    }
}

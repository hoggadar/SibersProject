using AutoMapper;
using Microsoft.AspNetCore.Http;
using SibersProjectBusiness.Interfaces;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Services
{
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly string _uploadDirectory = Path.Combine("uploads", "documents");

        public FileService(IMapper mapper)
        {
            _mapper = mapper;
            Directory.CreateDirectory(_uploadDirectory);
        }

        public async Task<IEnumerable<ProjectDocumentEntity>> UploadFilesAsync(long projectId, IFormFileCollection files)
        {
            var uploadedDocuments = new List<ProjectDocumentEntity>();

            foreach (var file in files)
            {
                if (file.Length == 0 || Path.GetExtension(file.FileName).ToLower() != ".pdf")
                {
                    throw new InvalidOperationException("Invalid file format. Only PDF files are allowed.");
                }
                var documentPath = Path.Combine(_uploadDirectory, Guid.NewGuid() + Path.GetExtension(file.FileName));
                using (var stream = new FileStream(documentPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var document = new ProjectDocumentEntity
                {
                    ProjectId = projectId,
                    FileName = file.FileName,
                    FilePath = documentPath,
                    UploadedAt = DateTime.UtcNow
                };
                uploadedDocuments.Add(document);
            }
            return uploadedDocuments;
        }
    }
}

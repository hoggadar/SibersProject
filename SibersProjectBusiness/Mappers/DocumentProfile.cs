using AutoMapper;
using SibersProjectBusiness.DTOs.Document;
using SibersProjectDataAccess.Entities;

namespace SibersProjectBusiness.Mappers
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<ProjectDocumentEntity, DocumentDto>();
        }
    }
}

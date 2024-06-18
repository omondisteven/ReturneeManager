using AutoMapper;
using ReturneeManager.Application.Features.DocumentTypes.Commands.AddEdit;
using ReturneeManager.Application.Features.DocumentTypes.Queries.GetAll;
using ReturneeManager.Application.Features.DocumentTypes.Queries.GetById;
using ReturneeManager.Domain.Entities.Misc;

namespace ReturneeManager.Application.Mappings
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<AddEditDocumentTypeCommand, DocumentType>().ReverseMap();
            CreateMap<GetDocumentTypeByIdResponse, DocumentType>().ReverseMap();
            CreateMap<GetAllDocumentTypesResponse, DocumentType>().ReverseMap();
        }
    }
}
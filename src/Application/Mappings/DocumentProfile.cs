using AutoMapper;
using ReturneeManager.Application.Features.Documents.Commands.AddEdit;
using ReturneeManager.Application.Features.Documents.Queries.GetById;
using ReturneeManager.Domain.Entities.Misc;

namespace ReturneeManager.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
            CreateMap<GetDocumentByIdResponse, Document>().ReverseMap();
        }
    }
}
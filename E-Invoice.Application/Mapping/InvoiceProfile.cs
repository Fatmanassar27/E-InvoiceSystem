using AutoMapper;
using E_Invoice.Application.DTOs;
using E_Invoice.Application.DTOs.Identity;
using E_Invoice.Application.DTOs.PartyDtos;
using E_Invoice.Application.DTOs.SubmissionDtos;
using E_Invoice.Domain.Entities;
using E_Invoice.Domain.Entities.identity;
using InvoiceLineDto = E_Invoice.Application.DTOs.InvoiceLineDto;

namespace E_Invoice.Application.Mapping
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            
            CreateMap<InvoiceDto, Invoice>().ReverseMap();
            CreateMap<IssuerAddress, AddressDto>().ReverseMap();
            CreateMap<ReceiverAddress, AddressDto>().ReverseMap();

            CreateMap<IssuerDto, Issuer>().ReverseMap();
            CreateMap<ReceiverDto, Receiver>().ReverseMap();

           
            CreateMap<DeliveryDto, Delivery>().ReverseMap();

            CreateMap<InvoiceLineDto, InvoiceLine>().ReverseMap();
            CreateMap<UnitValueDto, Value>().ReverseMap();
            CreateMap<DiscountDto, Discount>().ReverseMap();
            CreateMap<TaxableItemDto, TaxableItem>().ReverseMap();
            CreateMap<TaxTotalDto, TaxTotal>().ReverseMap();
            CreateMap<SignatureDto, Signature>().ReverseMap();

            //submission mapping
            CreateMap<AcceptedDocumentDto, AcceptedDocument>().ReverseMap();
            CreateMap<DocumentSubmissionResponseDto, DocumentSubmission>().ReverseMap()
                 .ForMember(dest => dest.RejectedDocuments, opt => opt.Ignore());

            //login and register mapping
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
            CreateMap<LoginDto, ApplicationUser>().ReverseMap();

        }
    }
}

using MediatR;
using CRM.Payment.Application.Features.PaymentTerms.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries
{
    public class GetPaymentTermsByIdQuery : IRequest<PaymentTermsDto>
    {
        public string Id { get; set; }

        public GetPaymentTermsByIdQuery(string id)
        {
            Id = id;
        }
    }
}


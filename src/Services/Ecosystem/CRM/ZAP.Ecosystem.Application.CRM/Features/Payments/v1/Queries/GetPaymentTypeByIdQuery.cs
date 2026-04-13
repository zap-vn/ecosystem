using MediatR;
using CRM.Payment.Application.Features.PaymentTypes.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries
{
    public class GetPaymentTypeByIdQuery : IRequest<PaymentTypeDto>
    {
        public string Id { get; set; }

        public GetPaymentTypeByIdQuery(string id)
        {
            Id = id;
        }
    }
}


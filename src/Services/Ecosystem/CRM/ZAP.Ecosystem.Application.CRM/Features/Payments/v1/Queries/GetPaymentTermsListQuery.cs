using MediatR;
using CRM.Payment.Application.Features.PaymentTerms.DTOs;
using CRM.BuildingBlocks.Models;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries
{
    public class GetPaymentTermsListQuery : IRequest<PagedResult<PaymentTermsDto>>
    {
        public FilterDTOs Filter { get; set; } = new();
    }
}


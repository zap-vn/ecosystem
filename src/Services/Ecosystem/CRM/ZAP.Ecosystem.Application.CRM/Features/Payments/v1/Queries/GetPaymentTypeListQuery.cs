using MediatR;
using CRM.Payment.Application.Features.PaymentTypes.DTOs;
using CRM.BuildingBlocks.Models;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries
{
    public class GetPaymentTypeListQuery : IRequest<PagedResult<PaymentTypeDto>>
    {
        public FilterDTOs Filter { get; set; } = new();
    }
}


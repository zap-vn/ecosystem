using MediatR;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Commands;

public class CreateBrandCommand : IRequest<object>
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? LogoUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public int StatusId { get; set; } = 1;
}

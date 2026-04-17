global using MediatR;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;

// Shared
global using ZAP.Ecosystem.Shared.Interfaces;
global using ZAP.Ecosystem.Shared.Responses;
global using ZAP.Ecosystem.Shared.Data; // For PagedResult if there

// Domain Entities
global using ZAP.Ecosystem.CRM.Domain.Entities.Customers;
global using ZAP.Ecosystem.CRM.Domain.Entities.Promotions;
// Domain Interfaces
global using ZAP.Ecosystem.CRM.Domain.Interfaces.Customers;
global using ZAP.Ecosystem.CRM.Domain.Interfaces.Promotions;

// CRM Features
global using ZAP.Ecosystem.CRM.Application.Features.Customers.v1.DTOs;
global using ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Queries;
global using ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Commands;



global using ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.DTOs;
global using ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.Queries;
global using ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.Commands;







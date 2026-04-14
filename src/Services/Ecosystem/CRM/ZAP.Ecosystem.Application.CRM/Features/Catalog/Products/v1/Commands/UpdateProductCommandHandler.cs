using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, object>
{
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product == null) return CrmResponse.NotFound("Product");

        product.name = request.Name ?? product.name;
        product.short_description = request.ShortDescription;
        product.long_description_html = request.LongDescriptionHtml;
        product.status_id = request.StatusId;
        product.is_featured = request.IsFeatured;
        product.product_type_id = request.ProductTypeId;

        product.variants = request.Variants.Select(v => new ProductVariant
        {
            id = v.Id ?? Guid.NewGuid(),
            product_id = product.id,
            variant_name = v.VariantName ?? string.Empty,
            sku_code = v.SkuCode ?? string.Empty,
            barcode = v.Barcode,
            sale_price = v.Price,
            base_price = v.OriginalPrice
        }).ToList();

        await _repository.UpdateAsync(product);
        return CrmResponse.Updated(new { id = request.Id });
    }
}


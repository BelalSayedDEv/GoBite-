using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class ProductService:IProductService
{
    private readonly IUnitOfWork unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

using GoBite.Application.Interfaces.Rrepository;
using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class ProductIngredientService:IProductIngredientService
{
    private readonly IUnitOfWork unitOfWork;

    public ProductIngredientService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

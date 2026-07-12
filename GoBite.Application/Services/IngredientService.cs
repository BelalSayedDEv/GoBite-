using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;

namespace GoBite.Application.Services;

public class IngredientService :IIngredientService
{
    private readonly IUnitOfWork unitOfWork;

    public IngredientService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
}

using Azure.Core;
using Catalog.Domain.Categories;
using Catalog.Infrastructure.Patterns;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Catalog.Application.CategoryCommandQuery.Command;

public class AddCategoryCommand : IRequest<AddCategoryCommandRespond>
{
    public string Title { get; set; }
    public string? Description { get; set; }
}

public class AddCategoryCommandRespond
{
    public Guid Id { get; set; }
    public string? Messasge { get; set; }
}
public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, AddCategoryCommandRespond>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AddCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<AddCategoryCommandRespond> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _categoryRepository.Exists(request.Title))
        {
            Log.Warning("Duplicated Title for Category, Title : {0}", request.Title);
            return new AddCategoryCommandRespond { Messasge = "نام تکراری میباشد!" };
        }
        var category = Category.CreateNew(request.Title, request.Description);
        await _categoryRepository.Insert(category);
        await _unitOfWork.SaveChanges();
        Log.Information("new Category Insert to Database, category Title : {0}", request.Title);
        return new AddCategoryCommandRespond { Id = category.Id.Value, Messasge = "دسته بندی با موفقیت اضافه شد!" };

    }
}

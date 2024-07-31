using FluentValidation;
using GameDashboardProject.Application.Abstractions.Mapper;
using GameDashboardProject.Application.Features.Command.AddBuilding;
using GameDashboardProject.Application.UnitOfWorks;
using GameDashboardProject.Domain.Buildings;
using MediatR;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

public class AddBuildingCommandHandler : IRequestHandler<AddBuildingCommand, AddBuildingResponse>
{
    private readonly IMongoUnitOfWorkk _mongoUnitOfWork;
    private readonly IMyMapper _mapper;
    private readonly IValidator<AddBuildingCommand> _validator;

    public AddBuildingCommandHandler(
        IMongoUnitOfWorkk mongoUnitOfWork,
        IMyMapper mapper,
        IValidator<AddBuildingCommand> validator)
    {
        _mongoUnitOfWork = mongoUnitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<AddBuildingResponse> Handle(AddBuildingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new AddBuildingResponse(false, $"Validation failed: {errors}");
        }

        var newBuilding = _mapper.Map<Building>(request);
        newBuilding.CreatedAt = DateTime.UtcNow;
        newBuilding.UpdatedAt = DateTime.UtcNow;
        newBuilding.DeletedAt = null;

        var buildingTypeIdStr = request.BuildingTypeId;

        var buildingReadRepository = _mongoUnitOfWork.GetReadRepository<Building>();
        var existingBuilding = await buildingReadRepository.FindAsync(b => b.BuildingTypeId == buildingTypeIdStr);
        if (existingBuilding != null)
        {
            return new AddBuildingResponse(false, $"A building with type ID {buildingTypeIdStr} already exists.");
        }

        var buildingWriteRepository = _mongoUnitOfWork.GetWriteRepository<Building>();
        await buildingWriteRepository.AddAsync(newBuilding);
        await _mongoUnitOfWork.SaveAsync();

        return new AddBuildingResponse(true, "Building added successfully");
    }
}

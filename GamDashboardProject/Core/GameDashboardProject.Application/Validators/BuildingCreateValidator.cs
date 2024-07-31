using FluentValidation;
using MongoDB.Bson;
using System.Collections.Generic;

public class BuildingCreateValidator : AbstractValidator<AddBuildingCommand>
{
    private readonly List<ObjectId> _validBuildingTypeIds;

    public BuildingCreateValidator()
    {
        _validBuildingTypeIds = GetValidBuildingTypeIds();

        RuleFor(x => x.BuildingTypeId)
            .NotEmpty().WithMessage("Building type is required.")
            .Must(BeAValidBuildingType).WithMessage("Building type is not valid.");

        RuleFor(x => x.BuildingCost)
            .GreaterThan(0).WithMessage("Building cost must be greater than zero.");

        RuleFor(x => x.ConstructionTime)
            .InclusiveBetween(30, 1800).WithMessage("Construction time must be between 30 and 1800 seconds.");
    }

    private bool BeAValidBuildingType(string buildingTypeId)
    {
        if (ObjectId.TryParse(buildingTypeId, out ObjectId objectId))
        {
            return _validBuildingTypeIds.Contains(objectId);
        }
        return false;
    }

    private List<ObjectId> GetValidBuildingTypeIds()
    {
        return new List<ObjectId>
        {
            new ObjectId("66a7f898a27741885b97d4f2"), // Farm
            new ObjectId("66a7f898a27741885b97d4f3"), // Academy
            new ObjectId("66a7f898a27741885b97d4f4"), // Headquarters
            new ObjectId("66a7f898a27741885b97d4f5"), // LumberMill
            new ObjectId("66a7f898a27741885b97d4f6")  // Barracks
        };
    }
}

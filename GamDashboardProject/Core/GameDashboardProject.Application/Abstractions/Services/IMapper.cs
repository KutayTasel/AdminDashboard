
using GameDashboardProject.Domain.Buildings;

namespace GameDashboardProject.Application.Abstractions.Mapper
{
    public interface IMyMapper
    {
        TDestination Map<TDestination, TSource>(TSource source);
        IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources);
        TDestination Map<TDestination>(object source);
        IList<TDestination> Map<TDestination>(IList<object> source);
    }
}

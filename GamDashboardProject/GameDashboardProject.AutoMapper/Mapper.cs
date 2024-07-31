using GameDashboardProject.Application.Abstractions.Mapper;
using GameDashboardProject.Domain.Buildings;
using Mapster;
using System.Collections.Generic;

namespace GameDashboardProject.Mapper
{
    public class Mapper : IMyMapper
    {
        public TDestination Map<TDestination, TSource>(TSource source)
        {
            return source.Adapt<TDestination>();
        }

        public IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources)
        {
            return sources.Adapt<IList<TDestination>>();
        }

        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>();
        }

        public IList<TDestination> Map<TDestination>(IList<object> source)
        {
            return source.Adapt<IList<TDestination>>();
        }
    }
}

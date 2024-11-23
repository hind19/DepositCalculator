using Persistence.Interfaces;
using Persistence.Repositories;

namespace Application
{
    // Simplified solution instead of DI contiainer
    internal static class DependencyResolver<T> where T : IRepository
    {
        private static readonly Dictionary<Type, Type> _dependencyValues = new Dictionary<Type, Type>
        {
            { typeof(IDepositPlanRepository), typeof(DepositPlanRepository) },
        };
        public static T ResolveDependency()
        {
            if (!_dependencyValues.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException($"Dependency {typeof(T).FullName}  has not been implemented yet");
            }
            return (T)Activator.CreateInstance(_dependencyValues[typeof(T)])!;
        }
    }
}

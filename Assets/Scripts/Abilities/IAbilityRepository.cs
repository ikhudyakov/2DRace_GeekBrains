using System.Collections.Generic;

namespace Ability
{
    public interface IAbilityRepository
    {
        internal IReadOnlyDictionary<int, IAbility> AbilityMapId { get; }
    }
}

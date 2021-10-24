using Controllers;
using Inventory;
using Items;
using System;
using System.Linq;

namespace Ability
{
    public class AbilityController : BaseController
    {
        private readonly IInvetoryModel _invetory;
        private readonly IAbilityRepository _abilityRepository;
        private readonly IAbilityCollectionView _view;
        private readonly IAbilityActivator _activator;

        public AbilityController(
            IInvetoryModel invetory, 
            IAbilityRepository abilityRepository, 
            IAbilityCollectionView view,
            IAbilityActivator activator)
        {
            _invetory = invetory;
            _abilityRepository = abilityRepository;
            _view = view;
            _activator = activator;

            var equiped = invetory.GetEquippedItems();
            var equipedAbilities = equiped.Where(i => _abilityRepository.AbilityMapId.ContainsKey(i.Id));
            view.Display(equipedAbilities.ToList());
            view.UseRequested += OnAbilityRequested;
        }

        private void OnAbilityRequested(object sender, IItem e)
        {
            var ability = _abilityRepository.AbilityMapId[e.Id];
            ability.Apply(_activator);
        }
    }
}

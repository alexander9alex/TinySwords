using System.Collections.Generic;
using Code.UI.Buttons.Data;
using Entitas;

namespace Code.Gameplay.Features.FastInteract
{
  [Game] public class Interactable : IComponent {}
  [Game] public class PickedForInteraction : IComponent { public List<int> Value; }
  [Game] public class TargetId : IComponent { public int Value; }
  [Game] public class InteractWithBuildingRequest : IComponent { }
  [Game] public class InteractWithUnitRequest : IComponent { }
  [Game] public class ActionTypeIdComponent : IComponent { public ActionTypeId Value; }
  [Game] public class AllActionTypeIds : IComponent { public List<ActionTypeId> Value; }
}

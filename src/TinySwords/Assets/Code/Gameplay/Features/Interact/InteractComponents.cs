using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Interact
{
  [Game] public class Interactable : IComponent {}
  [Game] public class PickedForInteraction : IComponent { public List<int> Value; }
  [Game] public class TargetId : IComponent { public int Value; }
  [Game] public class InteractWithBuildingRequest : IComponent { }
  [Game] public class InteractWithUnitRequest : IComponent { }
}

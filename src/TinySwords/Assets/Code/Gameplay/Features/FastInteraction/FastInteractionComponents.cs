using System.Collections.Generic;
using Code.Gameplay.Features.ControlAction.Data;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction
{
  [Game] public class FastInteraction : IComponent { }
  [Game] public class TargetId : IComponent { public int Value; }
  [Game] public class AllUnitCommandTypeIds : IComponent { public List<UnitCommandTypeId> Value; }
}

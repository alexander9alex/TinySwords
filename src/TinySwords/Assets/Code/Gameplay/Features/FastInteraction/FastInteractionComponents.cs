using System.Collections.Generic;
using Code.UI.Buttons.Data;
using Entitas;

namespace Code.Gameplay.Features.FastInteraction
{
  [Game] public class FastInteraction : IComponent { }
  [Game] public class TargetId : IComponent { public int Value; }
  [Game] public class AllActionTypeIds : IComponent { public List<ControlActionTypeId> Value; }
}

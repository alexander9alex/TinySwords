using System.Collections.Generic;
using Code.Gameplay.Features.Command.Data;
using Entitas;

namespace Code.Gameplay.Features.FastInteractions
{
  [Game] public class FastInteraction : IComponent { }
  [Game] public class AllUnitCommandTypeIds : IComponent { public List<CommandTypeId> Value; }
}

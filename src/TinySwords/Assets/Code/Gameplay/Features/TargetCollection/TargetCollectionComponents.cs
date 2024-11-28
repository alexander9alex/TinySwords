using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection
{
  [Game] public class CollectTargetsRadius : IComponent { public float Value; }
  [Game] public class TargetBuffer : IComponent { public List<int> Value; }

  [Game] public class CollectReachedTargetsRadius : IComponent { public float Value; }
  [Game] public class ReachedTargetBuffer : IComponent { public List<int> Value; }
}

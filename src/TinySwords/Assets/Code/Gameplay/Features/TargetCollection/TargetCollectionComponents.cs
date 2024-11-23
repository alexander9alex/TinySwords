using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection
{
  [Game] public class CollectTargetsRequest : IComponent { }
  [Game] public class CollectTargetsTimer : IComponent { public float Value; }
  [Game] public class CollectTargetsInterval : IComponent { public float Value; }
  [Game] public class CollectTargetsRadius : IComponent { public float Value; }
  [Game] public class TargetBuffer : IComponent { public List<int> Value; }
}

using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection
{
  [Game] public class CollectTargetsRadius : IComponent { public float Value; }
  [Game] public class TargetBuffer : IComponent { public List<int> Value; }
  [Game] public class CollectTargetTimer : IComponent { public float Value; }
  [Game] public class CollectTargetInterval : IComponent { public float Value; }
  [Game] public class CollectTargets : IComponent { }

  [Game] public class CollectReachedTargetsRadius : IComponent { public float Value; }
  [Game] public class ReachedTargetBuffer : IComponent { public List<int> Value; }
  
  [Game] public class CollectAlliesRadius : IComponent { public float Value; }
  [Game] public class AllyBuffer : IComponent { public List<int> Value; }
  [Game] public class AllyTargetId : IComponent { public int Value; }
  [Game] public class NotifyAlliesAboutTarget : IComponent { }
}

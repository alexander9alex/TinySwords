using System.Collections.Generic;
using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI;
using Entitas;

namespace Code.Gameplay.Features.AI
{
  [Game] public class MakeDecisionRequest : IComponent { }
  [Game] public class UnitAIComponent : IComponent { public IUnitAI Value; }
  [Game] public class UnitDecisionComponent : IComponent { public UnitDecision Value; }
  
  [Game] public class VisionRadius : IComponent { public float Value; }
  [Game] public class VisibleEntityBuffer : IComponent { public List<(int, float)> Value; }
  [Game] public class UpdateFieldOfVisionTimer : IComponent { public float Value; }
  [Game] public class UpdateFieldOfVisionInterval : IComponent { public float Value; }
  [Game] public class UpdateFieldOfVision : IComponent { }
  [Game] public class TimeSinceLastVisionUpdated : IComponent { public float Value; }
  [Game] public class UpdateFieldOfVisionNowRequest : IComponent { }

  [Game] public class CollectTargetsRadius : IComponent { public float Value; }
  [Game] public class TargetBuffer : IComponent { public List<int> Value; }

  [Game] public class CollectReachedTargetsRadius : IComponent { public float Value; }
  [Game] public class ReachedTargetBuffer : IComponent { public List<int> Value; }
  
  [Game] public class CollectAlliesRadius : IComponent { public float Value; }
  [Game] public class AllyBuffer : IComponent { public List<int> Value; }
  [Game] public class AllyTargetId : IComponent { public int Value; }
  [Game] public class NotifyAlliesAboutTarget : IComponent { }
}

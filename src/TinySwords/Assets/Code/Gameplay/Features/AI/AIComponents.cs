using Code.Gameplay.Features.Units.Data;
using Code.Gameplay.UtilityAI;
using Entitas;

namespace Code.Gameplay.Features.AI
{
  [Game] public class MakeDecisionRequest : IComponent { }
  [Game] public class UnitAIComponent : IComponent { public IUnitAI Value; }
  [Game] public class UnitDecisionComponent : IComponent { public UnitDecision Value; }
  
  [Game] public class NotifyAlliesAboutTarget : IComponent { }
}

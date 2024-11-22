using Code.Gameplay.UtilityAI;
using Entitas;

namespace Code.Gameplay.Features.AI
{
  [Game] public class MakeDecisionRequest : IComponent { }
  [Game] public class MakeDecisionTimer : IComponent { public float Value; }
  [Game] public class MakeDecisionInterval : IComponent { public float Value; }
  [Game] public class UnitAIComponent : IComponent { public IUnitAI Value; }
}

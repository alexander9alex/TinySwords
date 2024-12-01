using Entitas;

namespace Code.Gameplay.Features.AttackIndicator
{
  [Game] public class CreateAttackIndicator : IComponent { }
  [Game] public class AttackIndicator : IComponent { }
  [Game] public class DestructOldAttackIndicatorRequest : IComponent { }
  [Game] public class FollowToTarget : IComponent { }
}

using Entitas;

namespace Code.Gameplay.Features.UpdateAvoidance
{
  [Game] public class IdleAvoidancePriority : IComponent { public int Value; }
  [Game] public class MoveAvoidancePriority : IComponent { public int Value; }
  [Game] public class CurrentAvoidancePriority : IComponent { public int Value; }

}

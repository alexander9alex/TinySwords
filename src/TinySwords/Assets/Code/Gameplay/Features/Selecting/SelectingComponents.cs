using Entitas;

namespace Code.Gameplay.Features.Selecting
{
  public class SelectingComponents
  {
    [Game] public class Selected : IComponent { }
    [Game] public class Selectable : IComponent { }
    [Game] public class UnselectRequest : IComponent { }
    [Game] public class TargetId : IComponent { public int Value; }
  }
}

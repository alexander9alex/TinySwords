using Entitas;

namespace Code.Gameplay.Features.Selecting
{
  public class SelectingComponents
  {
    [Game] public class Selected : IComponent { }
    [Game] public class Unselected : IComponent { }
    [Game] public class SelectedNow : IComponent { }
    [Game] public class Selectable : IComponent { }
    [Game] public class UnselectPreviouslySelectedRequest : IComponent { }
    [Game] public class TargetId : IComponent { public int Value; }
  }
}

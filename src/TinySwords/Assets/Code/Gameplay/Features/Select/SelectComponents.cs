using Code.Gameplay.Features.Units.Animations.Animators;
using Entitas;

namespace Code.Gameplay.Features.Select
{
  public class SelectComponents
  {
    [Game] public class Selected : IComponent { }
    [Game] public class Unselected : IComponent { }
    [Game] public class SelectedNow : IComponent { }
    [Game] public class Selectable : IComponent { }
    [Game] public class SelectedChanged : IComponent { }
    [Game] public class UnselectPreviouslySelectedRequest : IComponent { }
    [Game] public class SelectingAnimator : IComponent { public ISelectingAnimator Value; }
    
    [Game] public class SingleSelectionRequest : IComponent { }
    [Game] public class MultipleSelectionRequest : IComponent { }
  }
}

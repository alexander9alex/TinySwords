using Code.Gameplay.Features.Interactions.Select.Animators;
using Entitas;

namespace Code.Gameplay.Features.Interactions.Select
{
    [Game] public class Selected : IComponent { }
    [Game] public class Unselected : IComponent { }
    [Game] public class SelectedNow : IComponent { }
    [Game] public class Selectable : IComponent { }
    [Game] public class UnselectPreviouslySelected : IComponent { }
    [Game] public class UpdateHudControlButtons : IComponent { }
    [Game] public class SelectingAnimator : IComponent { public ISelectingAnimator Value; }
}

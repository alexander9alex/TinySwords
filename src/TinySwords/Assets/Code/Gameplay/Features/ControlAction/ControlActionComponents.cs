using Code.UI.Buttons.Data;
using Entitas;

namespace Code.Gameplay.Features.ControlAction
{
  [Game] public class ActionStarted : IComponent { }
  [Game] public class ActionEnded : IComponent { }
  [Game] public class ApplyControlAction : IComponent { }
  [Game] public class CancelControlAction : IComponent { }
  [Game] public class MoveAction : IComponent { }
  [Game] public class MoveWithAttackAction : IComponent { }
  [Game] public class SelectedAction : IComponent { }
  [Game] public class ActionTypeIdComponent : IComponent { public ControlActionTypeId Value; }
}

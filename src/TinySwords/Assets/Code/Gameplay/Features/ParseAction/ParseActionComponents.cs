using Code.UI.Buttons.Data;
using Entitas;

namespace Code.Gameplay.Features.ParseAction
{
  [Game] public class MoveAction : IComponent { }
  [Game] public class MoveWithAttackAction : IComponent { }
  [Game] public class SelectedAction : IComponent { }
  [Game] public class ActionTypeIdComponent : IComponent { public ActionTypeId Value; }
}

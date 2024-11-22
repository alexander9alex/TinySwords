using Code.Gameplay.Features.ControlAction.Data;
using Entitas;

namespace Code.Gameplay.Features.ControlAction
{
  [Game] public class ActionStarted : IComponent { }
  [Game] public class ActionEnded : IComponent { }
  [Game] public class ApplyControlAction : IComponent { }
  [Game] public class CancelControlAction : IComponent { }
  [Game] public class MoveControlAction : IComponent { }
  [Game] public class MoveWithAttackControlAction : IComponent { }
  [Game] public class SelectedAction : IComponent { }
  [Game] public class UnitCommandTypeIdComponent : IComponent { public UnitCommandTypeId Value; }
  
  [Game] public class StayUnitAction : IComponent { }
  [Game] public class MoveUnitAction : IComponent { }
  [Game] public class AttackUnitAction : IComponent { }
}

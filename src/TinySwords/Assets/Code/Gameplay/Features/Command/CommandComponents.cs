using Code.Gameplay.Features.Command.Data;
using Entitas;

namespace Code.Gameplay.Features.Command
{
  [Game] public class Command : IComponent { }
  [Game] public class ApplyCommand : IComponent { }
  [Game] public class CancelCommand : IComponent { }
  [Game] public class SelectedCommand : IComponent { }
  [Game] public class ProcessCommand : IComponent { }
  [Game] public class RemovePreviousCommand : IComponent { }
  [Game] public class MoveCommand : IComponent { }
  [Game] public class MoveWithAttackCommand : IComponent { }
  [Game] public class AimedAttackCommand : IComponent { }
  [Game] public class CommandTypeIdComponent : IComponent { public CommandTypeId Value; }
  [Game] public class AimedTargetId : IComponent { public int Value; }
  }

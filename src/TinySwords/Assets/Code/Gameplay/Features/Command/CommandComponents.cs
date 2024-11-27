using Code.Gameplay.Features.Command.Data;
using Entitas;

namespace Code.Gameplay.Features.Command
{
  [Game] public class ApplyCommand : IComponent { }
  [Game] public class CancelCommand : IComponent { }
  [Game] public class SelectedCommand : IComponent { }
  [Game] public class UpdateCommand : IComponent { }
  [Game] public class CommandTypeIdComponent : IComponent { public CommandTypeId Value; }

  }

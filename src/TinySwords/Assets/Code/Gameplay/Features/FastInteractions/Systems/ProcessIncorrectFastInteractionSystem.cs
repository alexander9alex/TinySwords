using Code.Gameplay.Features.Command.Data;
using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.FastInteractions.Systems
{
  public class ProcessIncorrectFastInteractionSystem : IExecuteSystem
  {
    private readonly ICommandService _commandService;
    private readonly IGroup<GameEntity> _fastInteractions;

    public ProcessIncorrectFastInteractionSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;

      _fastInteractions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.FastInteraction,
          GameMatcher.ScreenPosition
        )
        .NoneOf(GameMatcher.Processed));
    }

    public void Execute()
    {
      foreach (GameEntity fastInteraction in _fastInteractions)
      {
        _commandService.ProcessIncorrectCommand(CommandTypeId.Move, fastInteraction.ScreenPosition);
      }
    }
  }
}

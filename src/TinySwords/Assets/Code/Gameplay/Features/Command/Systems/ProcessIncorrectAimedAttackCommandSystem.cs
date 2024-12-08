using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessIncorrectAimedAttackCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _processIncorrectCommandRequests;
    private readonly ICommandService _commandService;

    public ProcessIncorrectAimedAttackCommandSystem(GameContext game, ICommandService commandService)
    {
      _commandService = commandService;
      
      _processIncorrectCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessIncorrectCommandRequest, GameMatcher.AimedAttackCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processIncorrectCommandRequests)
      {
        _commandService.ProcessIncorrectAimedAttack(request.PositionOnScreen);
      }
    }
  }
}

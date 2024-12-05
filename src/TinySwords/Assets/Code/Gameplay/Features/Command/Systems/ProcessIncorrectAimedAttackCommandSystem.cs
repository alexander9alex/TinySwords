using Code.Gameplay.Features.Command.Services;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ProcessIncorrectAimedAttackCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _processIncorrectCommandRequests;
    private readonly IProcessCommandService _processCommandService;

    public ProcessIncorrectAimedAttackCommandSystem(GameContext game, IProcessCommandService processCommandService)
    {
      _processCommandService = processCommandService;
      
      _processIncorrectCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessIncorrectCommandRequest, GameMatcher.AimedAttackCommand, GameMatcher.CommandTypeId, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processIncorrectCommandRequests)
      {
        _processCommandService.ProcessIncorrectAimedAttack(request);
      }
    }
  }
}

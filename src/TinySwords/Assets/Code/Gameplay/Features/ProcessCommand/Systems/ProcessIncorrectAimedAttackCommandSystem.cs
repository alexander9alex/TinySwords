using Code.Gameplay.Features.ProcessCommand.Services;
using Entitas;

namespace Code.Gameplay.Features.ProcessCommand.Systems
{
  public class ProcessIncorrectAimedAttackCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _processIncorrectCommandRequests;
    private readonly IProcessCommandService _processCommandService;

    public ProcessIncorrectAimedAttackCommandSystem(GameContext game, IProcessCommandService processCommandService)
    {
      _processCommandService = processCommandService;
      
      _processIncorrectCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessIncorrectCommandRequest, GameMatcher.AimedAttackCommand, GameMatcher.CommandTypeId, GameMatcher.ScreenPosition));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processIncorrectCommandRequests)
      {
        _processCommandService.ProcessIncorrectAimedAttack(request.ScreenPosition);
      }
    }
  }
}

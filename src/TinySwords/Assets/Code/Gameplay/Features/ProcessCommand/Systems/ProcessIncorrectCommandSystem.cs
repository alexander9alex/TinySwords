using Code.Gameplay.Features.ProcessCommand.Services;
using Entitas;

namespace Code.Gameplay.Features.ProcessCommand.Systems
{
  public class ProcessIncorrectCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _processIncorrectCommandRequests;
    private readonly IProcessCommandService _processCommandService;

    public ProcessIncorrectCommandSystem(GameContext game, IProcessCommandService processCommandService)
    {
      _processCommandService = processCommandService;
      
      _processIncorrectCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ProcessIncorrectCommandRequest, GameMatcher.CommandTypeId, GameMatcher.ScreenPosition));
    }

    public void Execute()
    {
      foreach (GameEntity request in _processIncorrectCommandRequests)
      {
        _processCommandService.ProcessIncorrectCommand(request.ScreenPosition);
      }
    }
  }
}

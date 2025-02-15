using Code.Gameplay.Features.Win.Services;
using Entitas;

namespace Code.Gameplay.Features.Win.Systems
{
  public class CheckKillToWinConditionSystem : IExecuteSystem
  {
    private readonly IWinService _winService;
    
    private readonly IGroup<GameEntity> _conditions;
    private readonly IGroup<GameEntity> _units;

    public CheckKillToWinConditionSystem(GameContext game, IWinService winService)
    {
      _winService = winService;
      _conditions = game.GetGroup(GameMatcher.KillToWinCondition);

      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.KillToWin));
    }

    public void Execute()
    {
      foreach (GameEntity condition in _conditions)
      {
        if (_units.count <= 0)
        {
          _winService.Win();
          
          condition.isDestructed = true;
        }
      }
    }
  }
}

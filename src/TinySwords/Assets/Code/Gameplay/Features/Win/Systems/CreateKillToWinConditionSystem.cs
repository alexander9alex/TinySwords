using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Win.Systems
{
  public class CreateKillToWinConditionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _requests;
    private readonly IGroup<GameEntity> _units;

    public CreateKillToWinConditionSystem(GameContext game)
    {
      _requests = game.GetGroup(GameMatcher.CreateWinCondition);

      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.KillToWin));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _requests)
      {
        if (_units.count > 0)
          CreateKillToWinCondition();
      }
    }

    private void CreateKillToWinCondition()
    {
      CreateEntity.Empty()
        .With(x => x.isKillToWinCondition = true);
    }
  }
}

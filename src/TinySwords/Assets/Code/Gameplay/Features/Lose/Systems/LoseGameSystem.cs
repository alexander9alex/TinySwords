using Code.Common.Entities;
using Code.Gameplay.Features.Lose.Services;
using Entitas;

namespace Code.Gameplay.Features.Lose.Systems
{
  public class LoseGameSystem : IExecuteSystem
  {
    private readonly ILoseService _loseService;

    private readonly IGroup<GameEntity> _units;
    private readonly IGroup<GameEntity> _loses;

    public LoseGameSystem(GameContext game, ILoseService loseService)
    {
      _loseService = loseService;
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.BlueTeamColor));

      _loses = game.GetGroup(GameMatcher.Lose);
    }

    public void Execute()
    {
      if (_loses.count > 0)
        return;

      if (_units.count <= 0)
      {
        _loseService.Lose();

        CreateEntity.Empty()
          .isLose = true;
      }
    }
  }
}

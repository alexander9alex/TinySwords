using System.Collections.Generic;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Units.Services;
using Entitas;

namespace Code.Gameplay.Features.Units.Systems
{
  public class RecruitNeutralUnitSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly IRecruitUnitService _recruitUnitService;

    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(64);

    public RecruitNeutralUnitSystem(GameContext game, IRecruitUnitService recruitUnitService)
    {
      _game = game;
      _recruitUnitService = recruitUnitService;
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.NeutralUnit, GameMatcher.AllyBuffer, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        foreach (int allyId in unit.AllyBuffer)
        {
          GameEntity ally = _game.GetEntityWithId(allyId);

          if (ally is not { isAlive: true, hasTeamColor: true })
            return;

          if (ally.TeamColor == GameConstants.UserTeamColor)
          {
            _recruitUnitService.RecruitUnit(unit);
            unit.isNeutralUnit = false;
          }
        }
      }
    }
  }
}

using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class UnitMakeDecisionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(32);

    public UnitMakeDecisionSystem(GameContext game)
    {
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.MakeDecisionRequest, GameMatcher.UnitAI));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        unit.UnitAI.MakeBestDecision(unit);
        unit.isMakeDecisionRequest = false;
      }
    }
  }
}

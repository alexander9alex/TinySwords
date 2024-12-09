using System.Collections.Generic;
using Code.Gameplay.Features.AI.Services;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class RemoveAllUnitDecisionsSystem : IExecuteSystem
  {
    private readonly IDecisionService _decisionService;
    
    private readonly IGroup<GameEntity> _units;
    private readonly List<GameEntity> _buffer = new(32);

    public RemoveAllUnitDecisionsSystem(GameContext game, IDecisionService decisionService)
    {
      _decisionService = decisionService;
      _units = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit, GameMatcher.UnitDecision));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _units.GetEntities(_buffer))
      {
        _decisionService.RemoveUnitDecisions(unit);
      }
    }
  }
}

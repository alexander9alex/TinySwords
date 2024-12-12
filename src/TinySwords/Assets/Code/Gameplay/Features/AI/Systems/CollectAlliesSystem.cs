using System.Collections.Generic;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Constants;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class CollectAlliesSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CollectAlliesSystem(GameContext game, IPhysicsService physicsService)
    {
      _physicsService = physicsService;

      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.MakeDecisionRequest,
          GameMatcher.CollectAlliesRadius,
          GameMatcher.AllyBuffer,
          GameMatcher.WorldPosition,
          GameMatcher.TeamColor,
          GameMatcher.Alive
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        List<int> allies = new();
        
        foreach (GameEntity ally in GetAlliesInRadius(entity))
        {
          if (!ally.isAlive || !ally.hasId || !ally.hasTeamColor || entity.TeamColor != ally.TeamColor)
            continue;

          allies.Add(ally.Id);
        }

        entity.ReplaceAllyBuffer(allies);
      }
    }

    private IEnumerable<GameEntity> GetAlliesInRadius(GameEntity entity)
    {
      return _physicsService.CircleCast(
        entity.WorldPosition,
        entity.CollectAlliesRadius,
        GameConstants.UnitsAndBuildingsLayerMask);
    }
  }
}

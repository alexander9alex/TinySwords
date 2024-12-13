using System.Collections.Generic;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Constants;
using Entitas;

namespace Code.Gameplay.Features.NearestEntitiesCollection.Systems
{
  public class CollectReachedTargetsSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CollectReachedTargetsSystem(GameContext game, IPhysicsService physicsService)
    {
      _physicsService = physicsService;

      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.CollectTargets,
          GameMatcher.CollectReachedTargetsRadius,
          GameMatcher.ReachedTargetBuffer,
          GameMatcher.WorldPosition,
          GameMatcher.TeamColor,
          GameMatcher.Alive
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        List<int> reachedTargets = new();

        foreach (GameEntity target in GetTargetsInRadius(entity))
        {
          if (!target.isAlive || !target.hasTeamColor || !target.hasId || entity.TeamColor == target.TeamColor)
            continue;

          reachedTargets.Add(target.Id);
        }

        if (!new HashSet<int>(reachedTargets).SetEquals(entity.ReachedTargetBuffer))
        {
          entity.ReplaceReachedTargetBuffer(reachedTargets);
          entity.ReplaceMakeDecisionTimer(0);
        }
      }
    }

    private IEnumerable<GameEntity> GetTargetsInRadius(GameEntity entity)
    {
      return _physicsService.CircleCast(
        entity.WorldPosition,
        entity.CollectReachedTargetsRadius,
        GameConstants.UnitsAndBuildingsLayerMask);
    }
  }
}

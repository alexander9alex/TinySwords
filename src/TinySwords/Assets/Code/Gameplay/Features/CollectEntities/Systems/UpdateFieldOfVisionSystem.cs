using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.CollectEntities.Systems
{
  public class UpdateFieldOfVisionSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public UpdateFieldOfVisionSystem(GameContext game, IPhysicsService physicsService)
    {
      _physicsService = physicsService;

      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.UpdateFieldOfVision,
          GameMatcher.VisionRadius,
          GameMatcher.VisibleEntityBuffer,
          GameMatcher.WorldPosition,
          GameMatcher.TeamColor,
          GameMatcher.Alive
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        List<(int, float)> nearestEntities = GetNearestEntities(entity);

        entity.ReplaceVisibleEntityBuffer(nearestEntities);
        entity.isMakeDecisionRequest = true;
        
        entity.isUpdateFieldOfVision = false;
      }
    }

    private List<(int, float)> GetNearestEntities(GameEntity entity)
    {
      return (
          from target in GetEntitiesInRadius(entity)
          where TargetIsSuitable(target)
          select (target.Id, Vector2.Distance(entity.WorldPosition, target.WorldPosition))
        ).ToList();
    }

    private IEnumerable<GameEntity> GetEntitiesInRadius(GameEntity entity)
    {
      return _physicsService.CircleCast(
        entity.WorldPosition,
        entity.VisionRadius,
        GameConstants.UnitsAndBuildingsLayerMask);
    }

    private static bool TargetIsSuitable(GameEntity target) =>
      target.isAlive && target.hasTeamColor && target.hasId && target.hasWorldPosition;
  }
}

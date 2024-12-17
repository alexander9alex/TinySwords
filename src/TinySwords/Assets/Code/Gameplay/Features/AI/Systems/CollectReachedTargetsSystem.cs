using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
{
  public class CollectReachedTargetsSystem : IExecuteSystem
  {
    private readonly GameContext _game;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CollectReachedTargetsSystem(GameContext game)
    {
      _game = game;

      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.MakeDecisionRequest,
          GameMatcher.VisibleEntityBuffer,
          GameMatcher.CollectReachedTargetsRadius,
          GameMatcher.ReachedTargetBuffer,
          GameMatcher.TeamColor,
          GameMatcher.Alive
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        List<int> reachedTargets = new();

        foreach ((int id, float distance) targetParams in entity.VisibleEntityBuffer)
        {
          GameEntity target = _game.GetEntityWithId(targetParams.id);

          if (TargetIsSuitable(entity, target, targetParams.distance))
            reachedTargets.Add(target.Id);
        }

        entity.ReplaceReachedTargetBuffer(reachedTargets);
      }
    }

    private bool TargetIsSuitable(GameEntity entity, GameEntity target, float distanceToTarget)
    {
      if (distanceToTarget > entity.CollectReachedTargetsRadius)
        return false;

      if (target is not { isAlive: true, hasTeamColor: true, hasId: true })
        return false;

      if (entity.TeamColor == target.TeamColor)
        return false;

      return true;
    }
  }
}

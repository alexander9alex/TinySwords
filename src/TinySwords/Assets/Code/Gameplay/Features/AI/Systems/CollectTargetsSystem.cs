using System.Collections.Generic;
using Entitas;
using static Code.Gameplay.Constants.GameConstants;

namespace Code.Gameplay.Features.AI.Systems
{
  public class CollectTargetsSystem : IExecuteSystem
  {
    private readonly GameContext _game;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CollectTargetsSystem(GameContext game)
    {
      _game = game;

      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.MakeDecisionRequest,
          GameMatcher.VisibleEntityBuffer,
          GameMatcher.CollectTargetsRadius,
          GameMatcher.TargetBuffer,
          GameMatcher.TeamColor,
          GameMatcher.Alive
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        List<int> targets = new();

        foreach ((int id, float distance) targetParams in entity.VisibleEntityBuffer)
        {
          GameEntity target = _game.GetEntityWithId(targetParams.id);

          if (TargetIsSuitable(entity, target, targetParams.distance))
            targets.Add(target.Id);
        }

        entity.ReplaceTargetBuffer(targets);
      }
    }

    private static bool TargetIsSuitable(GameEntity entity, GameEntity target, float distanceToTarget)
    {
      if (EntityIsUnreachable(entity.CollectTargetsRadius, distanceToTarget))
        return false;

      if (TargetIsNotValid(target))
        return false;

      if (SameTeamColor(entity, target))
        return false;

      if (IsAlly(entity, target))
        return false;

      return true;
    }

    private static bool IsAlly(GameEntity entity, GameEntity target) =>
      AllyTeamColor[entity.TeamColor] == target.TeamColor;

    private static bool SameTeamColor(GameEntity entity, GameEntity target) =>
      entity.TeamColor == target.TeamColor;

    private static bool TargetIsNotValid(GameEntity target) =>
      target is not { isAlive: true, hasTeamColor: true, hasId: true };

    private static bool EntityIsUnreachable(float collectTargetRadius, float distanceToTarget) =>
      distanceToTarget > collectTargetRadius;
  }
}

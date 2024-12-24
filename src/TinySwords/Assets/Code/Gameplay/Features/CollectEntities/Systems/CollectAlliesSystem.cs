using System.Collections.Generic;
using Entitas;
using static Code.Gameplay.Constants.GameConstants;

namespace Code.Gameplay.Features.CollectEntities.Systems
{
  public class CollectAlliesSystem : IExecuteSystem
  {
    private readonly GameContext _game;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CollectAlliesSystem(GameContext game)
    {
      _game = game;

      _entities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.MakeDecisionRequest,
          GameMatcher.VisibleEntityBuffer,
          GameMatcher.CollectAlliesRadius,
          GameMatcher.AllyBuffer,
          GameMatcher.TeamColor,
          GameMatcher.Alive
        ));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        List<int> allies = new();

        foreach ((int id, float distance) allyParams in entity.VisibleEntityBuffer)
        {
          GameEntity ally = _game.GetEntityWithId(allyParams.id);

          if (AllyIsSuitable(entity, ally, allyParams.distance))
            allies.Add(ally.Id);
        }

        entity.ReplaceAllyBuffer(allies);
      }
    }

    private bool AllyIsSuitable(GameEntity entity, GameEntity ally, float distanceToAlly)
    {
      if (EntityIsUnreachable(entity.CollectAlliesRadius, distanceToAlly))
        return false;

      if (AllyIsNotValid(ally))
        return false;

      if (SameTeamColor(entity, ally))
        return true;

      if (IsAlly(entity, ally))
        return true;

      return false;
    }

    private static bool IsAlly(GameEntity entity, GameEntity ally) =>
      AllyTeamColor[entity.TeamColor] == ally.TeamColor;

    private static bool SameTeamColor(GameEntity entity, GameEntity ally) =>
      entity.TeamColor == ally.TeamColor;

    private static bool AllyIsNotValid(GameEntity ally) =>
      ally is not { isAlive: true, hasTeamColor: true, hasId: true };

    private static bool EntityIsUnreachable(float collectAllyRadius, float distanceToAlly) =>
      distanceToAlly > collectAllyRadius;
  }
}

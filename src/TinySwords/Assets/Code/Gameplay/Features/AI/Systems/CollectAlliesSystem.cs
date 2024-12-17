using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.AI.Systems
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
      if (distanceToAlly > entity.CollectAlliesRadius)
        return false;

      if (ally is not { isAlive: true, hasTeamColor: true, hasId: true })
        return false;

      if (entity.TeamColor != ally.TeamColor)
        return false;

      return true;
    }
  }
}

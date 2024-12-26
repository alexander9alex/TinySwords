using System.Collections.Generic;
using Code.Gameplay.Features.CollectEntities.Services;
using Entitas;

namespace Code.Gameplay.Features.CollectEntities.Systems
{
  public class CollectAlliesSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly ICollectEntityService _collectEntityService;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CollectAlliesSystem(GameContext game, ICollectEntityService collectEntityService)
    {
      _game = game;
      _collectEntityService = collectEntityService;

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
      if (_collectEntityService.EntityIsUnreachable(entity.CollectAlliesRadius, distanceToAlly))
        return false;

      if (_collectEntityService.EntityIsNotValid(ally))
        return false;

      if (_collectEntityService.SameTeamColor(entity, ally))
        return true;

      if (_collectEntityService.IsAlly(entity, ally))
        return true;

      return false;
    }
  }
}

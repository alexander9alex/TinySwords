using System.Collections.Generic;
using Code.Gameplay.Features.CollectEntities.Services;
using Entitas;

namespace Code.Gameplay.Features.CollectEntities.Systems
{
  public class CollectTargetsSystem : IExecuteSystem
  {
    private readonly GameContext _game;
    private readonly ICollectEntityService _collectEntityService;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public CollectTargetsSystem(GameContext game, ICollectEntityService collectEntityService)
    {
      _game = game;
      _collectEntityService = collectEntityService;

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

    private bool TargetIsSuitable(GameEntity entity, GameEntity target, float distanceToTarget)
    {
      if (_collectEntityService.EntityIsUnreachable(entity.CollectTargetsRadius, distanceToTarget))
        return false;

      if (_collectEntityService.EntityIsNotValid(target))
        return false;

      if (_collectEntityService.SameTeamColor(entity, target))
        return false;

      if (_collectEntityService.IsAlly(entity, target))
        return false;

      return true;
    }
  }
}

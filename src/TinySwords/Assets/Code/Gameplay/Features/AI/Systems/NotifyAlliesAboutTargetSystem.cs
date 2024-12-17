using System.Collections.Generic;
using Entitas;
using ModestTree;

namespace Code.Gameplay.Features.AI.Systems
{
  public class NotifyAlliesAboutTargetSystem : IExecuteSystem
  {
    private readonly GameContext _game;

    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new();

    public NotifyAlliesAboutTargetSystem(GameContext game)
    {
      _game = game;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.NotifyAlliesAboutTarget, GameMatcher.AllyBuffer, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        foreach (int allyId in entity.AllyBuffer)
        {
          GameEntity ally = _game.GetEntityWithId(allyId);

          if (NotHaveTargetsAndAllyTargets(ally))
            ally.isUpdateFieldOfVisionNowRequest = true;
        }

        entity.isNotifyAlliesAboutTarget = false;
      }
    }

    private static bool NotHaveTargetsAndAllyTargets(GameEntity ally) =>
      ally is { isAlive: true, hasTargetBuffer: true, hasAllyTargetId: false } && ally.TargetBuffer.IsEmpty();
  }
}

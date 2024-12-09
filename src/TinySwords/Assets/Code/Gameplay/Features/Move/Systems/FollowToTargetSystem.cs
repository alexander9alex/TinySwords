using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class FollowToTargetSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext _game;

    public FollowToTargetSystem(GameContext game)
    {
      _game = game;
      _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.FollowToTarget, GameMatcher.TargetId, GameMatcher.Alive, GameMatcher.NotAttacking));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities)
      {
        GameEntity target = _game.GetEntityWithId(entity.TargetId);

        if (target is not { hasWorldPosition: true })
          continue;

        entity.ReplaceDestination(target.WorldPosition);
      }
    }
  }
}

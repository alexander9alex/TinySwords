using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class FollowToTargetSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _followers;
    private readonly GameContext _game;

    public FollowToTargetSystem(GameContext game)
    {
      _game = game;
      
      _followers = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.FollowToTarget, GameMatcher.TargetId, GameMatcher.Transform));
    }

    public void Execute()
    {
      foreach (GameEntity follower in _followers)
      {
        GameEntity target = _game.GetEntityWithId(follower.TargetId);

        if (target is { hasWorldPosition: true })
          follower.Transform.position = target.WorldPosition;
      }
    }
  }
}

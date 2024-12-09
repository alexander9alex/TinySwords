using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class TeleportToTargetSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _followers;
    private readonly GameContext _game;

    public TeleportToTargetSystem(GameContext game)
    {
      _game = game;
      
      _followers = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.TeleportationToTarget, GameMatcher.TargetId, GameMatcher.Transform));
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

using Entitas;

namespace Code.Gameplay.Features.UpdateAvoidance.Systems
{
  public class UpdateMoveAvoidanceSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _moving;

    public UpdateMoveAvoidanceSystem(GameContext game)
    {
      _moving = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Move, GameMatcher.Destination, GameMatcher.CurrentAvoidancePriority, GameMatcher.MoveAvoidancePriority));
    }

    public void Execute()
    {
      foreach (GameEntity moving in _moving)
      {
        moving.ReplaceCurrentAvoidancePriority(moving.MoveAvoidancePriority);
      }
    }
  }
}

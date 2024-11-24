using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class UpdateUnitAvailabilitySystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _unit;

    public UpdateUnitAvailabilitySystem(GameContext game)
    {
      _unit = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _unit)
      {
        unit.isAvailable = !unit.isAttacking;
      }
    }
  }
}

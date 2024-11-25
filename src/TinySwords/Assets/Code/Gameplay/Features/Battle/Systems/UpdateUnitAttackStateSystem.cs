using Entitas;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class UpdateUnitAttackStateSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _unit;

    public UpdateUnitAttackStateSystem(GameContext game)
    {
      _unit = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Unit));
    }

    public void Execute()
    {
      foreach (GameEntity unit in _unit)
      {
        unit.isNotAttacking = !unit.isAttacking;
      }
    }
  }
}

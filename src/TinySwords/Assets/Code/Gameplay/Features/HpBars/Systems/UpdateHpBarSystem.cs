using Entitas;

namespace Code.Gameplay.Features.HpBars.Systems
{
  public class UpdateHpBarSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _hpBars;

    public UpdateHpBarSystem(GameContext game)
    {
      _hpBars = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.HpBar, GameMatcher.CurrentHp, GameMatcher.MaxHp));
    }

    public void Execute()
    {
      foreach (GameEntity hpBar in _hpBars)
      {
        hpBar.HpBar.UpdateHp(hpBar.CurrentHp, hpBar.MaxHp);
      }
    }
  }
}

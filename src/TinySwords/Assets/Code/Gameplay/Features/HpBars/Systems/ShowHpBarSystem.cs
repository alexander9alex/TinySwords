using Entitas;

namespace Code.Gameplay.Features.HpBars.Systems
{
  public class ShowHpBarSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _hpBars;

    public ShowHpBarSystem(GameContext game)
    {
      _hpBars = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.HpBar, GameMatcher.Focused));
    }

    public void Execute()
    {
      foreach (GameEntity hpBar in _hpBars)
      {
        hpBar.HpBar.Show();
      }
    }
  }
}

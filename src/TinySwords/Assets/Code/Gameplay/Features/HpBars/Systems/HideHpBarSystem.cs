using Entitas;

namespace Code.Gameplay.Features.HpBars.Systems
{
  public class HideHpBarSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _hpBars;

    public HideHpBarSystem(GameContext game)
    {
      // _hpBars = game.GetGroup(GameMatcher
        // .AllOf(GameMatcher.HpBar));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _hpBars)
      {
        
      }
    }
  }
}

using Entitas;

namespace Code.Gameplay.Features.Interactions.Select.Systems
{
  public class AnimateUnselectingSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _unselected;

    public AnimateUnselectingSystem(GameContext game)
    {
      _unselected = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Unselected,
          GameMatcher.SelectingAnimator));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _unselected)
        entity.SelectingAnimator.AnimateUnselecting();
    }
  }
}

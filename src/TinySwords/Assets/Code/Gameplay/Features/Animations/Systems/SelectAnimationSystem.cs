using Entitas;

namespace Code.Gameplay.Features.Animations.Systems
{
  public class SelectAnimationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selected;

    public SelectAnimationSystem(GameContext game)
    {
      _selected = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Selected,
          GameMatcher.SelectingAnimator));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _selected)
        entity.SelectingAnimator.AnimateSelecting();
    }
  }
}

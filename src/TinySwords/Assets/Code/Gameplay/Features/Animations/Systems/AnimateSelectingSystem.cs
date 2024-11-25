using Entitas;

namespace Code.Gameplay.Features.Animations.Systems
{
  public class AnimateSelectingSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selected;

    public AnimateSelectingSystem(GameContext game)
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

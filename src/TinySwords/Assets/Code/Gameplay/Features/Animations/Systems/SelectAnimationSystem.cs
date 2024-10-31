using Entitas;

namespace Code.Gameplay.Features.Animations.Systems
{
  public class SelectAnimationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectedEntities;

    public SelectAnimationSystem(GameContext game)
    {
      _selectedEntities = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Selected,
          GameMatcher.SelectingAnimator));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _selectedEntities)
        entity.SelectingAnimator.AnimateSelecting();
    }
  }
}

using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class DeselectIfClickNotProcessedSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _clicks;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _entitiesBuffer = new(64);

    public DeselectIfClickNotProcessedSystem(GameContext game)
    {
      _clicks = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.LeftClick)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _clicks)
      foreach (GameEntity entity in _selected.GetEntities(_entitiesBuffer))
      {
        entity.isSelected = false;
        entity.isUnselected = true;
      }
    }
  }
}

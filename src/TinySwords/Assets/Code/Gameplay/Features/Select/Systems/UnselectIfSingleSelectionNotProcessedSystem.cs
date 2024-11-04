using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class UnselectIfSingleSelectionNotProcessedSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _singleSelectionRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _entitiesBuffer = new(64);

    public UnselectIfSingleSelectionNotProcessedSystem(GameContext game)
    {
      _singleSelectionRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.SingleSelectionRequest)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _singleSelectionRequests)
      foreach (GameEntity entity in _selected.GetEntities(_entitiesBuffer))
      {
        entity.isSelected = false;
        entity.isUnselected = true;
      }
    }
  }
}

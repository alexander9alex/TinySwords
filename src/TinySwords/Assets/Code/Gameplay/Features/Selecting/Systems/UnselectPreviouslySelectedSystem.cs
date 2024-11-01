using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Selecting.Systems
{
  public class UnselectPreviouslySelectedSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _unselectRequests;
    private readonly IGroup<GameEntity> _previouslySelected;
    private readonly List<GameEntity> _buffer = new(64);

    public UnselectPreviouslySelectedSystem(GameContext game)
    {
      _unselectRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.UnselectPreviouslySelectedRequest));

      _previouslySelected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected)
        .NoneOf(GameMatcher.SelectedNow));
    }

    public void Execute()
    {
      foreach (GameEntity _ in _unselectRequests)
      foreach (GameEntity entity in _previouslySelected.GetEntities(_buffer))
      {
        entity.isSelected = false;
        entity.isUnselected = true;
      }
    }
  }
}

using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Interactions.Select.Systems
{
  public class UnselectPreviouslySelectedSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _unselectRequests;
    private readonly IGroup<GameEntity> _previouslySelected;
    private readonly List<GameEntity> _selectedBuffer = new(64);
    private readonly List<GameEntity> _requestsBuffer = new(1);

    public UnselectPreviouslySelectedSystem(GameContext game)
    {
      _unselectRequests = game.GetGroup(GameMatcher.UnselectPreviouslySelected);

      _previouslySelected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected)
        .NoneOf(GameMatcher.SelectedNow));
    }

    public void Execute()
    {
      foreach (GameEntity request in _unselectRequests.GetEntities(_requestsBuffer))
      {
        foreach (GameEntity previouslySelected in _previouslySelected.GetEntities(_selectedBuffer))
          Unselect(previouslySelected);

        request.isDestructed = true;
      }
    }

    private static void Unselect(GameEntity entity)
    {
      entity.isSelected = false;
      entity.isUnselected = true;
    }
  }
}

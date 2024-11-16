using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class CreateUpdateHudControlButtonsRequest : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectedChanged;

    public CreateUpdateHudControlButtonsRequest(GameContext game)
    {
      _selectedChanged = game.GetGroup(GameMatcher.SelectedChanged);
    }

    public void Execute()
    {
      foreach (GameEntity _ in _selectedChanged)
      {
        CreateEntity.Empty()
          .With(x => x.isUpdateHudControlButtons = true);
      }
    }
  }
}

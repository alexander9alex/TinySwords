using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Select.Systems
{
  public class ProcessSelectedChangingSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _selectedChanged;

    public ProcessSelectedChangingSystem(GameContext game)
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

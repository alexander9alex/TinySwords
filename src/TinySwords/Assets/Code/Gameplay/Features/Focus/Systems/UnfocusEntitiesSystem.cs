using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Focus.Systems
{
  public class UnfocusEntitiesSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _focused;
    private readonly List<GameEntity> _buffer = new(1);

    public UnfocusEntitiesSystem(GameContext game)
    {
      _focused = game.GetGroup(GameMatcher.Focused);
    }

    public void Execute()
    {
      foreach (GameEntity focused in _focused.GetEntities(_buffer))
      {
        focused.isFocused = false;
        focused.isUnfocused = true;
      }
    }
  }
}

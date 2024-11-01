using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CleanupLeftClickSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _interactions;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupLeftClickSystem(GameContext game)
    {
      _interactions = game.GetGroup(GameMatcher.AllOf(GameMatcher.LeftClick));
    }

    public void Cleanup()
    {
      foreach (GameEntity interaction in _interactions.GetEntities(_buffer))
      {
        interaction.isDestructed = true;
      }
    }
  }
}

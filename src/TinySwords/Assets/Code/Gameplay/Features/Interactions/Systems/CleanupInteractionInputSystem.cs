using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Interactions.Systems
{
  public class CleanupInteractionSystem : ICleanupSystem
  {
    private readonly IGroup<GameEntity> _interactions;
    private readonly List<GameEntity> _buffer = new(1);

    public CleanupInteractionSystem(GameContext game)
    {
      _interactions = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Interaction,
          GameMatcher.CompleteInteraction
        ));
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

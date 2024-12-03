using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class CleanupInitializeSoundSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;
    private readonly List<GameEntity> _buffer = new(32);

    public CleanupInitializeSoundSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.InitializeSound));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds.GetEntities(_buffer))
      {
        sound.isInitializeSound = false;
      }
    }
  }
}

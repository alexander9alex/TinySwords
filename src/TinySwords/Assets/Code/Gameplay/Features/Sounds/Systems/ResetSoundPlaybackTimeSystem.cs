using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class ResetSoundPlaybackTimeSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;
    private readonly List<GameEntity> _buffer = new(16);

    public ResetSoundPlaybackTimeSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.ResetSoundPlaybackTimeRequest)
        .NoneOf(GameMatcher.InitializeSound, GameMatcher.Delay));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds.GetEntities(_buffer))
      {
        sound.AudioSource.time = 0;

        sound.isResetSoundPlaybackTimeRequest = false;
      }
    }
  }
}

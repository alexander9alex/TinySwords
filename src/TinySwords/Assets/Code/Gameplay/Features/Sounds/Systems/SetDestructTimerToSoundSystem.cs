using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class SetDestructTimerToSoundSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;
    private readonly List<GameEntity> _buffer = new(16);

    public SetDestructTimerToSoundSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.PlaySoundRequest, GameMatcher.DestroyAfterPlayback)
        .NoneOf(GameMatcher.InitializeSound, GameMatcher.Delay));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds.GetEntities(_buffer))
      {
        sound.AddSelfDestructTimer(sound.AudioSource.clip.length);
      }
    }
  }
}

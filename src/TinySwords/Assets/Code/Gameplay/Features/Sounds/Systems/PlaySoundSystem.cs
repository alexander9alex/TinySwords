using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class PlaySoundSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;
    private readonly List<GameEntity> _buffer = new(16);

    public PlaySoundSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.PlaySoundRequest)
        .NoneOf(GameMatcher.InitializeSound));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds.GetEntities(_buffer))
      {
        sound.AudioSource.Play();
        sound.AddSelfDestructTimer(sound.AudioSource.clip.length);

        sound.isPlaySoundRequest = false;
      }
    }
  }
}

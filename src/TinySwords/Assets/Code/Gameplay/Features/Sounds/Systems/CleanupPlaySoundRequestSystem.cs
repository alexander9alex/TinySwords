using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class CleanupPlaySoundRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;
    private readonly List<GameEntity> _buffer = new(32);

    public CleanupPlaySoundRequestSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.PlaySoundRequest)
        .NoneOf(GameMatcher.InitializeSound, GameMatcher.Delay));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds.GetEntities(_buffer))
      {
        sound.isPlaySoundRequest = false;
      }
    }
  }
}

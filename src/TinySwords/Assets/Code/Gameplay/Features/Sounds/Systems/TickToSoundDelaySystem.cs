using System.Collections.Generic;
using Code.Gameplay.Services;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class TickToSoundDelaySystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IGroup<GameEntity> _sounds;
    private readonly List<GameEntity> _buffer = new(64);

    public TickToSoundDelaySystem(GameContext game, ITimeService time)
    {
      _time = time;
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.Delay, GameMatcher.PlaySoundRequest));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds.GetEntities(_buffer))
      {
        if (sound.Delay > 0)
          sound.ReplaceDelay(sound.Delay - _time.DeltaTime);
        else
          sound.RemoveDelay();
      }
    }
  }
}

using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class ResetSoundPlaybackTimeByIdSystem : IExecuteSystem
  {
    private readonly GameContext _game;

    private readonly IGroup<GameEntity> _changeSoundRequests;
    private readonly List<GameEntity> _buffer = new(16);

    public ResetSoundPlaybackTimeByIdSystem(GameContext game)
    {
      _game = game;
      _changeSoundRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ChangeSoundRequest, GameMatcher.ResetSoundPlaybackTimeRequest, GameMatcher.TargetId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _changeSoundRequests.GetEntities(_buffer))
      {
        GameEntity sound = _game.GetEntityWithId(request.TargetId);

        if (sound is { hasAudioSource: true })
          sound.isResetSoundPlaybackTimeRequest = true;

        request.isDestructed = true;
      }
    }
  }
}

using System.Collections.Generic;
using Code.Gameplay.Features.Sounds.Factory;
using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class CreateSoundInPositionSystem : IExecuteSystem
  {
    private readonly ISoundFactory _soundFactory;
    
    private readonly IGroup<GameEntity> _createSoundRequests;
    private readonly List<GameEntity> _buffer = new(16);

    public CreateSoundInPositionSystem(GameContext game, ISoundFactory soundFactory)
    {
      _soundFactory = soundFactory;
      
      _createSoundRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateSound, GameMatcher.SoundId, GameMatcher.WorldPosition));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createSoundRequests.GetEntities(_buffer))
      {
        _soundFactory.CreateSound(request.SoundId, request.WorldPosition);

        request.isDestructed = true;
      }
    }
  }
}

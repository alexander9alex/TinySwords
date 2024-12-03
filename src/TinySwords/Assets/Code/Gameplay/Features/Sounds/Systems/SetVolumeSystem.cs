using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class SetVolumeSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;

    public SetVolumeSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.Volume, GameMatcher.InitializeSound));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds)
      {
        sound.AudioSource.volume = sound.Volume;
      }
    }
  }
}

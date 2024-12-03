using Entitas;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class SetAudioClipSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;

    public SetAudioClipSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.AudioClip, GameMatcher.InitializeSound));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds)
      {
        sound.AudioSource.clip = sound.AudioClip;
      }
    }
  }
}

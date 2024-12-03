using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Systems
{
  public class SetRandomPitchSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _sounds;

    public SetRandomPitchSystem(GameContext game)
    {
      _sounds = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.AudioSource, GameMatcher.MinPitch, GameMatcher.MaxPitch, GameMatcher.InitializeSound));
    }

    public void Execute()
    {
      foreach (GameEntity sound in _sounds)
      {
        sound.AudioSource.pitch = RandomPitch(sound);
      }
    }

    private float RandomPitch(GameEntity sound) =>
      Random.Range(MinPitch(sound), MaxPitch(sound));

    private static float MinPitch(GameEntity sound) =>
      Mathf.Min(sound.MinPitch, sound.MaxPitch);

    private static float MaxPitch(GameEntity sound) =>
      Mathf.Max(sound.MinPitch, sound.MaxPitch);
  }
}

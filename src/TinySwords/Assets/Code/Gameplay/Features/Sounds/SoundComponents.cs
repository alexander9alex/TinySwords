using Code.Gameplay.Features.Sounds.Data;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds
{
  [Game] public class CreateSound : IComponent { }
  [Game] public class SoundIdComponent : IComponent { public SoundId Value; }
  [Game] public class AudioSourceComponent : IComponent { public AudioSource Value; }
  [Game] public class MinPitch : IComponent { public float Value; }
  [Game] public class MaxPitch : IComponent { public float Value; }
  [Game] public class PlayRequest : IComponent { }
}

﻿using Code.Gameplay.Features.Sounds.Data;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds
{
  [Game] public class CreateSound : IComponent { }
  [Game] public class SoundIdComponent : IComponent { public SoundId Value; }
  [Game] public class AudioSourceComponent : IComponent { public AudioSource Value; }
  [Game] public class AudioClipComponent : IComponent { public AudioClip Value; }
  [Game] public class Volume : IComponent { public float Value; }
  [Game] public class MinPitch : IComponent { public float Value; }
  [Game] public class MaxPitch : IComponent { public float Value; }
  [Game] public class PlaySoundRequest : IComponent { }
  [Game] public class ResetSoundPlaybackTimeRequest : IComponent { }
  [Game] public class PauseSoundRequest : IComponent { }
  [Game] public class ChangeSoundRequest : IComponent { }
  [Game] public class InitializeSound : IComponent { }
  [Game] public class DestroyAfterPlayback : IComponent { }
  [Game] public class Delay : IComponent { public float Value; }
}

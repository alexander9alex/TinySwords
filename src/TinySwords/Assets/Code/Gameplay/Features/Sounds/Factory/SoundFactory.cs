using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Sounds.Configs;
using Code.Gameplay.Features.Sounds.Data;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Sounds.Factory
{
  public class SoundFactory : ISoundFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly IIdentifierService _identifiers;
    private readonly ICameraProvider _cameraProvider;
    private readonly IInstantiator _instantiator;

    public SoundFactory(IStaticDataService staticData, IIdentifierService identifiers, ICameraProvider cameraProvider, IInstantiator instantiator)
    {
      _staticData = staticData;
      _identifiers = identifiers;
      _cameraProvider = cameraProvider;
      _instantiator = instantiator;
    }

    public void CreateSound(SoundId soundId) =>
      CreateSound(soundId, Vector2.zero);

    public void CreateSound(SoundId soundId, Vector2 pos)
    {
      SoundConfig config = _staticData.GetSoundConfig(soundId);

      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(config.SoundPrefab)
        .AddWorldPosition(PositionRelativeCamera(pos))
        .AddAudioClip(config.Clip)
        .AddVolume(config.Volume)
        
        .With(x => x.AddMinPitch(config.MinPitch), when: config.MinPitch != 0)
        .With(x => x.AddMaxPitch(config.MaxPitch), when: config.MaxPitch != 0)
        
        .With(x => x.AddDelay(config.Delay))
        .With(x => x.isInitializationRequest = true)
        .With(x => x.isInitializeSound = true)
        ;
    }

    public void CreateSoundDirectly(SoundId soundId)
    {
      SoundConfig config = _staticData.GetSoundConfig(soundId);

      AudioSource sound = _instantiator.InstantiatePrefabForComponent<AudioSource>(config.SoundPrefab);
      
      sound.clip = config.Clip;
      sound.volume = config.Volume;

      sound.Play();
      
      Object.Destroy(sound.gameObject, sound.clip.length);
    }

    private Vector3 PositionRelativeCamera(Vector2 pos) =>
      pos.SetZ(_cameraProvider.MainCamera.transform.position.z);
  }
}

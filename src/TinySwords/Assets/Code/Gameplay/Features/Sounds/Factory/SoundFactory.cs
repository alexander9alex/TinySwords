using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Common.Services;
using Code.Gameplay.Features.Sounds.Configs;
using Code.Gameplay.Features.Sounds.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Factory
{
  class SoundFactory : ISoundFactory
  {
    private readonly IStaticDataService _staticData;
    private readonly IIdentifierService _identifiers;
    private readonly ICameraProvider _cameraProvider;

    public SoundFactory(IStaticDataService staticData, IIdentifierService identifiers, ICameraProvider cameraProvider)
    {
      _staticData = staticData;
      _identifiers = identifiers;
      _cameraProvider = cameraProvider;
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
        .With(x => x.isInitializationRequest = true)
        .With(x => x.isPlayRequest = true)
        .With(x => x.AddMinPitch(config.MinPitch), when: config.MinPitch != 0)
        .With(x => x.AddMaxPitch(config.MaxPitch), when: config.MaxPitch != 0)
        ;
    }

    private Vector3 PositionRelativeCamera(Vector2 pos) =>
      pos.SetZ(_cameraProvider.MainCamera.transform.position.z);
  }
}

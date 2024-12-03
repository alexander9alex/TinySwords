using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Identifiers;
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

    public SoundFactory(IStaticDataService staticData, IIdentifierService identifiers)
    {
      _staticData = staticData;
      _identifiers = identifiers;
    }

    public void CreateSound(SoundId soundId) =>
      CreateSound(soundId, Vector2.zero);

    public void CreateSound(SoundId soundId, Vector2 pos)
    {
      SoundConfig config = _staticData.GetSoundConfig(soundId);

      CreateEntity.Empty()
        .AddId(_identifiers.Next())
        .AddViewPrefab(config.SoundPrefab)
        .AddWorldPosition(pos)
        .With(x => x.isInitializationRequest = true)
        .With(x => x.isPlayRequest = true);
    }
  }
}

using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Factory;
using Code.Gameplay.Features.Units.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Sounds.Services
{
  class SoundService : ISoundService
  {
    private readonly ISoundFactory _soundFactory;

    public SoundService(ISoundFactory soundFactory) =>
      _soundFactory = soundFactory;

    public void PlayTakingDamageSound(GameEntity entity)
    {
      if (!entity.hasUnitTypeId && !entity.hasWorldPosition)
        return;

      GameEntity createSoundRequest = CreateEntity.Empty()
        .AddWorldPosition(entity.WorldPosition)
        .With(x => x.isCreateSound = true);

      switch (entity.UnitTypeId)
      {
        case UnitTypeId.Knight:
          createSoundRequest
            .AddSoundId(SoundId.KnightTakeDamage);
          break;
        case UnitTypeId.TorchGoblin:
          createSoundRequest
            .AddSoundId(SoundId.TorchGoblinTakeDamage);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void PlayMakeDamageSound(GameEntity entity)
    {
      if (!entity.hasUnitTypeId && !entity.hasWorldPosition)
        return;

      GameEntity createSoundRequest = CreateEntity.Empty()
        .AddWorldPosition(entity.WorldPosition)
        .With(x => x.isCreateSound = true);

      switch (entity.UnitTypeId)
      {
        case UnitTypeId.Knight:
          createSoundRequest
            .AddSoundId(SoundId.KnightMakeHit);
          break;
        case UnitTypeId.TorchGoblin:
          createSoundRequest
            .AddSoundId(SoundId.TorchGoblinMakeHit);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void PlaySound(SoundId soundId) =>
      PlaySound(soundId, Vector3.zero);

    public void PlaySound(SoundId soundId, Vector3 pos)
    {
      CreateEntity.Empty()
        .AddSoundId(soundId)
        .AddWorldPosition(pos)
        .With(x => x.isCreateSound = true);
    }

    public void PlaySoundDirectly(SoundId soundId) =>
      _soundFactory.CreateSoundDirectly(soundId);
  }
}

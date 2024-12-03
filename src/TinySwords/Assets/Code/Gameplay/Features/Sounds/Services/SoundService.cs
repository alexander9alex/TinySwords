using System;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Units.Data;

namespace Code.Gameplay.Features.Sounds.Services
{
  class SoundService : ISoundService
  {
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
  }
}

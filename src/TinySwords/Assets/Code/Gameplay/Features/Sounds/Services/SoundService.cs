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
      if (!entity.hasUnitTypeId)
        return;

      switch (entity.UnitTypeId)
      {
        case UnitTypeId.Knight:
          CreateEntity.Empty()
            .With(x => x.isCreateSound = true)
            .AddSoundId(SoundId.KnightTakeDamage);
          break;
        case UnitTypeId.TorchGoblin:
          CreateEntity.Empty()
            .With(x => x.isCreateSound = true)
            .AddSoundId(SoundId.TorchGoblinTakeDamage);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}

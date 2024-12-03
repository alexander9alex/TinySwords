using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Code.Gameplay.Constants;
using Code.Gameplay.Features.Effects.Data;
using Code.Gameplay.Features.Sounds.Services;
using Entitas;
using ModestTree;

namespace Code.Gameplay.Features.Battle.Systems
{
  public class MakeHitSystem : IExecuteSystem
  {
    private readonly IPhysicsService _physicsService;
    private readonly GameContext _game;
    private readonly ISoundService _soundService;

    private readonly IGroup<GameEntity> _makeHitRequests;
    private readonly List<GameEntity> _buffer = new(16);

    public MakeHitSystem(GameContext game, IPhysicsService physicsService, ISoundService soundService)
    {
      _game = game;
      _physicsService = physicsService;
      _soundService = soundService;

      _makeHitRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.MakeHit, GameMatcher.CasterId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _makeHitRequests.GetEntities(_buffer))
      {
        GameEntity caster = _game.GetEntityWithId(request.CasterId);
        MakeHit(caster);
        
        request.isDestructed = true;
      }
    }

    private void MakeHit(GameEntity caster)
    {
      if (CasterNotValid(caster))
        return;

      List<int> targets = GetTargets(caster);

      if (targets.IsEmpty())
        return;

      if (targets.Contains(caster.TargetId))
        MakeHit(caster, caster.TargetId);
      else
        MakeHit(caster, targets.First());
    }

    private void MakeHit(GameEntity caster, int targetId)
    {
      CreateEntity.Empty()
        .AddTargetId(targetId)
        .AddEffectTypeId(EffectTypeId.Damage)
        .AddEffectValue(caster.Damage)
        .With(x => x.isDamageEffect = true);

      _soundService.PlayMakeDamageSound(caster);
    }

    private List<int> GetTargets(GameEntity unit)
    {
      return _physicsService.CircleCast(
          unit.WorldPosition.ToVector2() + unit.LookDirection * unit.AttackReach,
          unit.AttackReach / 2,
          GameConstants.UnitsAndBuildingsLayerMask)
        .Select(entity => entity.Id)
        .ToList();
    }

    private static bool CasterNotValid(GameEntity caster) =>
      !caster.hasWorldPosition || !caster.hasLookDirection || !caster.hasAttackReach || !caster.hasDamage;
  }
}

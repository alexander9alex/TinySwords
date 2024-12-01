using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Common.Providers;
using Code.Gameplay.Features.MoveIndicator.Factory;
using Entitas;

namespace Code.Gameplay.Features.AttackIndicator.Systems
{
  public class CreateAttackIndicatorSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IIndicatorFactory _indicatorFactory;

    private readonly IGroup<GameEntity> _createAttackIndicatorRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateAttackIndicatorSystem(GameContext game, ICameraProvider cameraProvider, IIndicatorFactory indicatorFactory)
    {
      _cameraProvider = cameraProvider;
      _indicatorFactory = indicatorFactory;

      _createAttackIndicatorRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateAttackIndicator, GameMatcher.WorldPosition, GameMatcher.TargetId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _createAttackIndicatorRequests.GetEntities(_buffer))
      {
        GameEntity attackIndicator = _indicatorFactory.CreateAttackIndicator(request.WorldPosition);

        attackIndicator
          .AddTargetId(request.TargetId)
          .With(x => x.isFollowToTarget = true)
          .With(x => x.isCreatedNow = true);
        
        CreateEntity.Empty()
          .With(x => x.isDestructOldAttackIndicatorRequest = true);

        request.isDestructed = true;
      }
    }
  }
}

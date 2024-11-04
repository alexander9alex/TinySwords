using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Services;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Factory
{
  class MoveClickIndicatorFactory : IMoveClickIndicatorFactory
  {
    private readonly IStaticDataService _staticData;

    public MoveClickIndicatorFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public GameEntity CreateMoveIndicator(Vector3 pos)
    {
      EntityBehaviour moveIndicatorPrefab = _staticData.GetMoveIndicatorPrefab();

      return CreateEntity.Empty()
        .AddViewPrefab(moveIndicatorPrefab)
        .AddWorldPosition(pos.RemoveZ())
        .With(x => x.isMoveClickIndicator = true)
        .With(x => x.isUpdatePositionAfterSpawning = true);
    }
  }
}

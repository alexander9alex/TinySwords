using System.Collections.Generic;
using System.Linq;
using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Constants;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.MoveInput.Systems
{
  public class ConvertEndDestinationSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _convertEndDestinationRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public ConvertEndDestinationSystem(GameContext game)
    {
      _convertEndDestinationRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ChangeEndDestinationRequest, GameMatcher.ConvertWhenGroup, GameMatcher.WorldPosition));

      _selected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected, GameMatcher.Movable, GameMatcher.Alive, GameMatcher.Id));
    }

    public void Execute()
    {
      foreach (GameEntity request in _convertEndDestinationRequests.GetEntities(_buffer))
      {
        if (_selected.count == 0)
          return;
        
        List<Vector2> destinations = GetDestinations(request.WorldPosition, _selected.count);

        foreach (GameEntity selected in _selected)
        {
          CreateEntity.Empty()
            .AddTargetId(selected.Id)
            .AddWorldPosition(destinations.First())
            .With(x => x.isChangeEndDestinationRequest = true);
          
          destinations.RemoveAt(0);
        }

        request.isProcessed = true;
      }
    }

    private List<Vector2> GetDestinations(Vector3 pos, int countOfSelected)
    {
      float sqrt = Mathf.Ceil(Mathf.Sqrt(countOfSelected));
      Vector3 leftUpClickWorldPos = GetLeftUpClickWorldPos(pos, sqrt);

      List<Vector2> destinations = new();

      for (int y = 0; y < sqrt; y++)
      for (int x = 0; x < sqrt; x++)
      {
        destinations.Add(leftUpClickWorldPos + new Vector3(x, -y) * GameConstants.UnitMinRadius);
      }

      return destinations;
    }

    private static Vector3 GetLeftUpClickWorldPos(Vector3 clickWorldPos, float sqrt)
    {
      if (sqrt % 2 == 0)
        return GetLeftUpClickWorldPos(clickWorldPos, sqrt + 1) + new Vector3(0.5f, -0.5f) * GameConstants.UnitMinRadius;

      int offsetCoef = (int)(sqrt / 2);
      return clickWorldPos + new Vector3(-offsetCoef, offsetCoef, 0) * GameConstants.UnitMinRadius;
    }
  }
}

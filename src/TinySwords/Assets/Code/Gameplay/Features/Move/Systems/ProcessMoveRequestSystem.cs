using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Providers;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Move.Systems
{
  public class ProcessMoveRequestSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;

    private readonly IGroup<GameEntity> _moveRequests;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public ProcessMoveRequestSystem(GameContext game, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;

      _moveRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.MoveRequest, GameMatcher.PositionOnScreen)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected));
    }

    public void Execute()
    {
      foreach (GameEntity request in _moveRequests.GetEntities(_buffer))
      {
        if (_selected.count == 0)
          return;

        List<Vector2> destinations = GetDestinations(request, _selected.count);

        foreach (GameEntity selected in _selected)
        {
          selected.ReplaceDestination(destinations.First());
          destinations.RemoveAt(0);
        }

        request.isProcessed = true;
      }
    }

    private List<Vector2> GetDestinations(GameEntity click, int countOfSelected)
    {
      Vector3 clickWorldPos = _cameraProvider.MainCamera.ScreenToWorldPoint(click.PositionOnScreen);

      float sqrt = Mathf.Ceil(Mathf.Sqrt(countOfSelected));
      Vector3 leftUpClickWorldPos = GetLeftUpClickWorldPos(clickWorldPos, sqrt);

      List<Vector2> destinations = new();

      for (int y = 0; y < sqrt; y++)
      for (int x = 0; x < sqrt; x++)
      {
        destinations.Add(leftUpClickWorldPos + new Vector3(x, -y));
      }

      return destinations;
    }

    private static Vector3 GetLeftUpClickWorldPos(Vector3 clickWorldPos, float sqrt)
    {
      if (sqrt % 2 == 0)
        return GetLeftUpClickWorldPos(clickWorldPos, sqrt + 1) + new Vector3(0.5f, -0.5f);

      int offsetCoef = (int)(sqrt / 2);
      return clickWorldPos + new Vector3(-offsetCoef, offsetCoef, 0);
    }
  }
}

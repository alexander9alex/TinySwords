using System.Collections.Generic;
using Code.Gameplay.Common.Providers;
using Entitas;

namespace Code.Gameplay.Features.Indicators.Systems
{
  public class ConvertPositionOnCreateIndicatorRequestSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IGroup<GameEntity> _changeEndDestinationRequests;
    private readonly List<GameEntity> _buffer = new(1);

    public ConvertPositionOnCreateIndicatorRequestSystem(GameContext game, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
      _changeEndDestinationRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.CreateIndicator, GameMatcher.PositionOnScreen));
    }

    public void Execute()
    {
      foreach (GameEntity request in _changeEndDestinationRequests.GetEntities(_buffer))
      {
        request.ReplaceWorldPosition(_cameraProvider.MainCamera.ScreenToWorldPoint(request.PositionOnScreen));
        request.RemovePositionOnScreen();
      }
    }
  }
}

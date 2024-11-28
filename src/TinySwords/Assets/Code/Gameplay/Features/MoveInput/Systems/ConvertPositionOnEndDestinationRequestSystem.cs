using System.Collections.Generic;
using Code.Gameplay.Common.Providers;
using Entitas;

namespace Code.Gameplay.Features.MoveInput.Systems
{
  public class ConvertPositionOnEndDestinationRequestSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly IGroup<GameEntity> _changeEndDestinationRequests;
    private readonly List<GameEntity> _buffer = new(32);

    public ConvertPositionOnEndDestinationRequestSystem(GameContext game, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
      _changeEndDestinationRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ChangeEndDestinationRequest, GameMatcher.PositionOnScreen));
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

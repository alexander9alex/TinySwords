using System.Collections.Generic;
using Code.Common.Entities;
using Code.Common.Extensions;
using Entitas;

namespace Code.Gameplay.Features.Input.Systems
{
  public class CreateFocusRequestSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _mousePositions;
    private readonly List<GameEntity> _buffer = new(1);

    public CreateFocusRequestSystem(GameContext game)
    {
      _mousePositions = game.GetGroup(GameMatcher.MousePositionOnScreen);
    }

    public void Execute()
    {
      foreach (GameEntity mousePos in _mousePositions.GetEntities(_buffer))
      {
        CreateEntity.Empty()
          .AddPositionOnScreen(mousePos.MousePositionOnScreen)
          .With(x => x.isFocusRequest = true);
      }
    }
  }
}

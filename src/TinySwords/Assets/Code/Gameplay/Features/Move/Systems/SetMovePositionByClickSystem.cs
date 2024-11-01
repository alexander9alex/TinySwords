using System.Collections.Generic;
using Code.Gameplay.Common.Providers;
using Entitas;

namespace Code.Gameplay.Features.Move.Systems
{
  public class SetMovePositionByClickSystem : IExecuteSystem
  {
    private readonly ICameraProvider _cameraProvider;
    
    private readonly IGroup<GameEntity> _rightClicks;
    private readonly IGroup<GameEntity> _selected;
    private readonly List<GameEntity> _buffer = new(1);

    public SetMovePositionByClickSystem(GameContext game, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
      _rightClicks = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.RightClick, GameMatcher.MousePosition)
        .NoneOf(GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected, GameMatcher.NavMeshAgent));
    }

    public void Execute()
    {
      foreach (GameEntity click in _rightClicks.GetEntities(_buffer))
      foreach (GameEntity selected in _selected)
      {
        selected.NavMeshAgent.SetDestination(_cameraProvider.MainCamera.ScreenToWorldPoint(click.MousePosition));
        
        click.isProcessed = true;
      }
    }
  }
}

using Entitas;

namespace Code.Gameplay.Features.MoveInput.Systems
{
  public class ChangeEndDestinationAtAllSelectedSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _changeEndDestinationRequests;
    private readonly IGroup<GameEntity> _selected;

    public ChangeEndDestinationAtAllSelectedSystem(GameContext game)
    {
      _changeEndDestinationRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ChangeEndDestinationRequest, GameMatcher.WorldPosition)
        .NoneOf(GameMatcher.TargetId, GameMatcher.Processed));

      _selected = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Selected, GameMatcher.Movable, GameMatcher.Alive));
    }

    public void Execute()
    {
      foreach (GameEntity request in _changeEndDestinationRequests)
      foreach (GameEntity selected in _selected)
      {
        selected.ReplaceEndDestination(request.WorldPosition);
        selected.ReplaceMakeDecisionTimer(0);
      }
    }
  }
}

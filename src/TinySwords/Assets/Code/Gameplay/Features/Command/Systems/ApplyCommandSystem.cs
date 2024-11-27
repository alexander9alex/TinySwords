using Code.Common.Entities;
using Code.Common.Extensions;
using Code.Gameplay.Features.Command.Data;
using Entitas;

namespace Code.Gameplay.Features.Command.Systems
{
  public class ApplyCommandSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _applyCommandRequests;
    private readonly IGroup<GameEntity> _selectedCommands;

    public ApplyCommandSystem(GameContext game)
    {
      _applyCommandRequests = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ApplyCommand, GameMatcher.PositionOnScreen));
      
      _selectedCommands = game.GetGroup(GameMatcher.AllOf(GameMatcher.SelectedCommand, GameMatcher.CommandTypeId));
    }

    public void Execute()
    {
      foreach (GameEntity request in _applyCommandRequests)
      foreach (GameEntity command in _selectedCommands)
      {
        CreateEntity.Empty()
          .AddCommandTypeId(command.CommandTypeId)
          .AddPositionOnScreen(request.PositionOnScreen)
          .With(x => x.isUpdateCommand = true);
        
        CreateEntity.Empty()
          .With(x => x.isCancelCommand = true);
      }
    }
  }
}

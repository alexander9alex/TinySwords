namespace Code.Gameplay.Features.Command.Services
{
  public interface ISelectableCommandService
  {
    bool CommandCompleted(GameEntity selectable);
    void RemoveCommand(GameEntity selectable);
  }
}

namespace Code.Gameplay.Features.Command.Services
{
  public interface ICommandService
  {
    void SelectCommand(GameEntity command);
    void CancelCommand(GameEntity command);
    bool CanApplyCommand(GameEntity command, GameEntity request);
    void ApplyCommand(GameEntity command, GameEntity request);
    void IncorrectCommand(GameEntity command, GameEntity request);
    bool CanProcessAimedAttack(out GameEntity target, GameEntity request);
    void ProcessAimedAttack(GameEntity request, GameEntity selected, GameEntity target);
  }
}

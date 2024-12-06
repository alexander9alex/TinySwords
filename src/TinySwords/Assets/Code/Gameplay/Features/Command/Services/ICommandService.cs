using Code.Gameplay.Features.Command.Data;

namespace Code.Gameplay.Features.Command.Services
{
  public interface ICommandService
  {
    void SelectCommand(CommandTypeId command);
    void CancelCommand(bool isCommandProcessed);
    bool CanApplyCommand(CommandTypeId command, GameEntity request);
    void ApplyCommand(CommandTypeId command, GameEntity request);
    void ProcessIncorrectCommand(CommandTypeId command, GameEntity request);
    bool CanProcessAimedAttack(out GameEntity target, GameEntity request);
    void ProcessAimedAttack(GameEntity request, GameEntity selected, GameEntity target);
    void ProcessIncorrectAimedAttack(GameEntity request);
    void CreateProcessCommandRequest(CommandTypeId command, GameEntity request);
  }
}

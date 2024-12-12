using Code.Gameplay.Features.Command.Data;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Command.Services
{
  public interface ICommandService
  {
    void SelectCommand(CommandTypeId command);
    void CancelCommand(bool isCommandProcessed);
    bool CanApplyCommand(CommandTypeId command, Vector2 screenPos);
    void ApplyCommand(CommandTypeId command, Vector2 screenPos);
    void ProcessIncorrectCommand(CommandTypeId command, GameEntity request);
    bool CanProcessAimedAttack(out GameEntity target, Vector2 screenPos);
    void ProcessIncorrectAimedAttack(Vector2 screenPos);
    void ProcessMoveCommand(GameEntity request, IGroup<GameEntity> selected);
    void ProcessMoveWithAttackCommand(GameEntity request, IGroup<GameEntity> selected);
    void ProcessAimedAttack(GameEntity request, IGroup<GameEntity> selected);
  }
}

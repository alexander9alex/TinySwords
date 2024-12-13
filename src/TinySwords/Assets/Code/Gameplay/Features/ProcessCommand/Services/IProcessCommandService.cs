using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.ProcessCommand.Services
{
  public interface IProcessCommandService
  {
    void ProcessMoveCommand(GameEntity request, IGroup<GameEntity> selected);
    void ProcessMoveWithAttackCommand(GameEntity request, IGroup<GameEntity> selected);
    void ProcessAimedAttack(GameEntity request, IGroup<GameEntity> selected);
    void ProcessIncorrectAimedAttack(Vector2 screenPos);
    bool CanProcessAimedAttack(out GameEntity target, Vector2 screenPos);
  }
}

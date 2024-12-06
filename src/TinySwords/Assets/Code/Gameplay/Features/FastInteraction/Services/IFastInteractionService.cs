namespace Code.Gameplay.Features.FastInteraction.Services
{
  public interface IFastInteractionService
  {
    void MoveSelected(GameEntity request);
    void MakeAimedAttack(GameEntity request);
    bool HasNotTargetInPosition(GameEntity request);
  }
}

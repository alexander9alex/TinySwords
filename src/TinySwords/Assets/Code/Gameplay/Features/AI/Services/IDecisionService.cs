namespace Code.Gameplay.Features.AI.Services
{
  public interface IDecisionService
  {
    void ProcessUnitDecision(GameEntity unit);
    void RemoveUnitDecisions(GameEntity unit);
  }
}

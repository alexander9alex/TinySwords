namespace Code.Gameplay.Features.Battle.Services
{
  public interface IAttackAnimationService
  {
    void UnitMakeHit(int unitId);
    void UnitFinishedAttack(int unitId);
    void UnitMakeHitWithDelay(int unitId, float delay);
    void UnitFinishedAttack(int unitId, float delay);
  }
}
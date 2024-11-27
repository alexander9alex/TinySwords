namespace Code.Gameplay.Features.Battle.Services
{
  public interface IAttackAnimationService
  {
    void UnitMakeHit(int unitId);
    void UnitFinishedAttack(int unitId);
  }
}
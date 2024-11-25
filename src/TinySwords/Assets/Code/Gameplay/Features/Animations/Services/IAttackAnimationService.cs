namespace Code.Gameplay.Features.Animations.Services
{
  public interface IAttackAnimationService
  {
    void UnitMakeHit(int unitId);
    void UnitFinishedAttack(int unitId);
  }
}
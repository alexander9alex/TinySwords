namespace Code.Gameplay.Features.Units.Animations.Services
{
  public interface IAttackAnimationService
  {
    void UnitMakeHit(int unitId);
    void UnitFinishedAttack(int unitId);
  }
}
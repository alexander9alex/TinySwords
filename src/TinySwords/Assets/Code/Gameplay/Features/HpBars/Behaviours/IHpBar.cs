namespace Code.Gameplay.Features.HpBars.Behaviours
{
  public interface IHpBar
  {
    void Show();
    void Hide();
    void UpdateHp(float currentHp, float maxHp);
  }
}

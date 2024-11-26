using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.HpBars.Behaviours
{
  public class HpBar : MonoBehaviour, IHpBar
  {
    public Slider HpBarSlider;

    public void Show() =>
      HpBarSlider.gameObject.SetActive(true);

    public void Hide() =>
      HpBarSlider.gameObject.SetActive(false);

    public void UpdateHp(float currentHp, float maxHp) =>
      HpBarSlider.value = currentHp / maxHp;
  }
}

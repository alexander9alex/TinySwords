using Code.UI.Buttons.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Hud.Factory
{
  public interface IHudFactory
  {
    Button CreateControlButton(ControlButtonConfig controlButtonConfig, RectTransform parent);
  }
}

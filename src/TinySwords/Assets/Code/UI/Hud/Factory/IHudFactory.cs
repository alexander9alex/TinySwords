using Code.Gameplay.Features.ControlAction.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Hud.Factory
{
  public interface IHudFactory
  {
    Button CreateControlButton(UnitActionUIConfig unitActionUIConfig, RectTransform parent);
    GameObject CreateActionDescription(GameObject prefab, RectTransform parent);
  }
}

using Code.Gameplay.Features.Command.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Hud.Factory
{
  public interface IHudFactory
  {
    Button CreateControlButton(CommandUIConfig commandUIConfig, RectTransform parent);
    GameObject CreateActionDescription(GameObject prefab, RectTransform parent);
  }
}

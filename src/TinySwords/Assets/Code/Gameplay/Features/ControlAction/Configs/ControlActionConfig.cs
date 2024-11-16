using Code.UI.Buttons.Data;
using UnityEngine;

namespace Code.Gameplay.Features.ControlAction.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Action Config", fileName = "ControlActionConfig", order = 0)]
  public class ControlActionConfig : ScriptableObject
  {
    public ControlActionTypeId ControlActionTypeId;
    public GameObject DescriptionPrefab;
  }
}
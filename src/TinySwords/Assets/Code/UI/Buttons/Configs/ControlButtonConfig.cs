using Code.UI.Buttons.Data;
using UnityEngine;

namespace Code.UI.Buttons.Configs
{
  [CreateAssetMenu(menuName = "Static Data/UI/Control Button Config", fileName = "ControlButtonConfig", order = 0)]
  public class ControlButtonConfig : ScriptableObject
  {
    public GameObject ControlButtonPrefab;
    public ActionTypeId ActionTypeId;
  }
}
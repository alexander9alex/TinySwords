using Code.UI.Buttons.Data;
using UnityEngine;

namespace Code.Gameplay.Features.ParseAction.Configs
{
  [CreateAssetMenu(menuName = "Static Data/Action Config", fileName = "ActionConfig", order = 0)]
  public class ActionConfig : ScriptableObject
  {
    public ActionTypeId ActionTypeId;
    public GameObject DescriptionPrefab;
  }
}
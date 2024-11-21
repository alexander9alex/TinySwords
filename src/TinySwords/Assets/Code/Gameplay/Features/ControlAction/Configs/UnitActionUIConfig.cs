using Code.Gameplay.Features.ControlAction.Data;
using UnityEngine;

namespace Code.Gameplay.Features.ControlAction.Configs
{
  [CreateAssetMenu(menuName = "Static Data/UI/Unit Action UI Config", fileName = "UnitActionUIConfig", order = 0)]
  public class UnitActionUIConfig : ScriptableObject
  {
    public UnitActionTypeId UnitActionTypeId;
    public GameObject UnitActionButtonPrefab;
    public GameObject UnitActionDescriptionPrefab;
  }
}
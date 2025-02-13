using Code.UI.Data;
using UnityEngine;

namespace Code.UI.Windows.Data
{
  [CreateAssetMenu(menuName = "Static Data/UI/Window Config", fileName = "WindowConfig", order = 0)]
  public class WindowConfig : ScriptableObject
  {
    public WindowId WindowId;
    public GameObject WindowPrefab;
  }
}

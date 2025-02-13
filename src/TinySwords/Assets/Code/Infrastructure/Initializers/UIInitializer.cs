using Code.UI.Windows.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Initializers
{
  public class UIInitializer : MonoBehaviour, IInitializable
  {
    public RectTransform UIParent;
    private IWindowFactory _windowFactory;

    [Inject]
    private void Construct(IWindowFactory windowFactory) =>
      _windowFactory = windowFactory;

    public void Initialize() =>
      _windowFactory.SetUIParent(UIParent);
  }
}

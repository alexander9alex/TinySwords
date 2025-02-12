using Code.UI.Data;
using UnityEngine;

namespace Code.UI.Windows
{
  public class BaseWindow : MonoBehaviour
  {
    public WindowId WindowId { get; protected set; }

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
    }

    private void OnDestroy() =>
      Cleanup();

    protected virtual void Initialize()
    {
    }

    protected virtual void SubscribeUpdates()
    {
    }

    protected virtual void UnsubscribeUpdates()
    {
      
    }

    protected virtual void Cleanup() =>
      UnsubscribeUpdates();
  }
}

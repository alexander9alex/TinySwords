using UnityEngine;

namespace Code.Common.Views
{
  public interface IEntityView
  {
    GameEntity Entity { get; }
    void SetEntity(GameEntity entity);
    void ReleaseEntity();
    GameObject gameObject { get; }
  }
}

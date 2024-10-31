using UnityEngine;

namespace Code.Infrastructure.Views
{
  public interface IEntityView
  {
    GameEntity Entity { get; }
    void SetEntity(GameEntity entity);
    void ReleaseEntity();
    GameObject gameObject { get; }
  }
}

using Code.Common.Views;
using UnityEngine;

namespace Code.Gameplay.Common.Registrars
{
  public abstract class EntityDependent : MonoBehaviour
  {
    public EntityBehaviour EntityBehaviour;
    public GameEntity Entity => EntityBehaviour.Entity;

    private void Awake()
    {
      if (EntityBehaviour == null)
        EntityBehaviour = GetComponent<EntityBehaviour>();
    }
  }
}

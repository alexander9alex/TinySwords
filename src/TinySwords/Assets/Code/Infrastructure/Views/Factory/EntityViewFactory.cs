using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Views.Factory
{
  public class EntityViewFactory : IEntityViewFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly Vector3 _farAway = new(9999, 9999, 9999);
    private GameObject EntityViewParent
    {
      get
      {
        if (_entityViewParent == null) 
          _entityViewParent = new GameObject("EntityViewParent");

        return _entityViewParent;
      }
    }
    private GameObject _entityViewParent;

    public EntityViewFactory(IInstantiator instantiator) =>
      _instantiator = instantiator;

    public EntityBehaviour CreateViewFromPrefab(GameEntity entity)
    {
      EntityBehaviour view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(entity.ViewPrefab, _farAway, Quaternion.identity, EntityViewParent.transform);

      view.SetEntity(entity);

      return view;
    }
  }
}

using Code.Infrastructure.Views.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Initializers
{
  public class EntityViewInitializer : MonoBehaviour, IInitializable
  {
    public Transform EntityViewParent;
    private IEntityViewFactory _entityViewFactory;

    [Inject]
    private void Construct(IEntityViewFactory entityViewFactory) =>
      _entityViewFactory = entityViewFactory;

    public void Initialize() =>
      _entityViewFactory.SetEntityViewParent(EntityViewParent);
  }
}

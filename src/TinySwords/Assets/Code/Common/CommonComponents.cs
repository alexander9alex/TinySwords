using Code.Infrastructure.Views;
using Entitas;

namespace Code.Common
{
  [Game] public class Destructed : IComponent { }
  [Game] public class View : IComponent { public IEntityView Value; }
  [Game] public class ViewPrefab : IComponent { public EntityBehaviour Value; }
  [Game] public class Processed : IComponent { }
  [Game] public class SelfDestructTimer : IComponent { public float Value; }
}

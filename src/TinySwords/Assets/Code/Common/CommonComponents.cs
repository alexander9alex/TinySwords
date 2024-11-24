using Code.Infrastructure.Views;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Common
{
  [Game] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
  [Game] public class Destructed : IComponent { }
  [Game] public class View : IComponent { public IEntityView Value; }
  [Game] public class ViewPrefab : IComponent { public EntityBehaviour Value; }
  [Game] public class Processed : IComponent { }
  [Game] public class SelfDestructTimer : IComponent { public float Value; }
  [Game] public class Available : IComponent { }
  [Game] public class Initialized : IComponent { }
  [Game] public class InitializationRequest : IComponent { }
}

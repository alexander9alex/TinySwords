using Code.Gameplay.Features.Build.Data;
using Entitas;

namespace Code.Gameplay.Features.Build
{
  [Game] public class Building : IComponent { }
  [Game] public class Castle : IComponent { }
  [Game] public class BuildTypeIdComponent : IComponent { public BuildingTypeId Value; }
}

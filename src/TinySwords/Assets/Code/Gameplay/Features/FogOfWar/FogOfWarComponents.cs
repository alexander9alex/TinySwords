using Code.Gameplay.Features.FogOfWar.Data;
using Entitas;

namespace Code.Gameplay.Features.FogOfWar
{
  [Game] public class Glowing : IComponent { }
  [Game] public class CreateFogOfWar : IComponent { }
  [Game] public class LevelParent : IComponent { }
  [Game] public class FogOfWarMarkerComponent : IComponent { public FogOfWarMarker Value; }
}

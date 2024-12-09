using Code.Gameplay.Features.Indicators.Data;
using Entitas;

namespace Code.Gameplay.Features.Indicators
{
  [Game] public class Indicator : IComponent { }
  [Game] public class CreatedNow : IComponent { }
  [Game] public class CreateIndicator : IComponent { }
  [Game] public class DestructOldIndicator : IComponent { }
  [Game] public class IndicatorTypeIdComponent : IComponent { public IndicatorTypeId Value; }
  [Game] public class TeleportationToTarget : IComponent { }
}

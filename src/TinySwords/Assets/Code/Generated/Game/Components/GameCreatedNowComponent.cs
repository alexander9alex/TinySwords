//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCreatedNow;

    public static Entitas.IMatcher<GameEntity> CreatedNow {
        get {
            if (_matcherCreatedNow == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CreatedNow);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCreatedNow = matcher;
            }

            return _matcherCreatedNow;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Code.Gameplay.Features.Indicators.CreatedNow createdNowComponent = new Code.Gameplay.Features.Indicators.CreatedNow();

    public bool isCreatedNow {
        get { return HasComponent(GameComponentsLookup.CreatedNow); }
        set {
            if (value != isCreatedNow) {
                var index = GameComponentsLookup.CreatedNow;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : createdNowComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

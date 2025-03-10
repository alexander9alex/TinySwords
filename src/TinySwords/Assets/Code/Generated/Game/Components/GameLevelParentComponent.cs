//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherLevelParent;

    public static Entitas.IMatcher<GameEntity> LevelParent {
        get {
            if (_matcherLevelParent == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LevelParent);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLevelParent = matcher;
            }

            return _matcherLevelParent;
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

    static readonly Code.Gameplay.Features.FogOfWar.LevelParent levelParentComponent = new Code.Gameplay.Features.FogOfWar.LevelParent();

    public bool isLevelParent {
        get { return HasComponent(GameComponentsLookup.LevelParent); }
        set {
            if (value != isLevelParent) {
                var index = GameComponentsLookup.LevelParent;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : levelParentComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

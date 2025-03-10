//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPauseSoundRequest;

    public static Entitas.IMatcher<GameEntity> PauseSoundRequest {
        get {
            if (_matcherPauseSoundRequest == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PauseSoundRequest);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPauseSoundRequest = matcher;
            }

            return _matcherPauseSoundRequest;
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

    static readonly Code.Gameplay.Features.Sounds.PauseSoundRequest pauseSoundRequestComponent = new Code.Gameplay.Features.Sounds.PauseSoundRequest();

    public bool isPauseSoundRequest {
        get { return HasComponent(GameComponentsLookup.PauseSoundRequest); }
        set {
            if (value != isPauseSoundRequest) {
                var index = GameComponentsLookup.PauseSoundRequest;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : pauseSoundRequestComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

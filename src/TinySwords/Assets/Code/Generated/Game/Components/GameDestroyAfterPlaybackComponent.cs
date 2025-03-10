//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherDestroyAfterPlayback;

    public static Entitas.IMatcher<GameEntity> DestroyAfterPlayback {
        get {
            if (_matcherDestroyAfterPlayback == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DestroyAfterPlayback);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDestroyAfterPlayback = matcher;
            }

            return _matcherDestroyAfterPlayback;
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

    static readonly Code.Gameplay.Features.Sounds.DestroyAfterPlayback destroyAfterPlaybackComponent = new Code.Gameplay.Features.Sounds.DestroyAfterPlayback();

    public bool isDestroyAfterPlayback {
        get { return HasComponent(GameComponentsLookup.DestroyAfterPlayback); }
        set {
            if (value != isDestroyAfterPlayback) {
                var index = GameComponentsLookup.DestroyAfterPlayback;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : destroyAfterPlaybackComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherNotAddedNavMeshRootSource;

    public static Entitas.IMatcher<GameEntity> NotAddedNavMeshRootSource {
        get {
            if (_matcherNotAddedNavMeshRootSource == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NotAddedNavMeshRootSource);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNotAddedNavMeshRootSource = matcher;
            }

            return _matcherNotAddedNavMeshRootSource;
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

    static readonly Code.Gameplay.Features.NavMesh.NotAddedNavMeshRootSource notAddedNavMeshRootSourceComponent = new Code.Gameplay.Features.NavMesh.NotAddedNavMeshRootSource();

    public bool isNotAddedNavMeshRootSource {
        get { return HasComponent(GameComponentsLookup.NotAddedNavMeshRootSource); }
        set {
            if (value != isNotAddedNavMeshRootSource) {
                var index = GameComponentsLookup.NotAddedNavMeshRootSource;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : notAddedNavMeshRootSourceComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

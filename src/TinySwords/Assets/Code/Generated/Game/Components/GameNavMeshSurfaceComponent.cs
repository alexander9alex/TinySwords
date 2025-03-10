//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherNavMeshSurface;

    public static Entitas.IMatcher<GameEntity> NavMeshSurface {
        get {
            if (_matcherNavMeshSurface == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.NavMeshSurface);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherNavMeshSurface = matcher;
            }

            return _matcherNavMeshSurface;
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

    public Code.Gameplay.Features.NavMesh.NavMeshSurfaceComponent navMeshSurface { get { return (Code.Gameplay.Features.NavMesh.NavMeshSurfaceComponent)GetComponent(GameComponentsLookup.NavMeshSurface); } }
    public NavMeshPlus.Components.NavMeshSurface NavMeshSurface { get { return navMeshSurface.Value; } }
    public bool hasNavMeshSurface { get { return HasComponent(GameComponentsLookup.NavMeshSurface); } }

    public GameEntity AddNavMeshSurface(NavMeshPlus.Components.NavMeshSurface newValue) {
        var index = GameComponentsLookup.NavMeshSurface;
        var component = (Code.Gameplay.Features.NavMesh.NavMeshSurfaceComponent)CreateComponent(index, typeof(Code.Gameplay.Features.NavMesh.NavMeshSurfaceComponent));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceNavMeshSurface(NavMeshPlus.Components.NavMeshSurface newValue) {
        var index = GameComponentsLookup.NavMeshSurface;
        var component = (Code.Gameplay.Features.NavMesh.NavMeshSurfaceComponent)CreateComponent(index, typeof(Code.Gameplay.Features.NavMesh.NavMeshSurfaceComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveNavMeshSurface() {
        RemoveComponent(GameComponentsLookup.NavMeshSurface);
        return this;
    }
}

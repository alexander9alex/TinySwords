//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherHpBar;

    public static Entitas.IMatcher<GameEntity> HpBar {
        get {
            if (_matcherHpBar == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HpBar);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHpBar = matcher;
            }

            return _matcherHpBar;
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

    public Code.Gameplay.Features.HpBars.HpBarComponent hpBar { get { return (Code.Gameplay.Features.HpBars.HpBarComponent)GetComponent(GameComponentsLookup.HpBar); } }
    public Code.Gameplay.Features.HpBars.Behaviours.IHpBar HpBar { get { return hpBar.Value; } }
    public bool hasHpBar { get { return HasComponent(GameComponentsLookup.HpBar); } }

    public GameEntity AddHpBar(Code.Gameplay.Features.HpBars.Behaviours.IHpBar newValue) {
        var index = GameComponentsLookup.HpBar;
        var component = (Code.Gameplay.Features.HpBars.HpBarComponent)CreateComponent(index, typeof(Code.Gameplay.Features.HpBars.HpBarComponent));
        component.Value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceHpBar(Code.Gameplay.Features.HpBars.Behaviours.IHpBar newValue) {
        var index = GameComponentsLookup.HpBar;
        var component = (Code.Gameplay.Features.HpBars.HpBarComponent)CreateComponent(index, typeof(Code.Gameplay.Features.HpBars.HpBarComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveHpBar() {
        RemoveComponent(GameComponentsLookup.HpBar);
        return this;
    }
}

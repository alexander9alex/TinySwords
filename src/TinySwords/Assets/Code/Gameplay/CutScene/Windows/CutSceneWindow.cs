using System;
using Code.Gameplay.CutScene.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.CutScene.Windows
{
  public class CutSceneWindow : MonoBehaviour
  {
    private readonly Color _invisibleColor = new(0, 0, 0, 0);

    public TextMeshProUGUI Text;
    public Button NextReplica;

    private CutSceneConfig _config;
    private Color _startColor;

    public void Construct(CutSceneConfig config) =>
      _config = config;

    private void Awake() =>
      _startColor = Text.color;

    public CutSceneConfig GetCutSceneConfig() =>
      _config;

    public void SetNextReplicaAction(Action action) =>
      NextReplica.onClick.AddListener(() => action?.Invoke());

    public void SetReplica(string text) =>
      Text.text = text;

    public void ClearReplica() =>
      Text.text = "";

    public void SetReplicaFade(float fade) =>
      Text.color = Color.Lerp(_invisibleColor, _startColor, fade);
  }
}

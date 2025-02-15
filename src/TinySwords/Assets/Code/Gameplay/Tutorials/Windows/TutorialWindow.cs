using System;
using System.Collections.Generic;
using Code.Gameplay.Features.Sounds.Data;
using Code.Gameplay.Features.Sounds.Services;
using Code.Gameplay.Tutorials.Data;
using Code.Gameplay.Tutorials.Extensions;
using Code.UI.Windows;
using Code.UI.Windows.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Tutorials.Windows
{
  public class TutorialWindow : BaseWindow
  {
    public TutorialId TutorialId;

    [Space(20)]
    public Button NextPageButton;
    public Button PreviousPageButton;
    public Button CloseTutorialButton;

    public Transform PagesParent;
    private List<GameObject> _pages;
    private int _pageId;

    private IWindowService _windowService;
    private ISoundService _soundService;
    private Action _closeTutorialAction;

    [Inject]
    private void Construct(IWindowService windowService, ISoundService soundService)
    {
      WindowId = TutorialId.ToWindowId();

      _windowService = windowService;
      _soundService = soundService;
    }

    protected override void Initialize()
    {
      InitializeButtons();
      InitializePages();
    }

    public void SetCloseTutorialAction(Action action) =>
      _closeTutorialAction = action;

    private void InitializeButtons()
    {
      NextPageButton.onClick.AddListener(NextPage);
      NextPageButton.onClick.AddListener(MakeClickSound);

      PreviousPageButton.onClick.AddListener(PreviousPage);
      PreviousPageButton.onClick.AddListener(MakeClickSound);

      CloseTutorialButton.onClick.AddListener(CloseTutorial);
    }

    private void InitializePages()
    {
      _pages = GetPages();
      HideAllPages();
      ShowPage(0);
    }

    private void HideAllPages()
    {
      for (int i = 0; i < _pages.Count; i++)
        HidePage(i);
    }

    private void ShowPage(int pageId)
    {
      SetNextButtonActive(pageId);
      SetPreviousButtonActive(pageId);
      _pages[pageId].SetActive(true);
    }

    private void NextPage() =>
      ChangePage(pageOffset: +1);

    private void PreviousPage() =>
      ChangePage(pageOffset: -1);

    private void CloseTutorial()
    {
      _closeTutorialAction?.Invoke();
      _windowService.CloseWindow(WindowId);
    }

    private void ChangePage(int pageOffset)
    {
      int nextPageId = _pageId + pageOffset;

      HidePage(_pageId);
      ShowPage(nextPageId);

      _pageId = nextPageId;
    }

    private List<GameObject> GetPages()
    {
      List<GameObject> pages = new();

      for (int i = 0; i < PagesParent.childCount; i++)
        pages.Add(PagesParent.GetChild(i).gameObject);

      return pages;
    }

    private void SetNextButtonActive(int pageId) =>
      NextPageButton.interactable = pageId < _pages.Count - 1;

    private void SetPreviousButtonActive(int pageId) =>
      PreviousPageButton.interactable = pageId > 0;

    private void HidePage(int pageId) =>
      _pages[pageId].SetActive(false);

    private void MakeClickSound() =>
      _soundService.PlaySound(SoundId.ButtonClick);
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
    private Action _closeTutorial;

    [Inject]
    private void Construct(IWindowService windowService)
    {
      WindowId = TutorialId.ToWindowId();

      _windowService = windowService;
    }

    protected override void Initialize()
    {
      InitializeButtons();
      InitializePages();
    }

    public void SetCloseTutorialAction(Action action) =>
      _closeTutorial = action;

    private void InitializeButtons()
    {
      NextPageButton.onClick.AddListener(NextPage);
      PreviousPageButton.onClick.AddListener(PreviousPage);
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
      _closeTutorial?.Invoke();
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
      return PagesParent.GetComponentsInChildren<Transform>()
        .Where(x => x != PagesParent)
        .Select(x => x.gameObject)
        .ToList();
    }

    private void SetNextButtonActive(int pageId) =>
      NextPageButton.interactable = pageId < _pages.Count - 1;

    private void SetPreviousButtonActive(int pageId) =>
      PreviousPageButton.interactable = pageId > 0;

    private void HidePage(int pageId) =>
      _pages[pageId].SetActive(false);
  }
}

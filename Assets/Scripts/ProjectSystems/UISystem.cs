using Studio.Scenes.Base;
using Studio.UI;
using Studio.UI.Views.Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Studio.ProjectSystems
{
    public class UISystem : MonoBehaviour
    {
        /// <summary>
        /// Root view in current scene that never hide by ViewStacks
        /// </summary>
        private View _rootView;

        /// <summary>
        /// All views in current scene
        /// </summary>
        private List<View> _views;

        private View _previousView;

        private ViewStacks _viewStacks;

        public View CurrentView { get; private set; }

        public SceneView CurrentSceneView { get; private set; }

        [Inject]
        public void Construct()
        {
            Utilities.Logger.Log("UISystem Construct", Settings.LogTypes.Info);
        }

        public void Initialize()
        {
        }

        public void SetupViewsInCurrentScene(List<View> views, View rootView, SceneView sceneView)
        {
            CurrentSceneView = sceneView;

            _rootView = rootView;

            _views = views;

            _viewStacks = new ViewStacks(this);
        }

        public void SetupViewsInCurrentScene(List<View> views, SceneView sceneView)
        {
            CurrentSceneView = sceneView;

            _views = views;
        }

        public List<View> TryToRemoveRootViewFromViewList(List<View> targetListOfViews, View rootView)
        {
            for (int i = 0; i < targetListOfViews.Count; i++)
            {
                if (targetListOfViews[i] == rootView)
                {
                    targetListOfViews.RemoveAt(i);
                }
            }

            return targetListOfViews;
        }

        public List<View> TryToRemoveDuplicatesFromViewList(List<View> targetListOfViews)
        {
            List<View> distinationList = targetListOfViews.Distinct().ToList();

            return distinationList;
        }

        // TODO - run this code before loading new scene
        // For example - when user click on button [Return to Menu] in gameplay scene
        // Need to reset current scene before initializing new
        // If don't do this - welcom to errror hell
        // My little pony (/ ^_^)/  \(^_^ \)
        public void ResetViewsBeforeSceneChange()
        {
            Utilities.Logger.Log($"Reseting views and SceneView in [{CurrentSceneView}]", Settings.LogTypes.Info);

            if (_views != null)
            {
                _views.Clear();
                _views = null;
            }

            if (_rootView != null)
            {
                _rootView.Dispose();
                _rootView = null;
            }

            if (_viewStacks != null)
            {
                _viewStacks.Dispose();
                _viewStacks = null;
            }

            if (CurrentSceneView != null)
            {
                CurrentSceneView.Dispose();
                CurrentSceneView = null;
            }
        }

        public void ReturnToPreviousView()
        {
            if (_previousView == null)
            {
                return;
            }

            if (CurrentView == null)
            {
                return;
            }

            CurrentView.Hide();
            _viewStacks.RemoveViewFromTopOfStack();

            CurrentView = _previousView;
            CurrentView.Show();
            _viewStacks.AddViewToTopOfStack(CurrentView);
        }

        public void ShowView<T>() where T : View
        {
            if (CurrentView != null)
            {
                _previousView = CurrentView;
                CurrentView.Hide();
                _viewStacks.RemoveViewFromTopOfStack();
            }

            for (int i = 0; i < _views.Count; i++)
            {
                if (_views[i] is T)
                {
                    CurrentView = _views[i];
                    break;
                }
            }

            CurrentView.Show();
            _viewStacks.AddViewToTopOfStack(CurrentView);
        }

        public void ShowView(View viewToShow)
        {
            if (CurrentView != null)
            {
                _previousView = CurrentView;
                CurrentView.Hide();
                _viewStacks.RemoveViewFromTopOfStack();
            }

            CurrentView = viewToShow;
            CurrentView.Show();
            _viewStacks.AddViewToTopOfStack(CurrentView);
        }

        public void HideCurrentView()
        {
            if (CurrentView != null)
            {
                _previousView = CurrentView;
                CurrentView.Hide();
                _viewStacks.RemoveViewFromTopOfStack();
            }

            SetCurrentViewByFirstViewInStack();
        }

        public T GetView<T>() where T : View
        {
            View view = null;

            for (int i = 0; i < _views.Count; i++)
            {
                if (_views[i] is T)
                {
                    view = _views[i];
                    return (T)view;
                }
            }

            return (T)view;
        }

        private void SetCurrentViewByFirstViewInStack()
        {
            CurrentView = _viewStacks.GetFirstViewInStack();
        }
    }
}
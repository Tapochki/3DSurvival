using Studio.ProjectSystems;
using Studio.Settings;
using Studio.UI.Views;
using Studio.UI.Views.Base;
using System.Collections.Generic;

namespace Studio.UI
{
    public class ViewStacks
    {
        private UISystem _uiSystem;

        private View _currentView;

        private Stack<View> _stackOfViews;

        public ViewStacks(UISystem uiSystem)
        {
            _uiSystem = uiSystem;

            Init();
        }

        private void Init()
        {
            _stackOfViews = new Stack<View>();

            EventBus.OnEscapeButtonDownEvent -= OnEscapeButtonDownEventHandler;
            EventBus.OnEscapeButtonDownEvent += OnEscapeButtonDownEventHandler;
        }

        private void OnEscapeButtonDownEventHandler()
        {
            if (_stackOfViews.Count <= 0)
            {
                _uiSystem.ShowView<ViewExitPage>();
                AddViewToTopOfStack(_uiSystem.GetView<ViewExitPage>());
                return;
            }

            _currentView.Hide();
            RemoveViewFromTopOfStack();
        }

        public void AddViewToTopOfStack(View panel)
        {
            _currentView = panel;
            _stackOfViews.Push(panel);
        }

        public void RemoveViewFromTopOfStack()
        {
            if (_stackOfViews.Count > 0)
            {
                _stackOfViews.Pop();
            }

            _currentView = GetFirstViewInStack();
        }

        public View GetFirstViewInStack()
        {
            if (_stackOfViews.Count > 0)
            {
                return _stackOfViews.Peek();
            }

            return null;
        }

        public void Dispose()
        {
            _currentView = null;

            _stackOfViews.Clear();
            _stackOfViews = null;

            EventBus.OnEscapeButtonDownEvent -= OnEscapeButtonDownEventHandler;
        }
    }
}
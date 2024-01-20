using Studio.ProjectSystems;
using Studio.UI.Views.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Studio.Scenes.Base
{
    public class SceneView : MonoBehaviour
    {
        [Header("Root view that never been hiden by ViewStacks")]
        [SerializeField] private View _rootView;

        [Space(4)]
        [Header("List of all view in current scene exept root view")]
        [SerializeField] private List<View> _views;

        protected UISystem _uiSystem;
        protected SoundSystem _soundSystem;

        public virtual void Construct(SoundSystem soundSystem, UISystem uiSystem)
        {
            Utilities.Logger.Log("Base SceneView Construct", Settings.LogTypes.Info);

            _uiSystem = uiSystem;
            _soundSystem = soundSystem;
        }

        public virtual void Init()
        {
            if (_rootView != null)
            {
                _views = _uiSystem.TryToRemoveRootViewFromViewList(_views, _rootView);
            }

            _views = _uiSystem.TryToRemoveDuplicatesFromViewList(_views);

            _uiSystem.SetupViewsInCurrentScene(_views, _rootView, this);

            for (int i = 0; i < _views.Count; i++)
            {
                _views[i].Construct(_soundSystem);

                _views[i].Hide();
            }

            if (_rootView != null)
            {
                _rootView.Construct(_soundSystem);
                _rootView.Show();
            }
        }

        public virtual void ShowView(View view)
        {
        }

        public virtual void HideView()
        {
        }

        public virtual void Dispose()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                _views[i].Dispose();
            }

            _rootView.Dispose();
            _rootView = null;

            _soundSystem = null;
            _uiSystem = null;
        }
    }
}
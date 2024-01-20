using Studio.ProjectSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Studio.UI.Views.Base
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class View : MonoBehaviour
    {
        private SoundSystem _soundSystem;

        private GameObject _selfObject;
        private Canvas _canvas;

        public void ForceActiveGameObject() => _selfObject.SetActive(true);

        public virtual void Construct(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
        }

        public virtual void Init()
        {
            _selfObject = this.gameObject;

            if (!_selfObject.activeInHierarchy)
            {
                ForceActiveGameObject();
            }

            _canvas = GetComponent<Canvas>();
            _canvas.vertexColorAlwaysGammaSpace = true;
        }

        public virtual void Show()
        {
            if (_canvas.enabled)
            {
                return;
            }

            if (_canvas == null)
            {
                return;
            }

            _canvas.enabled = true;

            _soundSystem.PlaySound(Settings.Sounds.ShowView);
        }

        public virtual void Hide()
        {
            if (!_canvas.enabled)
            {
                return;
            }

            if (_canvas == null)
            {
                return;
            }

            _canvas.enabled = false;

            _soundSystem.PlaySound(Settings.Sounds.HideView);
        }

        public virtual void Dispose()
        {
            _canvas = null;
            _soundSystem = null;
            _selfObject = null;
        }
    }
}
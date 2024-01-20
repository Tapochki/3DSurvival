using Studio.Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio.Utilities
{
    public class OnBehaviourHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler,
        IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        { EventBus.OnDragBeginEvent?.Invoke(eventData, gameObject); }

        public void OnDrag(PointerEventData eventData)
        { EventBus.OnDragUpdateEvent?.Invoke(eventData, gameObject); }

        public void OnEndDrag(PointerEventData eventData)
        { EventBus.OnDragEndEvent?.Invoke(eventData, gameObject); }

        public void OnPointerEnter(PointerEventData eventData)
        { EventBus.OnPointerEnterEvent?.Invoke(eventData); }

        public void OnPointerExit(PointerEventData eventData)
        { EventBus.OnPointerExitEvent?.Invoke(eventData); }

        public void OnPointerDown(PointerEventData eventData)
        { EventBus.OnPointerDownEvent?.Invoke(eventData); }

        public void OnPointerUp(PointerEventData eventData)
        { EventBus.OnPointerUpEvent?.Invoke(eventData); }

        private void OnMouseUp()
        { EventBus.OnMouseUpEvent?.Invoke(gameObject); }

        private void OnMouseDown()
        { EventBus.OnMouseDownEvent?.Invoke(gameObject); }

        private void OnTriggerEnter2D(Collider2D collider)
        { EventBus.OnTrigger2DEnterEvent?.Invoke(collider); }

        private void OnTriggerStay2D(Collider2D collision)
        { EventBus.OnTrigger2DStayEvent?.Invoke(collision); }

        private void OnTriggerExit2D(Collider2D collider)
        { EventBus.OnTrigger2DExitEvent?.Invoke(collider); }

        private void OnTriggerEnter(Collider collider)
        { EventBus.OnTriggerEnterEvent?.Invoke(collider); }

        private void OnTriggerStay(Collider collider)
        { EventBus.OnTriggerStayEvent?.Invoke(collider); }

        private void OnTriggerExit(Collider collider)
        { EventBus.OnTriggerExitEvent?.Invoke(collider); }

        private void OnCollisionEnter2D(Collision2D collision)
        { EventBus.OnCollision2DEnterEvent?.Invoke(collision); }

        private void OnCollisionStay2D(Collision2D collision)
        { EventBus.OnCollision2DStayEvent?.Invoke(collision); }

        private void OnCollisionExit2D(Collision2D collision)
        { EventBus.OnCollision2DExitEvent?.Invoke(collision); }

        private void OnCollisionEnter(Collision collision)
        { EventBus.OnCollisionEnterEvent?.Invoke(collision); }

        private void OnCollisionStay(Collision collision)
        { EventBus.OnCollisionStayEvent?.Invoke(collision); }

        private void OnCollisionExit(Collision collision)
        { EventBus.OnCollisionExitEvent?.Invoke(collision); }

        private void OnAnimationStringEvent(string parameter)
        { EventBus.OnAnimationStringEvent?.Invoke(parameter); }

        private void OnAnimationEvent()
        { EventBus.OnAnimationEvent?.Invoke(); }
    }
}
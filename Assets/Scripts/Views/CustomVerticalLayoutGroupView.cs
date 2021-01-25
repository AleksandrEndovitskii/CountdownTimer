using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class CustomVerticalLayoutGroupView : MonoBehaviour
    {
        public Action<RectTransform> ElementAdded = delegate { };

        public List<RectTransform> Content => _content;

        private List<RectTransform> _content = new List<RectTransform>();

        private VerticalLayoutGroup _verticalLayoutGroup;

        private Vector2 _directionOfElementsInstantiation = Vector2.up;

        private void Awake()
        {
            _verticalLayoutGroup = this.gameObject.GetComponent<VerticalLayoutGroup>();
        }

        public void AddElement(RectTransform rectTransform)
        {
            rectTransform.gameObject.transform.SetParent(this.gameObject.transform);

            var layoutElement = rectTransform.gameObject.GetComponent<LayoutElement>();

            var indexOfElement = _content.Count;

            var elementPositionY = -_directionOfElementsInstantiation.y * indexOfElement * layoutElement.preferredHeight;
            elementPositionY += -_directionOfElementsInstantiation.y * indexOfElement * _verticalLayoutGroup.spacing;

            elementPositionY += -_directionOfElementsInstantiation.y * _verticalLayoutGroup.padding.top;

            rectTransform.anchoredPosition = new Vector2(_verticalLayoutGroup.padding.left, elementPositionY);

            _content.Add(rectTransform);
            ElementAdded.Invoke(rectTransform);
        }
    }
}

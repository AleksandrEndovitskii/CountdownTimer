using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class CustomVerticalLayoutGroupView : MonoBehaviour
    {
        public Action<List<RectTransform>> ContentChanged = delegate { };

        public List<RectTransform> Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value == _content)
                {
                    return;
                }

                _content = value;

                ContentChanged.Invoke(Content);
            }
        }

        private List<RectTransform> _content = new List<RectTransform>();

        private VerticalLayoutGroup _verticalLayoutGroup;

        private Vector2 _directionOfElementsInstantiation = Vector2.up;

        private void Awake()
        {
            _verticalLayoutGroup = this.gameObject.GetComponent<VerticalLayoutGroup>();
        }

        public void ClearContent()
        {
            foreach (var rectTransform in Content)
            {
                Destroy(rectTransform.gameObject);
            }

            Content.Clear();
        }
        public void CreateContent(List<RectTransform> rectTransforms)
        {
            for (var i = 0; i < rectTransforms.Count; i++)
            {
                rectTransforms[i].gameObject.transform.SetParent(this.gameObject.transform);

                var rectTransform = rectTransforms[i].gameObject.GetComponent<RectTransform>();
                var layoutElement = rectTransforms[i].gameObject.GetComponent<LayoutElement>();

                var elementPositionY = -_directionOfElementsInstantiation.y * i * layoutElement.preferredHeight;
                elementPositionY += -_directionOfElementsInstantiation.y * i * _verticalLayoutGroup.spacing;

                elementPositionY += -_directionOfElementsInstantiation.y * _verticalLayoutGroup.padding.top;

                rectTransform.anchoredPosition = new Vector2(_verticalLayoutGroup.padding.left, elementPositionY);
            }

            Content = rectTransforms;
        }
    }
}

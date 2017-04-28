using UnityEngine;
using UnityEditor;

class TwoD : MonoBehaviour {
    [MenuItem("My Tools/2D/Fit Rect to Children")]
    static void FitToChildren() {
        foreach (GameObject rootGameObject in Selection.gameObjects) {
            RectTransform rootTransform = rootGameObject.GetComponent<RectTransform>();

            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            int count = rootGameObject.transform.childCount;
            for (int i = 0; i < count; ++i) {
                Transform child = rootGameObject.transform.GetChild(0);
//                print(child.gameObject.name);
                RectTransform childRect = child.GetComponent<RectTransform>();
//                print(childRect.position);
//                print(childRect.rect.position);
                if (childRect != null) {
                    print(childRect.anchoredPosition);
//                    Vector2 oldAnchorMin = childRect.anchorMin;
//                    Vector2 oldAnchorMax = childRect.anchorMax;
//                    childRect.anchorMin = new Vector2(0.5f, 0.5f);
//                    childRect.anchorMax = new Vector2(0.5f, 0.5f);
                    child.SetParent(rootTransform.parent);
                    print(childRect.anchoredPosition);
                    Bounds childBounds = new Bounds(childRect.anchoredPosition, new Vector3(childRect.rect.width, childRect.rect.height, 0));
//                    childRect.anchorMin = oldAnchorMin;
//                    childRect.anchorMax = oldAnchorMax;
                    childRect.SetParent(rootTransform);
//                    print(childBounds);
                    if (hasBounds) {
                        bounds.Encapsulate(childBounds);
                    }
                    else {
                        bounds = childBounds;
                        hasBounds = true;
                    }
//                    print(bounds);
                }
            }

//            rootTransform.offsetMin = bounds.min;
//            rootTransform.offsetMax = bounds.max;

//            Vector2 center = new Vector2(0.5f, 0.5f);
//            Vector2 topLeft = new Vector2(0, 1);
//            Vector2 oldAnchorMin = rootTransform.anchorMin;
//            Vector2 oldAnchorMax = rootTransform.anchorMax;
//            Vector2 oldPivot = rootTransform.pivot;
//            rootTransform.anchorMin = topLeft;
//            rootTransform.anchorMax = topLeft;
//            rootTransform.pivot = topLeft;
//
//            rootTransform.anchoredPosition = bounds.center;

//            rootTransform.anchorMin = oldAnchorMin;
//            rootTransform.anchorMax = oldAnchorMax;
//            rootTransform.pivot = oldPivot;

            print(bounds);
        }
    }
}

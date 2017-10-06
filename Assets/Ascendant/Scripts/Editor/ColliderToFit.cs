using UnityEngine;
using UnityEditor;

public class ColliderToFit : MonoBehaviour {

    [MenuItem("My Tools/Collider/Fit to Children")]
    static void FitToChildren() {
        foreach (GameObject rootGameObject in Selection.gameObjects) {
            Collider rootCollider = rootGameObject.GetComponent<Collider>();
            if (!(rootCollider is BoxCollider))
                continue;

            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            for (int i = 0; i < rootGameObject.transform.childCount; ++i) {
                Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
                if (childRenderer != null) {
                    if (hasBounds) {
                        bounds.Encapsulate(childRenderer.bounds);
                    } else {
                        bounds = childRenderer.bounds;
                        hasBounds = true;
                    }
                }
            }

            BoxCollider collider = (BoxCollider)rootCollider;
            collider.center = bounds.center - rootGameObject.transform.position;
            collider.size = bounds.size;
        }
    }

    [MenuItem("My Tools/Collider2D/Fit to Children")]
    static void FitToChildren2D() {
        foreach (GameObject rootGameObject in Selection.gameObjects) {
            Collider2D rootCollider = rootGameObject.GetComponent<Collider2D>();
            if (!(rootCollider is BoxCollider2D))
                continue;

            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            for (int i = 0; i < rootGameObject.transform.childCount; ++i) {
                RectTransform childRect = rootGameObject.transform.GetChild(i).GetComponent<RectTransform>();
                if (childRect != null) {
                    Bounds childBounds = new Bounds(childRect.rect.position, new Vector3(childRect.rect.width, childRect.rect.height, 0));
                    if (hasBounds) {
                        bounds.Encapsulate(childBounds);
                    }
                    else {
                        bounds = childBounds;
                        hasBounds = true;
                    }
                    print(bounds);
                }
                else {
                    print("no renderer");
                }
            }

            BoxCollider2D collider = (BoxCollider2D)rootCollider;
            collider.offset = bounds.center;
            collider.size = bounds.size;
        }
    }

}
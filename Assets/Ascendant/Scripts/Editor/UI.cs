using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    private static string buttonFont = "Fonts/CinzelDecorative-Black";
    [MenuItem("GameObject/Ascendant/Button")]
    public static void Button(MenuCommand menuCommand) {
        // Use default button creator
        GameObject buttonGameObject = DefaultControls.CreateButton(new DefaultControls.Resources() {
            standard = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd")
//            standard = Resources.Load<Sprite>("Button")
        });

//        EventTrigger.Entry e = new EventTrigger.Entry();
//        e.eventID = EventTriggerType.PointerEnter;
//        e.callback.AddListener((eventData) => {
//            EditorGUIUtility.AddCursorRect(eventData.selectedObject.GetComponent<RectTransform>().rect, MouseCursor.Link);
//        });
//        buttonGameObject.AddComponent<EventTrigger>().triggers.Add(e);
        
        // Setup image
        Image image = buttonGameObject.GetComponent<Image>();
        image.color = Utils.Color.FromDecimal(255, 90, 90);
        image.material = Resources.GetBuiltinResource<Material>("Sprites-Default.mat");

        // Setup button
        Button button = buttonGameObject.GetComponent<Button>();
        button.colors = new ColorBlock() {
            normalColor = Utils.Color.FromDecimal(200, 200, 200),
            highlightedColor = Color.white,
            pressedColor = Utils.Color.FromDecimal(200, 200, 200),
            disabledColor = Utils.Color.FromDecimal(200, 200, 200, 128),
            colorMultiplier = 1,
            fadeDuration = 0.1f
        };
        button.navigation = new Navigation() {
            mode = Navigation.Mode.None
        };

        // Setup text
        Text text = buttonGameObject.GetComponentInChildren<Text>();
        text.color = Color.white;
        text.font = Resources.Load<Font>(buttonFont);
        text.material = Resources.Load<Material>(buttonFont);

        // Attach to correct GameObject
        if (menuCommand.context == null) {
            buttonGameObject.transform.SetParent(FindObjectOfType<Canvas>().transform);
        } else {
            GameObjectUtility.SetParentAndAlign(buttonGameObject, menuCommand.context as GameObject);
        }

        // Allow undo
        Undo.RegisterCreatedObjectUndo(buttonGameObject, "Create Ascendant Button");

        // Select new button
        Selection.activeObject = buttonGameObject;
    }
}
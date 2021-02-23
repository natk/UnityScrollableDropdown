using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Demonstrates a custom dropdown list for Editor UI
// It's faster to scroll than EditorGUILayout.Popup when having many items
public class ScrollableDdlExample : EditorWindow
{
    // Make it SerializeField so that the scroll posision is the same as the last time
    [SerializeField]static Vector2 _scrollPos = Vector2.zero;
    bool showDdl = false;
    public Rect ddlRect = new Rect(100, 100, 200, 200);
    string ddlLabel = "Select";

    [MenuItem("Window/ScrollableDdlExample")]
    static void Init()
    {
        ScrollableDdlExample window = (ScrollableDdlExample)EditorWindow.GetWindow(typeof(ScrollableDdlExample));
    }

    void OnGUI()
    {
        // Dropdown style Button to show Dropdown
        if(EditorGUILayout.DropdownButton(new GUIContent(ddlLabel), FocusType.Passive))
        {
            showDdl = true;
        }
        // Draw Dropdown
        if(showDdl){
            BeginWindows();
            ddlRect = GUILayout.Window(123, ddlRect, DrawDdl, "");
            // Close the dropdown window when clicked outside
            if(Event.current.type == EventType.MouseDown){
                if(ddlRect.Contains(Event.current.mousePosition) == false){
                    showDdl = false;
                }
            }
            EndWindows();
        }
    }

    void OnLostFocus(){
        showDdl = false; // Close the dropdown window when window loose focus
    }

    // Dropdown window contents
    void DrawDdl(int unusedWindowID)
    {
        // Make it scrollable
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        // Scrollable items
        for(int i = 0; i < 100; i++){
            if(GUILayout.Button("Item " + i, GUILayout.ExpandWidth(true))){
                ddlLabel = "Item " + i;
                showDdl = false;
            }
        }
        EditorGUILayout.EndScrollView();
        GUI.DragWindow();
    }
}

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class NodeEditor : EditorWindow
{
    Rect window1;
    Rect window2;    

    [MenuItem("Window/Node editor")]
    static void ShowEditor()
    {
        NodeEditor editor = EditorWindow.GetWindow<NodeEditor>();
        editor.Init();
    }

    public void Init()
    {
        window1 = new Rect(10, 10, 100, 100);
        window2 = new Rect(210, 210, 100, 100);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,30), "Reload")) {
            LoadAllObjectsInScene();
        };
        //DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows

        BeginWindows();
        window1 = GUI.Window(1, window1, DrawNodeWindow, "Window 1");   // Updates the Rect's when these are dragged
        window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2");
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        if (GUI.Button(new Rect(10, 20, 80, 20), "Hello World")) {
            Debug.Log("Got a click in window " + id);
        }

        GUI.DragWindow();
    }

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.white, null, 1.5f);
    }

    void LoadAllObjectsInScene() {
        Transform[] objects = FindObjectsOfType<Transform>();
        foreach (Transform transform in objects) {
            Component[] components = transform.GetComponentsInChildren<Component>();
            foreach (Component component in components) {
                List<System.Reflection.FieldInfo> fieldsWithEvents = GetFieldsWithUnityEvents(component);
                foreach (System.Reflection.FieldInfo property in fieldsWithEvents) {
                    Debug.Log(transform.name + " " + component.GetType() + " = " + property.FieldType + " " + property.Name);
                }

                List<System.Reflection.PropertyInfo> propsWithEvents = GetPropsWithUnityEvents(component);

                foreach (System.Reflection.PropertyInfo property in propsWithEvents) {
                    Debug.Log(transform.name + " " + component.GetType() + " = " + property.PropertyType + " " + property.Name);
                }
            }
        }
    }

    List<System.Reflection.FieldInfo> GetFieldsWithUnityEvents(Component component) {
        List<System.Reflection.FieldInfo> fieldsWithEvents = new List<System.Reflection.FieldInfo>();

        System.Reflection.FieldInfo[] fields = component.GetType().GetFields();
        foreach (System.Reflection.FieldInfo field in fields) {
            if (field.FieldType.IsSubclassOf(typeof(UnityEventBase))) {
                fieldsWithEvents.Add(field);
            }
        }
        return fieldsWithEvents;
    }

    List<System.Reflection.PropertyInfo> GetPropsWithUnityEvents(Component component) {
        List<System.Reflection.PropertyInfo> propsWithEvents = new List<System.Reflection.PropertyInfo>();

        System.Reflection.PropertyInfo[] props = component.GetType().GetProperties();
        foreach (System.Reflection.PropertyInfo prop in props) {
            if (prop.PropertyType.IsSubclassOf(typeof(UnityEventBase))) {
                propsWithEvents.Add(prop);
            }
        }
        return propsWithEvents;
    }
}
#endif
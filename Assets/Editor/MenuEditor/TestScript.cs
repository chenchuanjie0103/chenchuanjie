using UnityEditor;
using UnityEngine;

public class TestScript : EditorWindow
{
    private static TestScript window;
    private bool b_Toggle = true;
    private string windowName = "第一个editor窗体";

    //显示窗体入口
    [MenuItem("Test+/MyWindow")]
    private static void ShowWindow()
    {
        window = EditorWindow.GetWindow<TestScript>("Window Example");
        window.Show();
    }

    //显示时调用
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    Rect r = new Rect(100, 200, 60, 60);
    //绘制窗体内容
    private void OnGUI()
    {
        windowName = EditorGUILayout.TextField("name", windowName);
        b_Toggle = EditorGUILayout.Toggle("Toggle", b_Toggle);
        if (GUI.Button(new Rect(0, 50, 100, 30), "按钮"))
        {
            Debug.Log("这是按钮操作");
            PopupWindow.Show(r, new PopupExample());
        }
        if (b_Toggle)
        {
            BeginWindows();
            r = GUILayout.Window(1, r, DoWindow, "嵌套窗口");
            EndWindows();
        }
    }
    void DoWindow(int WindowID)
    {
        GUILayout.Button("这是嵌套窗口");
        windowName = EditorGUILayout.TextField("name", windowName);
        b_Toggle = EditorGUILayout.Toggle("Toggle", b_Toggle);
        GUI.DragWindow();//可拖动
    }
    //固定帧数调用
    private void Update()
    {
        Debug.Log("Update");
    }

    //隐藏时调用
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    //销毁时调用
    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
public class PopupExample : PopupWindowContent
{
    bool toggle1 = true;
    bool toggle2 = true;
    bool toggle3 = true;

    public override Vector2 GetWindowSize()
    {
        return new Vector2(200, 150);
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Popup Options Example", EditorStyles.boldLabel);
        toggle1 = EditorGUILayout.Toggle("Toggle 1", toggle1);
        toggle2 = EditorGUILayout.Toggle("Toggle 2", toggle2);
        toggle3 = EditorGUILayout.Toggle("Toggle 3", toggle3);
    }

    public override void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
    }

    public override void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
}
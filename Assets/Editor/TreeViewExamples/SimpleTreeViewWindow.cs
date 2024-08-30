using UnityEngine;
using UnityEditor.IMGUI.Controls;


namespace UnityEditor.TreeViewExamples
{
    public class SimpleTreeViewWindow : EditorWindow
    {
        // SerializeField 用于确保将视图状态写入窗口
        // 布局文件。这意味着只要窗口未关闭，即使重新启动 Unity，也会保持
        // 状态。如果省略该属性，仍然会序列化/反序列化状态。
        [SerializeField] TreeViewState m_TreeViewState;
        //TreeView 不可序列化，因此应该通过树数据对其进行重建。
        SimpleTreeView m_TreeView;
        SearchField m_SearchField;

        [MenuItem("TreeView Examples/Simple Tree Window")]
        static void ShowWindow()
        {
            // 获取现有打开的窗口；如果没有，则新建一个窗口：
            var window = GetWindow<SimpleTreeViewWindow>();
            window.titleContent = new GUIContent("My Window");
            window.Show();
        }
        void OnEnable()
        {
            //检查是否已存在序列化视图状态（在程序集重新加载后
            // 仍然存在的状态）
            if (m_TreeViewState == null)
                m_TreeViewState = new TreeViewState();

            m_TreeView = new SimpleTreeView(m_TreeViewState);
            m_SearchField = new SearchField();
            m_SearchField.downOrUpArrowKeyPressed += m_TreeView.SetFocusAndEnsureSelectedItem;
        }
        void OnGUI()
        {
            DoToolbar();
            DoTreeView();
            //m_TreeView.OnGUI(new Rect(0, 0, position.width, position.height));
        }
        void DoToolbar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Space(100);
            GUILayout.FlexibleSpace();
            m_TreeView.searchString = m_SearchField.OnToolbarGUI(m_TreeView.searchString);
            GUILayout.EndHorizontal();
        }
        void DoTreeView()
        {
            Rect rect = GUILayoutUtility.GetRect(0, 100000, 0, 100000);
            m_TreeView.OnGUI(rect);
        }
    }
}

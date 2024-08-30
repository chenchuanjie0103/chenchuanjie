using UnityEngine;
using UnityEditor.IMGUI.Controls;


namespace UnityEditor.TreeViewExamples
{
	// 定义一个编辑器窗口类，用于展示自定义的 TreeView
	class TransformTreeWindow : EditorWindow
    {
        [SerializeField] TreeViewState m_TreeViewState; // 存储 TreeView 的状态
		[SerializeField] string currentSearchQuery = string.Empty; // 更具描述性的搜索字符串变量
		SearchField m_SearchField;   // 添加搜索框组件
		TreeView m_TreeView;

		[MenuItem("TreeView Examples/Transform Hierarchy")]
		static void ShowWindow()
		{
			var window = GetWindow<TransformTreeWindow>();
			window.titleContent = new GUIContent("My Hierarchy");
			window.Show();
		}

		void OnEnable ()
		{
			if (m_TreeViewState == null)
				m_TreeViewState = new TreeViewState ();
			m_TreeView = new TransformTreeView(m_TreeViewState, new MMultiColumnHeader());
			m_SearchField = new SearchField();
		}

		void OnSelectionChange ()
		{
			if (m_TreeView != null)
				m_TreeView.SetSelection (Selection.instanceIDs);
			Repaint ();
		}

		void OnHierarchyChange()
		{
			if (m_TreeView != null)
				m_TreeView.Reload();
			Repaint ();
		}

		void OnGUI ()
        {
            DoToolbar();
			DoTreeView();
		}

        // 绘制 TreeView 控件
        void DoTreeView()
		{
			Rect rect = GUILayoutUtility.GetRect(0, 100000, 0, 100000); // 获取一个足够大的矩形区域
			m_TreeView.OnGUI(rect);
		}

		// 绘制工具栏
		void DoToolbar()
		{
			GUILayout.BeginVertical();
			GUILayout.Space(5);
			GUILayout.BeginHorizontal(EditorStyles.toolbar);

			Rect searchFieldRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.Height(20));
			string updatedSearchQuery = m_SearchField.OnGUI(searchFieldRect, currentSearchQuery);

			if(!string.Equals(updatedSearchQuery, currentSearchQuery))
			{
				UpdateSearchQuery(updatedSearchQuery);
			}
            GUILayout.EndHorizontal();
			GUILayout.EndVertical();
		}
		void UpdateSearchQuery(string newQuery)
		{
			currentSearchQuery = newQuery;
			m_TreeView.searchString = currentSearchQuery;
			m_TreeView.Reload();
		}
	}
}

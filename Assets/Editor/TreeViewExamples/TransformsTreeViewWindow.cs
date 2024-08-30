using UnityEngine;
using UnityEditor.IMGUI.Controls;


namespace UnityEditor.TreeViewExamples
{
	// ����һ���༭�������࣬����չʾ�Զ���� TreeView
	class TransformTreeWindow : EditorWindow
    {
        [SerializeField] TreeViewState m_TreeViewState; // �洢 TreeView ��״̬
		[SerializeField] string currentSearchQuery = string.Empty; // ���������Ե������ַ�������
		SearchField m_SearchField;   // ������������
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

        // ���� TreeView �ؼ�
        void DoTreeView()
		{
			Rect rect = GUILayoutUtility.GetRect(0, 100000, 0, 100000); // ��ȡһ���㹻��ľ�������
			m_TreeView.OnGUI(rect);
		}

		// ���ƹ�����
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

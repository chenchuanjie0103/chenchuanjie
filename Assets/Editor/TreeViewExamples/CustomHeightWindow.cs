using System;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UnityEditor.TreeViewExamples
{
	class CustomHeightWindow : EditorWindow
	{
		//1. �ֶκ�����
		[NonSerialized] bool m_Initialized;     //���ڱ�Ǵ����Ƿ��ѳ�ʼ��
		[SerializeField] TreeViewState m_TreeViewState; //���ڱ�������ͼ��״̬
		SearchField m_SearchField;		//�����ṩ��������
		CustomHeightTreeView m_TreeView;    //�Զ��������ͼ�࣬������ʾ�Ͳ�����������
		MyTreeAsset m_MyTreeAsset;      //���е�ǰ����ͼ���ݵ��ʲ�����

		//2. ���ڲ˵�����ʲ��򿪴���
		[MenuItem("TreeView Examples/Custom Row Heights")]
		public static CustomHeightWindow GetWindow()
		{
			var window = GetWindow<CustomHeightWindow>();
			window.titleContent = new GUIContent("Custom Heights");
			window.Focus();
			window.Repaint();
			return window;
		}
		//OnOpenAsset ���������û�˫�� MyTreeAsset ���͵��ʲ�ʱ���Զ��� CustomHeightWindow ���ڲ����ظ��ʲ�������
		[OnOpenAsset]
		public static bool OnOpenAsset (int instanceID, int line)
		{
			var myTreeAsset = EditorUtility.InstanceIDToObject (instanceID) as MyTreeAsset;
			if (myTreeAsset != null)
			{
				var window = GetWindow ();
				window.SetTreeAsset(myTreeAsset);
				return true;
			}
			return false;
		}
		//����������ͼ������Դ
		void SetTreeAsset (MyTreeAsset myTreeAsset)
		{
			m_MyTreeAsset = myTreeAsset;
			m_Initialized = false;
		}
		//3. ���ڲ��ֺͳ�ʼ��
		Rect treeViewRect
		{
			get { return new Rect(20, 30, position.width-40, position.height-60); }
		}
		Rect toolbarRect
		{
			get { return new Rect (20f, 10f, position.width-40f, 20f); }
		}
		Rect bottomToolbarRect
		{
			get { return new Rect(20f, position.height - 18f, position.width - 40f, 16f); }
		}
		//InitIfNeeded ������鴰���Ƿ��ѳ�ʼ��
		void InitIfNeeded ()
		{
			if (!m_Initialized)
			{
				//δ��ʼ���򴴽� TreeViewState��TreeModel �� CustomHeightTreeView ʵ���������������ֶε��¼�������
				if (m_TreeViewState == null)
					m_TreeViewState = new TreeViewState();
					
				var treeModel = new TreeModel<MyTreeElement>(GetData());
				m_TreeView = new CustomHeightTreeView(m_TreeViewState, treeModel);

				m_SearchField = new SearchField ();
				m_SearchField.downOrUpArrowKeyPressed += m_TreeView.SetFocusAndEnsureSelectedItem;

				m_Initialized = true;
			}
		}
		//4. ���ݻ�ȡ��ѡ��仯����
		IList<MyTreeElement> GetData ()
		{
			//GetData() ������ȡ����ͼ������
			if (m_MyTreeAsset != null && m_MyTreeAsset.treeElements != null && m_MyTreeAsset.treeElements.Count > 0)
				return m_MyTreeAsset.treeElements;

			// ����һЩ�������
			return MyTreeElementGenerator.GenerateRandomTree(130); 
		}
		void OnSelectionChange ()
		{
			//OnSelectionChange() ������ѡ��仯ʱ��������ͼ����
			if (!m_Initialized)
				return;

			var myTreeAsset = Selection.activeObject as MyTreeAsset;
			if (myTreeAsset != null && myTreeAsset != m_MyTreeAsset)
			{
				//��������ͼ�����¼���
				m_MyTreeAsset = myTreeAsset;
				m_TreeView.treeModel.SetData (GetData ());
				m_TreeView.Reload ();
			}
		}
		//5. GUI ����
		void OnGUI ()
		{
			InitIfNeeded();     //���� InitIfNeeded ��ȷ����ʼ��
			SearchBar (toolbarRect);
			DoTreeView(treeViewRect);
			BottomToolBar (bottomToolbarRect);
		}
		//SearchBar() �������������򲢴��������ַ���
		void SearchBar (Rect rect)
		{
			m_TreeView.searchString = m_SearchField.OnGUI(rect, m_TreeView.searchString);
		}
		//DoTreeView() ������������ͼ
		void DoTreeView (Rect rect)
		{
			m_TreeView.OnGUI(rect);
		}
		//BottomToolBar() �������Ƶײ������������ṩ��չ��ȫ�����͡��۵�ȫ������ť����������ͼ�Ľڵ�״̬
		void BottomToolBar (Rect rect)
		{
			GUILayout.BeginArea (rect);// ��ָ���ľ��������ڿ�ʼ���� GUI ���
			// ʹ��ˮƽ�����Ժ������а�ť�ͱ�ǩ
			using (new EditorGUILayout.HorizontalScope ())
			{
				var style = "miniButton";   // ���尴ť����ʽ
				if (GUILayout.Button("Expand All", style))
				{
					m_TreeView.ExpandAll ();
				}

				if (GUILayout.Button("Collapse All", style))
				{
					m_TreeView.CollapseAll ();
				}
			}
			GUILayout.EndArea();
		}
	}
}

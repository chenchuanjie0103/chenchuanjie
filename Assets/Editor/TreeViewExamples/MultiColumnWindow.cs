using System;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UnityEditor.TreeViewExamples
{
	class MultiColumnWindow : EditorWindow
	{
		//1. �ֶκ�����
		[NonSerialized] bool m_Initialized;     //���ڱ�Ǵ����Ƿ��ѳ�ʼ��
		[SerializeField] TreeViewState m_TreeViewState; //���ڱ�������ͼ��״̬
		[SerializeField] MultiColumnHeaderState m_MultiColumnHeaderState;
		SearchField m_SearchField;      //�����ṩ��������
		MultiColumnTreeView m_TreeView;    //�Զ��������ͼ�࣬������ʾ�Ͳ�����������
		MyTreeAsset m_MyTreeAsset;      //���е�ǰ����ͼ���ݵ��ʲ�����

		//2. ���ڲ˵�����ʲ��򿪴���
		[MenuItem("TreeView Examples/Multi Columns")]
		public static MultiColumnWindow GetWindow ()
		{
			var window = GetWindow<MultiColumnWindow>();
			window.titleContent = new GUIContent("Multi Columns");
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
		Rect multiColumnTreeViewRect
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
		// ����һ���������ԣ����ڷ���˽���ֶ� m_TreeView
		public MultiColumnTreeView treeView
		{
			get { return m_TreeView; }
		}
		//InitIfNeeded ������鴰���Ƿ��ѳ�ʼ��
		void InitIfNeeded ()
		{
			if (!m_Initialized)
			{
				if (m_TreeViewState == null)
					m_TreeViewState = new TreeViewState();
				bool firstInit = m_MultiColumnHeaderState == null;
				
				// ����Ĭ�ϵ� MultiColumnHeaderState ʵ��
				var headerState = MultiColumnTreeView.CreateDefaultMultiColumnHeaderState(multiColumnTreeViewRect.width);
				
				// �����ǰ�� MultiColumnHeaderState ���Ա����ǣ�����и���
				if (MultiColumnHeaderState.CanOverwriteSerializedFields(m_MultiColumnHeaderState, headerState))
					MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
				m_MultiColumnHeaderState = headerState;
				
				// ʹ���µ� headerState ���� MyMultiColumnHeader ʵ��
				var multiColumnHeader = new MyMultiColumnHeader(headerState);
				if (firstInit)
					multiColumnHeader.ResizeToFit ();//����ǵ�һ�γ�ʼ���������п�����Ӧ����
				// ���� TreeModel ʵ������������
				var treeModel = new TreeModel<MyTreeElement>(GetData());
				
				// ʹ�� m_TreeViewState��multiColumnHeader �� treeModel ���� MultiColumnTreeView ʵ��
				m_TreeView = new MultiColumnTreeView(m_TreeViewState, multiColumnHeader, treeModel);
				
				m_SearchField = new SearchField();
				// ���� SearchField �ļ�ͷ�������¼���������ʹ�������ý��㲢ȷ��ѡ�е���
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
			SearchBar(toolbarRect);
			DoTreeView (multiColumnTreeViewRect);
			BottomToolBar (bottomToolbarRect);
		}
		//SearchBar() �������������򲢴��������ַ���
		void SearchBar (Rect rect)
		{
			treeView.searchString = m_SearchField.OnGUI (rect, treeView.searchString);
		}
		//DoTreeView() ������������ͼ
		void DoTreeView (Rect rect)
		{
			m_TreeView.OnGUI(rect);
		}
		void BottomToolBar (Rect rect)
		{
			GUILayout.BeginArea (rect);// ��ָ���ľ��������ڿ�ʼ���� GUI ���

			// ʹ��ˮƽ�����Ժ������а�ť�ͱ�ǩ
			using (new EditorGUILayout.HorizontalScope ())
			{
				var style = "miniButton";	// ���尴ť����ʽ
				if (GUILayout.Button("Expand All", style))
				{
					treeView.ExpandAll ();
				}
				if (GUILayout.Button("Collapse All", style))
				{
					treeView.CollapseAll ();
				}
				GUILayout.FlexibleSpace();// ��ӿ������ռ䣬ʹ��ť���ǩ֮�����ʵ��ļ��

				// ��ʾ��ǰ���ʲ���·�������û���ʲ�����ʾ���ַ���
				GUILayout.Label (m_MyTreeAsset != null ? AssetDatabase.GetAssetPath (m_MyTreeAsset) : string.Empty);

				GUILayout.FlexibleSpace ();

				if (GUILayout.Button("Set sorting", style))
				{
					var myColumnHeader = (MyMultiColumnHeader)treeView.multiColumnHeader;
					//�� 4 �а����������򣬵� 3 �а��ս������򣬵� 2 �а�����������
					myColumnHeader.SetSortingColumns (new int[] {4, 3, 2}, new[] {true, false, true});
					myColumnHeader.mode = MyMultiColumnHeader.Mode.LargeHeader;
				}
				GUILayout.Label ("Header: ", "minilabel");
				if (GUILayout.Button("Large", style))
				{
					var myColumnHeader = (MyMultiColumnHeader) treeView.multiColumnHeader;
					myColumnHeader.mode = MyMultiColumnHeader.Mode.LargeHeader;
				}
				if (GUILayout.Button("Default", style))
				{
					var myColumnHeader = (MyMultiColumnHeader)treeView.multiColumnHeader;
					myColumnHeader.mode = MyMultiColumnHeader.Mode.DefaultHeader;
				}
				if (GUILayout.Button("No sort", style))
				{
					var myColumnHeader = (MyMultiColumnHeader)treeView.multiColumnHeader;
					myColumnHeader.mode = MyMultiColumnHeader.Mode.MinimumHeaderWithoutSorting;
				}
				GUILayout.Space (10);   //�ڲ����в���һ���߶�Ϊ 10 ���صĿհ�����

				// ����һ����values <-> controls����ť�������л���ʾ�ؼ��Ĳ���
				if (GUILayout.Button("values <-> controls", style))
				{
					treeView.showControls = !treeView.showControls;
				}
			}
			GUILayout.EndArea();
		}
	}
	internal class MyMultiColumnHeader : MultiColumnHeader
	{
		Mode m_Mode;

		// ö�ٶ����˲�ͬ��ͷ��ģʽ
		public enum Mode
		{
			LargeHeader,
			DefaultHeader,
			MinimumHeaderWithoutSorting
		}
		// ���캯������ʼ�� MyMultiColumnHeader ��
		public MyMultiColumnHeader(MultiColumnHeaderState state)
			: base(state)
		{
			mode = Mode.DefaultHeader;
		}
		// �����������úͻ�ȡ��ǰ�ı���ģʽ
		public Mode mode
		{
			get
			{
				return m_Mode;
			}
			set
			{
				m_Mode = value;
				switch (m_Mode)
				{
					case Mode.LargeHeader:
						canSort = true;
						height = 37f;
						break;
					case Mode.DefaultHeader:
						canSort = true;
						height = DefaultGUI.defaultHeight;
						break;
					case Mode.MinimumHeaderWithoutSorting:
						canSort = false;
						height = DefaultGUI.minimumHeight;
						break;
				}
			}
		}
		// ��д ColumnHeaderGUI �������Զ����б���Ļ���
		protected override void ColumnHeaderGUI (MultiColumnHeaderState.Column column, Rect headerRect, int columnIndex)
		{
			// ���û����Ĭ�ϻ��Ʒ���
			base.ColumnHeaderGUI(column, headerRect, columnIndex);

			// �����ǰģʽ�� LargeHeader��ִ�ж���Ļ��Ʋ���
			if (mode == Mode.LargeHeader)
			{
				// ��ĳЩ������ʾʾ��������Ϣ
				if (columnIndex > 2)
				{
					headerRect.xMax -= 3f;
					// ���沢���ñ�ǩ�Ķ��뷽ʽ
					var oldAlignment = EditorStyles.largeLabel.alignment;
					EditorStyles.largeLabel.alignment = TextAnchor.UpperRight;
					// ����ʾ���ı�
					GUI.Label(headerRect, 36 + columnIndex + "%", EditorStyles.largeLabel);
					// �ָ�ԭ���Ķ��뷽ʽ
					EditorStyles.largeLabel.alignment = oldAlignment;
				}
			}
		}
	}
}

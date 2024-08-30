using System;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UnityEditor.TreeViewExamples
{
	class MultiColumnWindow : EditorWindow
	{
		//1. 字段和属性
		[NonSerialized] bool m_Initialized;     //用于标记窗口是否已初始化
		[SerializeField] TreeViewState m_TreeViewState; //用于保存树视图的状态
		[SerializeField] MultiColumnHeaderState m_MultiColumnHeaderState;
		SearchField m_SearchField;      //用于提供搜索功能
		MultiColumnTreeView m_TreeView;    //自定义的树视图类，用于显示和操作树形数据
		MyTreeAsset m_MyTreeAsset;      //持有当前树视图数据的资产对象

		//2. 窗口菜单项和资产打开处理
		[MenuItem("TreeView Examples/Multi Columns")]
		public static MultiColumnWindow GetWindow ()
		{
			var window = GetWindow<MultiColumnWindow>();
			window.titleContent = new GUIContent("Multi Columns");
			window.Focus();
			window.Repaint();
			return window;
		}
		//OnOpenAsset 方法允许当用户双击 MyTreeAsset 类型的资产时，自动打开 CustomHeightWindow 窗口并加载该资产的数据
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
		//设置树形视图的数据源
		void SetTreeAsset (MyTreeAsset myTreeAsset)
		{
			m_MyTreeAsset = myTreeAsset;
			m_Initialized = false;
		}
		//3. 窗口布局和初始化
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
		// 定义一个公共属性，用于访问私有字段 m_TreeView
		public MultiColumnTreeView treeView
		{
			get { return m_TreeView; }
		}
		//InitIfNeeded 方法检查窗口是否已初始化
		void InitIfNeeded ()
		{
			if (!m_Initialized)
			{
				if (m_TreeViewState == null)
					m_TreeViewState = new TreeViewState();
				bool firstInit = m_MultiColumnHeaderState == null;
				
				// 创建默认的 MultiColumnHeaderState 实例
				var headerState = MultiColumnTreeView.CreateDefaultMultiColumnHeaderState(multiColumnTreeViewRect.width);
				
				// 如果当前的 MultiColumnHeaderState 可以被覆盖，则进行覆盖
				if (MultiColumnHeaderState.CanOverwriteSerializedFields(m_MultiColumnHeaderState, headerState))
					MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
				m_MultiColumnHeaderState = headerState;
				
				// 使用新的 headerState 创建 MyMultiColumnHeader 实例
				var multiColumnHeader = new MyMultiColumnHeader(headerState);
				if (firstInit)
					multiColumnHeader.ResizeToFit ();//如果是第一次初始化，调整列宽以适应内容
				// 创建 TreeModel 实例，传入数据
				var treeModel = new TreeModel<MyTreeElement>(GetData());
				
				// 使用 m_TreeViewState、multiColumnHeader 和 treeModel 创建 MultiColumnTreeView 实例
				m_TreeView = new MultiColumnTreeView(m_TreeViewState, multiColumnHeader, treeModel);
				
				m_SearchField = new SearchField();
				// 设置 SearchField 的箭头键按下事件处理器，使其能设置焦点并确保选中的项
				m_SearchField.downOrUpArrowKeyPressed += m_TreeView.SetFocusAndEnsureSelectedItem;
				
				m_Initialized = true;
			}
		}
		//4. 数据获取和选择变化处理
		IList<MyTreeElement> GetData ()
		{
			//GetData() 方法获取树视图的数据
			if (m_MyTreeAsset != null && m_MyTreeAsset.treeElements != null && m_MyTreeAsset.treeElements.Count > 0)
				return m_MyTreeAsset.treeElements;

			// 生成一些随机数据
			return MyTreeElementGenerator.GenerateRandomTree(130); 
		}
		void OnSelectionChange ()
		{
			//OnSelectionChange() 方法在选择变化时更新树视图数据
			if (!m_Initialized)
				return;

			var myTreeAsset = Selection.activeObject as MyTreeAsset;
			if (myTreeAsset != null && myTreeAsset != m_MyTreeAsset)
			{
				//更新树视图并重新加载
				m_MyTreeAsset = myTreeAsset;
				m_TreeView.treeModel.SetData (GetData ());
				m_TreeView.Reload ();
			}
		}
		//5. GUI 绘制
		void OnGUI ()
		{
			InitIfNeeded();     //调用 InitIfNeeded 以确保初始化
			SearchBar(toolbarRect);
			DoTreeView (multiColumnTreeViewRect);
			BottomToolBar (bottomToolbarRect);
		}
		//SearchBar() 方法绘制搜索框并处理搜索字符串
		void SearchBar (Rect rect)
		{
			treeView.searchString = m_SearchField.OnGUI (rect, treeView.searchString);
		}
		//DoTreeView() 方法绘制树视图
		void DoTreeView (Rect rect)
		{
			m_TreeView.OnGUI(rect);
		}
		void BottomToolBar (Rect rect)
		{
			GUILayout.BeginArea (rect);// 在指定的矩形区域内开始绘制 GUI 组件

			// 使用水平布局以横向排列按钮和标签
			using (new EditorGUILayout.HorizontalScope ())
			{
				var style = "miniButton";	// 定义按钮的样式
				if (GUILayout.Button("Expand All", style))
				{
					treeView.ExpandAll ();
				}
				if (GUILayout.Button("Collapse All", style))
				{
					treeView.CollapseAll ();
				}
				GUILayout.FlexibleSpace();// 添加可伸缩空间，使按钮与标签之间有适当的间距

				// 显示当前树资产的路径，如果没有资产则显示空字符串
				GUILayout.Label (m_MyTreeAsset != null ? AssetDatabase.GetAssetPath (m_MyTreeAsset) : string.Empty);

				GUILayout.FlexibleSpace ();

				if (GUILayout.Button("Set sorting", style))
				{
					var myColumnHeader = (MyMultiColumnHeader)treeView.multiColumnHeader;
					//第 4 列按照升序排序，第 3 列按照降序排序，第 2 列按照升序排序
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
				GUILayout.Space (10);   //在布局中插入一个高度为 10 像素的空白区域

				// 创建一个“values <-> controls”按钮，并绑定切换显示控件的操作
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

		// 枚举定义了不同的头部模式
		public enum Mode
		{
			LargeHeader,
			DefaultHeader,
			MinimumHeaderWithoutSorting
		}
		// 构造函数，初始化 MyMultiColumnHeader 类
		public MyMultiColumnHeader(MultiColumnHeaderState state)
			: base(state)
		{
			mode = Mode.DefaultHeader;
		}
		// 属性用于设置和获取当前的标题模式
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
		// 重写 ColumnHeaderGUI 方法以自定义列标题的绘制
		protected override void ColumnHeaderGUI (MultiColumnHeaderState.Column column, Rect headerRect, int columnIndex)
		{
			// 调用基类的默认绘制方法
			base.ColumnHeaderGUI(column, headerRect, columnIndex);

			// 如果当前模式是 LargeHeader，执行额外的绘制操作
			if (mode == Mode.LargeHeader)
			{
				// 在某些列上显示示例叠加信息
				if (columnIndex > 2)
				{
					headerRect.xMax -= 3f;
					// 保存并设置标签的对齐方式
					var oldAlignment = EditorStyles.largeLabel.alignment;
					EditorStyles.largeLabel.alignment = TextAnchor.UpperRight;
					// 绘制示例文本
					GUI.Label(headerRect, 36 + columnIndex + "%", EditorStyles.largeLabel);
					// 恢复原来的对齐方式
					EditorStyles.largeLabel.alignment = oldAlignment;
				}
			}
		}
	}
}

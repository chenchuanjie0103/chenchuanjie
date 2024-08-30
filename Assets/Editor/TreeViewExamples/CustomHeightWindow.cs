using System;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UnityEditor.TreeViewExamples
{
	class CustomHeightWindow : EditorWindow
	{
		//1. 字段和属性
		[NonSerialized] bool m_Initialized;     //用于标记窗口是否已初始化
		[SerializeField] TreeViewState m_TreeViewState; //用于保存树视图的状态
		SearchField m_SearchField;		//用于提供搜索功能
		CustomHeightTreeView m_TreeView;    //自定义的树视图类，用于显示和操作树形数据
		MyTreeAsset m_MyTreeAsset;      //持有当前树视图数据的资产对象

		//2. 窗口菜单项和资产打开处理
		[MenuItem("TreeView Examples/Custom Row Heights")]
		public static CustomHeightWindow GetWindow()
		{
			var window = GetWindow<CustomHeightWindow>();
			window.titleContent = new GUIContent("Custom Heights");
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
		//InitIfNeeded 方法检查窗口是否已初始化
		void InitIfNeeded ()
		{
			if (!m_Initialized)
			{
				//未初始化则创建 TreeViewState、TreeModel 和 CustomHeightTreeView 实例，并设置搜索字段的事件处理器
				if (m_TreeViewState == null)
					m_TreeViewState = new TreeViewState();
					
				var treeModel = new TreeModel<MyTreeElement>(GetData());
				m_TreeView = new CustomHeightTreeView(m_TreeViewState, treeModel);

				m_SearchField = new SearchField ();
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
			SearchBar (toolbarRect);
			DoTreeView(treeViewRect);
			BottomToolBar (bottomToolbarRect);
		}
		//SearchBar() 方法绘制搜索框并处理搜索字符串
		void SearchBar (Rect rect)
		{
			m_TreeView.searchString = m_SearchField.OnGUI(rect, m_TreeView.searchString);
		}
		//DoTreeView() 方法绘制树视图
		void DoTreeView (Rect rect)
		{
			m_TreeView.OnGUI(rect);
		}
		//BottomToolBar() 方法绘制底部工具栏，并提供“展开全部”和“折叠全部”按钮来控制树视图的节点状态
		void BottomToolBar (Rect rect)
		{
			GUILayout.BeginArea (rect);// 在指定的矩形区域内开始绘制 GUI 组件
			// 使用水平布局以横向排列按钮和标签
			using (new EditorGUILayout.HorizontalScope ())
			{
				var style = "miniButton";   // 定义按钮的样式
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

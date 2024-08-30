using UnityEditor.IMGUI.Controls;
using UnityEngine;


namespace UnityEditor.TreeViewExamples
{
	//1. 类定义
	internal class CustomHeightTreeView : TreeViewWithTreeModel<MyTreeElement>
	{
		// 内部静态类，用于定义 GUI 样式
		static class Styles
		{
			public static GUIStyle background = "RL Background";
			public static GUIStyle headerBackground = "RL Header";
		}
		// 构造函数：初始化了树视图的状态，包括设置边框和折叠偏移量，然后重新加载树视图
		public CustomHeightTreeView(TreeViewState state, TreeModel<MyTreeElement> model)
			: base(state, model)
		{
			showBorder = true;
			customFoldoutYOffset = 3f;
			Reload();
		}
		//2. 自定义行高度
		protected override float GetCustomRowHeight (int row, TreeViewItem item)
		{
			var myItem = (TreeViewItem<MyTreeElement>)item;
			
			if (myItem.data.enabled)
				return 85f;
			
			return 30f;
		}
		//3. 自定义绘制
		public override void OnGUI (Rect rect)
		{
			// Background
			if (Event.current.type == EventType.Repaint)
				DefaultStyles.backgroundOdd.Draw(rect, false, false, false, false);
			// TreeView
			base.OnGUI (rect);
		}
		//4. 行绘制
		protected override void RowGUI (RowGUIArgs args)
		{
			var item = (TreeViewItem<MyTreeElement>) args.item;
			var contentIndent = GetContentIndent (item);

			// Background
			var bgRect = args.rowRect;
			bgRect.x = contentIndent;
			bgRect.width = Mathf.Max (bgRect.width - contentIndent, 155f) - 5f;
			bgRect.yMin += 2f; //顶部向下移动
			bgRect.yMax -= 2f;
			DrawItemBackground(bgRect);

			// Custom label
			var headerRect = bgRect;
			headerRect.xMin += 5f; //左边界向右移动
			headerRect.xMax -= 10f;
			headerRect.height = Styles.headerBackground.fixedHeight;
			HeaderGUI (headerRect, args.label, item);

			// Controls
			var controlsRect = headerRect;
			controlsRect.xMin += 20f;
			controlsRect.y += headerRect.height;
			if (item.data.enabled)
				ControlsGUI (controlsRect, item);
		}
		//5. 背景绘制
		void DrawItemBackground (Rect bgRect)
		{
			if (Event.current.type == EventType.Repaint)
			{
				var rect = bgRect;
				rect.height = Styles.headerBackground.fixedHeight;
				Styles.headerBackground.Draw(rect, false, false, false, false);
				//先绘制标题部分的背景，然后绘制剩余的背景
				rect.y += rect.height;
				rect.height = bgRect.height - rect.height;
				Styles.background.Draw(rect, false, false, false, false);
			}
		}
		//6. 标题绘制

		//HeaderGUI 方法绘制行的标题部分，包括一个可切换的开关和标签
		void HeaderGUI (Rect headerRect, string label, TreeViewItem<MyTreeElement> item)
		{
			headerRect.y += 1f;

			Rect toggleRect = headerRect;
			toggleRect.width = 16;
			EditorGUI.BeginChangeCheck ();
			//使用 EditorGUI.Toggle 来绘制一个开关，并在状态变化时刷新行高
			item.data.enabled = EditorGUI.Toggle(toggleRect, item.data.enabled);
			if (EditorGUI.EndChangeCheck ())
				RefreshCustomRowHeights ();

			Rect labelRect = headerRect;
			labelRect.xMin += toggleRect.width + 2f;
			GUI.Label (labelRect, label);
		}
		//7. 控件绘制
		void ControlsGUI(Rect controlsRect, TreeViewItem<MyTreeElement> item)
		{
			var rect = controlsRect;
			rect.y += 3f;
			rect.height = EditorGUIUtility.singleLineHeight;
			item.data.floatValue1 = EditorGUI.Slider(rect, GUIContent.none, item.data.floatValue1, 0f, 1f);
			rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;
			item.data.material = (Material)EditorGUI.ObjectField(rect, GUIContent.none, item.data.material, typeof(Material), false);
			rect.y += rect.height + EditorGUIUtility.standardVerticalSpacing;
			item.data.text = GUI.TextField(rect, item.data.text);
		}
		//8. 重命名处理

		//GetRenameRect 方法调整了重命名区域的位置，以适应可能被其他列遮挡的情况
		protected override Rect GetRenameRect (Rect rowRect, int row, TreeViewItem item)
		{
			// 调用基类方法获取默认的重命名区域
			var renameRect = base.GetRenameRect (rowRect, row, item);
			renameRect.xMin += 25f;
			renameRect.y += 2f;
			return renameRect;
		}

		//CanRename 方法检查是否可以重命名，通过判断重命名区域的宽度
		protected override bool CanRename(TreeViewItem item)
		{
			// 调用 GetRenameRect 方法获取重命名区域的矩形
			// 传入 treeViewRect (树视图的矩形边界), 0 (行索引), item (当前项)
			Rect renameRect = GetRenameRect (treeViewRect, 0, item);
			return renameRect.width > 30;
		}

		//RenameEnded 方法处理重命名结束时的逻辑，将新名称应用到 MyTreeElement 并重新加载树视图
		protected override void RenameEnded(RenameEndedArgs args)
		{   
			if (args.acceptedRename)
			{
				var element = treeModel.Find(args.itemID);
				element.name = args.newName;
				Reload();
			}
		}
	}
}

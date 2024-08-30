using UnityEditor.IMGUI.Controls;
using UnityEngine;


namespace UnityEditor.TreeViewExamples
{
	//1. �ඨ��
	internal class CustomHeightTreeView : TreeViewWithTreeModel<MyTreeElement>
	{
		// �ڲ���̬�࣬���ڶ��� GUI ��ʽ
		static class Styles
		{
			public static GUIStyle background = "RL Background";
			public static GUIStyle headerBackground = "RL Header";
		}
		// ���캯������ʼ��������ͼ��״̬���������ñ߿���۵�ƫ������Ȼ�����¼�������ͼ
		public CustomHeightTreeView(TreeViewState state, TreeModel<MyTreeElement> model)
			: base(state, model)
		{
			showBorder = true;
			customFoldoutYOffset = 3f;
			Reload();
		}
		//2. �Զ����и߶�
		protected override float GetCustomRowHeight (int row, TreeViewItem item)
		{
			var myItem = (TreeViewItem<MyTreeElement>)item;
			
			if (myItem.data.enabled)
				return 85f;
			
			return 30f;
		}
		//3. �Զ������
		public override void OnGUI (Rect rect)
		{
			// Background
			if (Event.current.type == EventType.Repaint)
				DefaultStyles.backgroundOdd.Draw(rect, false, false, false, false);
			// TreeView
			base.OnGUI (rect);
		}
		//4. �л���
		protected override void RowGUI (RowGUIArgs args)
		{
			var item = (TreeViewItem<MyTreeElement>) args.item;
			var contentIndent = GetContentIndent (item);

			// Background
			var bgRect = args.rowRect;
			bgRect.x = contentIndent;
			bgRect.width = Mathf.Max (bgRect.width - contentIndent, 155f) - 5f;
			bgRect.yMin += 2f; //���������ƶ�
			bgRect.yMax -= 2f;
			DrawItemBackground(bgRect);

			// Custom label
			var headerRect = bgRect;
			headerRect.xMin += 5f; //��߽������ƶ�
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
		//5. ��������
		void DrawItemBackground (Rect bgRect)
		{
			if (Event.current.type == EventType.Repaint)
			{
				var rect = bgRect;
				rect.height = Styles.headerBackground.fixedHeight;
				Styles.headerBackground.Draw(rect, false, false, false, false);
				//�Ȼ��Ʊ��ⲿ�ֵı�����Ȼ�����ʣ��ı���
				rect.y += rect.height;
				rect.height = bgRect.height - rect.height;
				Styles.background.Draw(rect, false, false, false, false);
			}
		}
		//6. �������

		//HeaderGUI ���������еı��ⲿ�֣�����һ�����л��Ŀ��غͱ�ǩ
		void HeaderGUI (Rect headerRect, string label, TreeViewItem<MyTreeElement> item)
		{
			headerRect.y += 1f;

			Rect toggleRect = headerRect;
			toggleRect.width = 16;
			EditorGUI.BeginChangeCheck ();
			//ʹ�� EditorGUI.Toggle ������һ�����أ�����״̬�仯ʱˢ���и�
			item.data.enabled = EditorGUI.Toggle(toggleRect, item.data.enabled);
			if (EditorGUI.EndChangeCheck ())
				RefreshCustomRowHeights ();

			Rect labelRect = headerRect;
			labelRect.xMin += toggleRect.width + 2f;
			GUI.Label (labelRect, label);
		}
		//7. �ؼ�����
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
		//8. ����������

		//GetRenameRect ���������������������λ�ã�����Ӧ���ܱ��������ڵ������
		protected override Rect GetRenameRect (Rect rowRect, int row, TreeViewItem item)
		{
			// ���û��෽����ȡĬ�ϵ�����������
			var renameRect = base.GetRenameRect (rowRect, row, item);
			renameRect.xMin += 25f;
			renameRect.y += 2f;
			return renameRect;
		}

		//CanRename ��������Ƿ������������ͨ���ж�����������Ŀ��
		protected override bool CanRename(TreeViewItem item)
		{
			// ���� GetRenameRect ������ȡ����������ľ���
			// ���� treeViewRect (����ͼ�ľ��α߽�), 0 (������), item (��ǰ��)
			Rect renameRect = GetRenameRect (treeViewRect, 0, item);
			return renameRect.width > 30;
		}

		//RenameEnded ������������������ʱ���߼�����������Ӧ�õ� MyTreeElement �����¼�������ͼ
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

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityObject = UnityEngine.Object;

namespace UnityEditor.TreeViewExamples
{
	class TransformTreeView : TreeView
	{
		const float kRowHeights = 20f;
		const float kToggleWidth = 18f;
		public bool showControls = true;
		private int m_sortedColumnIndex = -1;
		private bool m_isAscending = true;
		public TransformTreeView(TreeViewState state, MultiColumnHeader multicolumnHeader)
			: base(state, multicolumnHeader)
		{
			// �Զ�������
			showAlternatingRowBackgrounds = true;
			showBorder = true;
			rowHeight = kRowHeights;
			columnIndexForTreeFoldouts = 0;
			customFoldoutYOffset = (kRowHeights - EditorGUIUtility.singleLineHeight) * 0.5f; // center foldout in the row since we also center content. See RowGUI
			extraSpaceBeforeIconAndLabel = kToggleWidth;
			multicolumnHeader.sortingChanged += OnSortingChangeds;
			Reload();
		}//��ʼ��ʵ�������ݲ���
		void OnSortingChanged(MultiColumnHeader multiColumnHeader)
		{
			SortIfNeeded(rootItem, GetRows());
		}
		void SortIfNeeded(TreeViewItem root, IList<TreeViewItem> rows)
		{
			if (rows.Count <= 1)
				return;

			if (multiColumnHeader.sortedColumnIndex == -1)
			{
				return;
			}
			Repaint();
		}
		protected override TreeViewItem BuildRoot()        // ��������ͼ�ĸ��ڵ�
		{
			return new TreeViewItem { id = 0, depth = -1 }; // ����һ�����ڵ�
		}
		protected override IList<TreeViewItem> BuildRows(TreeViewItem root)   // ��������ͼ���У��ڵ㣩
		{
			var rows = GetRows() ?? new List<TreeViewItem>(200); // ��ȡ�����У����������򴴽����б�
			Scene scene = SceneManager.GetSceneAt(0); // ��ȡ��ǰ����
			rows.Clear();
			root = new TreeViewItem { id = 0, depth = -1 };//���³�ʼ�����ڵ�
			var gameObjectRoots = scene.GetRootGameObjects(); // ��ȡ�����еĸ���Ϸ����
			foreach (var gameObject in gameObjectRoots)
			{
				var item = CreateTreeViewItemForGameObject(gameObject);
				//��ʾ����������������Ϸ����
				if (MatchesSearchString(gameObject))
				{
					root.AddChild(item);
					rows.Add(item);
				}
				if (gameObject.transform.childCount > 0)
				{
					if (IsExpanded(item.id)) // ����ڵ���չ��
					{
						AddChildrenRecursive(gameObject, item, rows);
					}
					else
					{
						item.children = CreateChildListForCollapsedParent(); // �����۵���������б�
					}
				}
			}
			if (m_sortedColumnIndex >= 0)
			{
				if (string.IsNullOrEmpty(searchString))
				{
					(root, rows) = ApplySorting(root, rows);
				}
				else
				{
					rows = Sort(rows.ToList());
				}
			}
			SetupDepthsFromParentsAndChildren(root); // ���������Ϣ
			return rows;
		}


		(TreeViewItem, IList<TreeViewItem>) ApplySorting(TreeViewItem root, IList<TreeViewItem> rows)
		{
			rows.Clear();
			void SortAndAdd(List<TreeViewItem> items)
			{
				var sortedItems = Sort(items);
				foreach (var item in sortedItems)
				{
					rows.Add(item);
					if (IsExpanded(item.id))	// ���������չ���ģ���ݹ�ض����ӽڵ������������
					{
						SortAndAdd(item.children);
					}
				}
			}
			// ��ʼ�ݹ�������ڵ������
			SortAndAdd(root.children);
			return (root, rows);
		}
		List<TreeViewItem> Sort(List<TreeViewItem> rows)
		{
			switch (m_sortedColumnIndex)
			{
				case 0:
					return m_isAscending
						? rows.Where(item => item is HTreeViewItem)
							.OrderBy(item => ((HTreeViewItem)item).displayName, StringComparer.OrdinalIgnoreCase).ToList()
						: rows.Where(item => item is HTreeViewItem)
							.OrderByDescending(item => ((HTreeViewItem)item).displayName, StringComparer.OrdinalIgnoreCase).ToList();
				case 1:
					return m_isAscending
						? rows.Where(item => item is HTreeViewItem)
							.OrderBy(item => ((HTreeViewItem)item).childCount).ToList()
						: rows.Where(item => item is HTreeViewItem)
							.OrderByDescending(item => ((HTreeViewItem)item).childCount).ToList();
				case 2:
					return m_isAscending
						? rows.Where(item => item is HTreeViewItem)
							.OrderBy(item => ((HTreeViewItem)item).componentCount).ToList()
						: rows.Where(item => item is HTreeViewItem)
							.OrderByDescending(item => ((HTreeViewItem)item).componentCount).ToList();
				default:
					return rows;
			}
		}//���ݸ������������������򣬶�TreeView���б��������
		void OnSortingChangeds(MultiColumnHeader multiColumnHeader)
		{
			// ����������������������� 
			m_sortedColumnIndex = multiColumnHeader.sortedColumnIndex;
			m_isAscending = multiColumnHeader.IsSortedAscending(m_sortedColumnIndex);
			Reload();
		}//�������л����������仯ʱ���ã����ڸ����������ò����¼�������


		void AddChildrenRecursive(GameObject go, TreeViewItem item, IList<TreeViewItem> rows)
		{
			int childCount = go.transform.childCount;
			item.children = new List<TreeViewItem>(childCount);
			for (int i = 0; i < childCount; ++i)
			{
				var childTransform = go.transform.GetChild(i);
				var childItem = CreateTreeViewItemForGameObject(childTransform.gameObject);
				//��ʾ����������������Ϸ����:������ǰ������������������
				if (MatchesSearchString(childTransform.gameObject))
				{
					item.AddChild(childItem);
					rows.Add(childItem);
				}
				if (childTransform.childCount > 0)
				{
					if (IsExpanded(childItem.id))
					{
						AddChildrenRecursive(childTransform.gameObject, childItem, rows);
					}
					else
					{
						childItem.children = CreateChildListForCollapsedParent();
					}
				}
			}
		}//�ݹ������ӽڵ�
		protected override void SelectionChanged(IList<int> selectedIds)
		{
			if (selectedIds.Count > 0)
			{
				int instanceID = selectedIds[0];
				GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
				if (gameObject != null)
				{
					EditorGUIUtility.PingObject(gameObject);
					Selection.activeGameObject = gameObject;
				}
			}
		}//ѡ�и���
		bool MatchesSearchString(GameObject gameObject)
		{
			return string.IsNullOrEmpty(searchString) || gameObject.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0;
		}// ������Ϸ�����Ƿ���һ�������ַ���		 


		static TreeViewItem CreateTreeViewItemForGameObject(GameObject gameObject)
		{
			var item = new HTreeViewItem
			{
				id = gameObject.GetInstanceID(),
				depth = -1,
				displayName = gameObject.name,
				childCount = GetTotalChildCount(gameObject),
				componentCount = GetTotalComponentCount(gameObject)
			};

			return item;
		}// ������Ϸ���󴴽� TreeViewItem
		GameObject GetGameObject(int instanceID)
		{
			return (GameObject)EditorUtility.InstanceIDToObject(instanceID); // ת�� ID ����Ϸ����
		}// ͨ��ʵ��ID��ȡ��Ӧ����Ϸ����
		static int GetTotalChildCount(GameObject gameObject)
		{
			int totalCount = 1;
			var stack = new Stack<Transform>();
			stack.Push(gameObject.transform);

			while (stack.Count > 0)
			{
				var current = stack.Pop();
				totalCount += current.childCount;

				for (int i = 0; i < current.childCount; ++i)
				{
					stack.Push(current.GetChild(i));
				}
			}
			return totalCount;
		}
		static int GetTotalComponentCount(GameObject gameObject)
		{
			int totalCount = gameObject.GetComponents<Component>().Length;
			var stack = new Stack<Transform>();
			stack.Push(gameObject.transform);

			while (stack.Count > 0)
			{
				var current = stack.Pop();

				for (int i = 0; i < current.childCount; ++i)
				{
					var child = current.GetChild(i);
					totalCount += child.gameObject.GetComponents<Component>().Length;
					stack.Push(child);
				}
			}
			return totalCount;
		}


		// ��Ⱦ�� GUI �ķ���
		protected override void RowGUI(RowGUIArgs args)
		{
			//��ȡ��ǰ�е���
			var item = args.item as HTreeViewItem;
			if (item == null)
				return;
			float margin = 20f + (item.depth * 10f);		//�������߾�
			int visibleColumns = args.GetNumVisibleColumns();		//��ȡ�ɼ�����
			//����ÿһ�е�����
			for (int i = 0; i < visibleColumns; i++)
			{
				Rect cRect = args.GetCellRect(i);
				// �����е����λ��
				if (string.IsNullOrEmpty(searchString))
				{
					if (i == 0)
					{
						cRect.x += margin;
					}
					else
					{
						cRect.width -= 10f;
					}
				}
				string content = GetColumnContent(item, i);
				if (!string.IsNullOrEmpty(content))
				{
					GUI.Label(cRect, content);
				}

			}
		}// ����ѡ��仯
		private string GetColumnContent(HTreeViewItem item, int columnIndex)
		{
			return columnIndex switch
			{
				0 => item.displayName,
				1 => item.childCount.ToString(),
				2 => item.componentCount.ToString(),
				_ => null
			};
		}// ����ÿ������	




		// ��. �ж��Ƿ���Կ�ʼ��ק
		protected override bool CanStartDrag(CanStartDragArgs args)
		{
			return true;
		}
		// ���ϷŲ�����ʼʱ���ô˷�����׼���ϷŲ���
		protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
		{
			// ׼����ʼ�϶�����
			DragAndDrop.PrepareStartDrag();

			// ������˳����϶����� ID ��������
			var sortedDraggedIDs = SortItemIDsInRowOrder(args.draggedItemIDs);

			// ����һ�� UnityObject �б����洢�����Ķ���
			List<UnityObject> objList = new List<UnityObject>(sortedDraggedIDs.Count);

			// ���������� ID������ת��Ϊ UnityObject������ӵ��б���
			foreach (var id in sortedDraggedIDs)
			{
				UnityObject obj = EditorUtility.InstanceIDToObject(id);
				if (obj != null)
					objList.Add(obj);
			}

			// �����϶����������
			DragAndDrop.objectReferences = objList.ToArray();

			// ȷ���϶��ı��⣬����ж����������ʾ "<Multiple>"
			string title = objList.Count > 1 ? "<Multiple>" : objList[0].name;
			DragAndDrop.StartDrag(title);
		}

		// �����ϷŲ������߼�
		protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
		{
			// ��ȡ��ǰ�϶��Ķ���
			var draggedObjects = DragAndDrop.objectReferences;
			var transforms = new List<Transform>(draggedObjects.Length);

			// ���϶��Ķ���ת��Ϊ Transform ����ӵ��б���
			foreach (var obj in draggedObjects)
			{
				var go = obj as GameObject;
				if (go == null)
				{
					return DragAndDropVisualMode.None;
				}
				transforms.Add(go.transform);
			}

			// �Ƴ���Щ�������϶�������������
			RemoveItemsThatAreDescendantsFromOtherItems(transforms);

			if (args.performDrop)
			{
				// �����Ϸ�λ�õĲ�ͬ�����Ϸ��߼�
				switch (args.dragAndDropPosition)
				{
					case DragAndDropPosition.UponItem:
					case DragAndDropPosition.BetweenItems:
						// ��ȡ����� Transform
						Transform parent = args.parentItem != null ? GetGameObject(args.parentItem.id).transform : null;


						// ��������Ӷ� parent Ϊ null �ļ��  
						if (parent == null && transforms.Count > 0)
						{
							// ��� parent Ϊ null�����϶��Ķ�����ڣ�������ҪĬ����Ϊ�������  
							// ���磬����ѡ��һ��Ĭ�ϸ��������ʾ������Ϣ  
							Debug.LogError("Cannot drop items without a valid parent.");
							return DragAndDropVisualMode.None;
						}


						// ��֤�Ƿ�������¸���
						if (!IsValidReparenting(parent, transforms))
							return DragAndDropVisualMode.None;

						// �������� Transform �ĸ���
						foreach (var trans in transforms)
							trans.SetParent(parent);

						// ���������֮���Ϸţ��������������
						if (args.dragAndDropPosition == DragAndDropPosition.BetweenItems)
						{
							int insertIndex = args.insertAtIndex;
							for (int i = transforms.Count - 1; i >= 0; i--)
							{
								var transform = transforms[i];
								insertIndex = GetAdjustedInsertIndex(parent, transform, insertIndex);
								transform.SetSiblingIndex(insertIndex);
							}
						}
						break;

					case DragAndDropPosition.OutsideItems:
						// �϶������ⲿ��ȡ������
						foreach (var trans in transforms)
						{
							trans.SetParent(null);
						}
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				// ���¼�����ͼ������ѡ��
				Reload();
				SetSelection(transforms.Select(t => t.gameObject.GetInstanceID()).ToList(), TreeViewSelectionOptions.RevealAndFrame);
			}

			return DragAndDropVisualMode.Move;
		}

		// ��ȡ������Ĳ�������
		int GetAdjustedInsertIndex(Transform parent, Transform transformToInsert, int insertIndex)
		{
			// �����ǰ����ֵ�����С�ڲ��������������������һ
			if (transformToInsert.parent == parent && transformToInsert.GetSiblingIndex() < insertIndex)
				return --insertIndex;
			return insertIndex;
		}

		// ��֤���¸��������Ƿ�Ϸ�
		bool IsValidReparenting(Transform parent, List<Transform> transformsToMove)
		{
			if (parent == null)
				return true;
			foreach (var transformToMove in transformsToMove)
			{
				if (transformToMove == parent)
					return false;
				if (IsHoveredAChildOfDragged(parent, transformToMove))
					return false;
			}
			return true;
		}

		// ���ĳ�� Transform �Ƿ�����һ�� Transform ������
		bool IsHoveredAChildOfDragged(Transform hovered, Transform dragged)
		{
			Transform t = hovered.parent;
			while (t)
			{
				if (t == dragged)
					return true;
				t = t.parent;
			}
			return false;
		}

		// ���ĳ�� Transform �Ƿ���ָ�� Transform �б������
		static bool IsDescendantOf(Transform transform, List<Transform> transforms)
		{
			while (transform != null)
			{
				transform = transform.parent;
				if (transforms.Contains(transform))
					return true;
			}
			return false;
		}

		// �Ƴ��б�����Щ���������������
		static void RemoveItemsThatAreDescendantsFromOtherItems(List<Transform> transforms)
		{
			transforms.RemoveAll(t => IsDescendantOf(t, transforms));
		}
	}
	class HTreeViewItem : TreeViewItem
	{
		public int childCount;
		public int componentCount;
	}
	class MMultiColumnHeader : MultiColumnHeader
	{
		public MMultiColumnHeader() : base(CreateDefaultMultiColumnHeaderState())
		{
		}
		private static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState()
		{
			var columns = new[]
			{
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Name"),
					width = 200,
					minWidth = 100,
					maxWidth = 400,
					canSort = true,
					autoResize = true
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("GameObject����"),
					width = 150,
					minWidth = 50,
					maxWidth = 200,
					canSort = true,
					autoResize = true
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Component����"),
					width = 150,
					minWidth = 50,
					maxWidth = 200,
					canSort = true,
					autoResize = true
				}
			};
			var state = new MultiColumnHeaderState(columns);
			return state;
		}
	}
}

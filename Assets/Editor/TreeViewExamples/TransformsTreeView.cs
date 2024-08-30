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
			// 自定义设置
			showAlternatingRowBackgrounds = true;
			showBorder = true;
			rowHeight = kRowHeights;
			columnIndexForTreeFoldouts = 0;
			customFoldoutYOffset = (kRowHeights - EditorGUIUtility.singleLineHeight) * 0.5f; // center foldout in the row since we also center content. See RowGUI
			extraSpaceBeforeIconAndLabel = kToggleWidth;
			multicolumnHeader.sortingChanged += OnSortingChangeds;
			Reload();
		}//初始化实例，传递参数
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
		protected override TreeViewItem BuildRoot()        // 构建树视图的根节点
		{
			return new TreeViewItem { id = 0, depth = -1 }; // 创建一个根节点
		}
		protected override IList<TreeViewItem> BuildRows(TreeViewItem root)   // 构建树视图的行（节点）
		{
			var rows = GetRows() ?? new List<TreeViewItem>(200); // 获取现有行，若不存在则创建新列表
			Scene scene = SceneManager.GetSceneAt(0); // 获取当前场景
			rows.Clear();
			root = new TreeViewItem { id = 0, depth = -1 };//重新初始化根节点
			var gameObjectRoots = scene.GetRootGameObjects(); // 获取场景中的根游戏对象
			foreach (var gameObject in gameObjectRoots)
			{
				var item = CreateTreeViewItemForGameObject(gameObject);
				//显示符合搜索条件的游戏对象
				if (MatchesSearchString(gameObject))
				{
					root.AddChild(item);
					rows.Add(item);
				}
				if (gameObject.transform.childCount > 0)
				{
					if (IsExpanded(item.id)) // 如果节点已展开
					{
						AddChildrenRecursive(gameObject, item, rows);
					}
					else
					{
						item.children = CreateChildListForCollapsedParent(); // 创建折叠父项的子列表
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
			SetupDepthsFromParentsAndChildren(root); // 设置深度信息
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
					if (IsExpanded(item.id))	// 如果该项是展开的，则递归地对其子节点进行排序和添加
					{
						SortAndAdd(item.children);
					}
				}
			}
			// 开始递归排序根节点的子项
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
		}//根据给定的列索引和排序方向，对TreeView项列表进行排序
		void OnSortingChangeds(MultiColumnHeader multiColumnHeader)
		{
			// 更新排序的列索引和排序方向 
			m_sortedColumnIndex = multiColumnHeader.sortedColumnIndex;
			m_isAscending = multiColumnHeader.IsSortedAscending(m_sortedColumnIndex);
			Reload();
		}//当排序列或排序方向发生变化时调用，用于更新排序设置并重新加载数据


		void AddChildrenRecursive(GameObject go, TreeViewItem item, IList<TreeViewItem> rows)
		{
			int childCount = go.transform.childCount;
			item.children = new List<TreeViewItem>(childCount);
			for (int i = 0; i < childCount; ++i)
			{
				var childTransform = go.transform.GetChild(i);
				var childItem = CreateTreeViewItemForGameObject(childTransform.gameObject);
				//显示符合搜索条件的游戏对象:包括当前项和其符合条件的子项
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
		}//递归增加子节点
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
		}//选中高亮
		bool MatchesSearchString(GameObject gameObject)
		{
			return string.IsNullOrEmpty(searchString) || gameObject.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0;
		}// 检查的游戏对象是否有一个搜索字符串		 


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
		}// 根据游戏对象创建 TreeViewItem
		GameObject GetGameObject(int instanceID)
		{
			return (GameObject)EditorUtility.InstanceIDToObject(instanceID); // 转换 ID 到游戏对象
		}// 通过实例ID获取对应的游戏对象
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


		// 渲染行 GUI 的方法
		protected override void RowGUI(RowGUIArgs args)
		{
			//获取当前行的项
			var item = args.item as HTreeViewItem;
			if (item == null)
				return;
			float margin = 20f + (item.depth * 10f);		//计算左侧边距
			int visibleColumns = args.GetNumVisibleColumns();		//获取可见列数
			//绘制每一列的内容
			for (int i = 0; i < visibleColumns; i++)
			{
				Rect cRect = args.GetCellRect(i);
				// 调整列的左侧位置
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
		}// 处理选择变化
		private string GetColumnContent(HTreeViewItem item, int columnIndex)
		{
			return columnIndex switch
			{
				0 => item.displayName,
				1 => item.childCount.ToString(),
				2 => item.componentCount.ToString(),
				_ => null
			};
		}// 绘制每列内容	




		// 二. 判断是否可以开始拖拽
		protected override bool CanStartDrag(CanStartDragArgs args)
		{
			return true;
		}
		// 在拖放操作开始时调用此方法来准备拖放操作
		protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
		{
			// 准备开始拖动操作
			DragAndDrop.PrepareStartDrag();

			// 根据行顺序对拖动的项 ID 进行排序
			var sortedDraggedIDs = SortItemIDsInRowOrder(args.draggedItemIDs);

			// 创建一个 UnityObject 列表来存储排序后的对象
			List<UnityObject> objList = new List<UnityObject>(sortedDraggedIDs.Count);

			// 遍历排序后的 ID，将其转换为 UnityObject，并添加到列表中
			foreach (var id in sortedDraggedIDs)
			{
				UnityObject obj = EditorUtility.InstanceIDToObject(id);
				if (obj != null)
					objList.Add(obj);
			}

			// 设置拖动对象的引用
			DragAndDrop.objectReferences = objList.ToArray();

			// 确定拖动的标题，如果有多个对象则显示 "<Multiple>"
			string title = objList.Count > 1 ? "<Multiple>" : objList[0].name;
			DragAndDrop.StartDrag(title);
		}

		// 处理拖放操作的逻辑
		protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
		{
			// 获取当前拖动的对象
			var draggedObjects = DragAndDrop.objectReferences;
			var transforms = new List<Transform>(draggedObjects.Length);

			// 将拖动的对象转换为 Transform 并添加到列表中
			foreach (var obj in draggedObjects)
			{
				var go = obj as GameObject;
				if (go == null)
				{
					return DragAndDropVisualMode.None;
				}
				transforms.Add(go.transform);
			}

			// 移除那些是其他拖动对象的子孙的项
			RemoveItemsThatAreDescendantsFromOtherItems(transforms);

			if (args.performDrop)
			{
				// 根据拖放位置的不同处理拖放逻辑
				switch (args.dragAndDropPosition)
				{
					case DragAndDropPosition.UponItem:
					case DragAndDropPosition.BetweenItems:
						// 获取父项的 Transform
						Transform parent = args.parentItem != null ? GetGameObject(args.parentItem.id).transform : null;


						// 在这里添加对 parent 为 null 的检查  
						if (parent == null && transforms.Count > 0)
						{
							// 如果 parent 为 null，但拖动的对象存在，可能需要默认行为或错误处理  
							// 例如，可以选择一个默认父对象或显示错误消息  
							Debug.LogError("Cannot drop items without a valid parent.");
							return DragAndDropVisualMode.None;
						}


						// 验证是否可以重新父级
						if (!IsValidReparenting(parent, transforms))
							return DragAndDropVisualMode.None;

						// 重新设置 Transform 的父级
						foreach (var trans in transforms)
							trans.SetParent(parent);

						// 如果是在项之间拖放，调整子项的排序
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
						// 拖动到项外部，取消父级
						foreach (var trans in transforms)
						{
							trans.SetParent(null);
						}
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				// 重新加载视图并更新选择
				Reload();
				SetSelection(transforms.Select(t => t.gameObject.GetInstanceID()).ToList(), TreeViewSelectionOptions.RevealAndFrame);
			}

			return DragAndDropVisualMode.Move;
		}

		// 获取调整后的插入索引
		int GetAdjustedInsertIndex(Transform parent, Transform transformToInsert, int insertIndex)
		{
			// 如果当前项的兄弟索引小于插入索引，则插入索引减一
			if (transformToInsert.parent == parent && transformToInsert.GetSiblingIndex() < insertIndex)
				return --insertIndex;
			return insertIndex;
		}

		// 验证重新父级操作是否合法
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

		// 检查某个 Transform 是否是另一个 Transform 的子孙
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

		// 检查某个 Transform 是否是指定 Transform 列表的子孙
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

		// 移除列表中那些是其他项子孙的项
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
					headerContent = new GUIContent("GameObject数量"),
					width = 150,
					minWidth = 50,
					maxWidth = 200,
					canSort = true,
					autoResize = true
				},
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Component数量"),
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

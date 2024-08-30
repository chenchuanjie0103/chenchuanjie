using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;


namespace UnityEditor.TreeViewExamples
{
	class SimpleTreeView : TreeView
	{
		public SimpleTreeView(TreeViewState treeViewState)
			: base(treeViewState)
		{
			Reload();
		}		
		protected override TreeViewItem BuildRoot ()
		{
			// ÿ�ε��� Reload ʱ������ BuildRoot���Ӷ�ȷ��ʹ������
			// ���� TreeViewItem���˴������ǽ������̶���һ�������ʵʾ���У�
			// Ӧ������ģ�ʹ��� TreeView �Լ���ģ�ʹ������

			// �˲���˵�� ID Ӧ����Ψһ�ġ���������
			// ����Ϊ -1�������������ڴ˻����ϵ�����
			var root = new TreeViewItem {id = 0, depth = -1, displayName = "Root"};
			var allItems = new List<TreeViewItem> 
			{
				new TreeViewItem {id = 1, depth = 0, displayName = "Animals"},
				new TreeViewItem {id = 2, depth = 1, displayName = "Mammals"},
				new TreeViewItem {id = 3, depth = 2, displayName = "Tiger"},
				new TreeViewItem {id = 4, depth = 2, displayName = "Elephant"},
				new TreeViewItem {id = 5, depth = 2, displayName = "Okapi"},
				new TreeViewItem {id = 6, depth = 2, displayName = "Armadillo"},
				new TreeViewItem {id = 7, depth = 1, displayName = "Reptiles"},
				new TreeViewItem {id = 8, depth = 2, displayName = "Crocodile"},
				new TreeViewItem {id = 9, depth = 2, displayName = "Lizard"},
			};
			// ���ڳ�ʼ��������� TreeViewItem.children �� .parent ��ʵ�÷�����
			SetupParentsAndChildrenFromDepths(root, allItems);
			//�������ĸ�
			return root;
		}
	}
}
////��ͨ�����ַ������� TreeViewItem��ֱ�����ø���������ʹ�� AddChild ����
//var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
//var animals = new TreeViewItem { id = 1, displayName = "Animals" };
//var mammals = new TreeViewItem { id = 2, displayName = "Mammals" };
//var tiger = new TreeViewItem { id = 3, displayName = "Tiger" };
//var elephant = new TreeViewItem { id = 4, displayName = "Elephant" };
//var okapi = new TreeViewItem { id = 5, displayName = "Okapi" };
//var armadillo = new TreeViewItem { id = 6, displayName = "Armadillo" };
//var reptiles = new TreeViewItem { id = 7, displayName = "Reptiles" };
//var croco = new TreeViewItem { id = 8, displayName = "Crocodile" };
//var lizard = new TreeViewItem { id = 9, displayName = "Lizard" };
//root.AddChild(animals);
//animals.AddChild(mammals);
//animals.AddChild(reptiles);
//mammals.AddChild(tiger);
//mammals.AddChild(elephant);
//mammals.AddChild(okapi);
//mammals.AddChild(armadillo);
//reptiles.AddChild(croco);
//reptiles.AddChild(lizard);
//SetupDepthsFromParentsAndChildren(root);
//return root;
using UnityEngine;
using UnityEditor.IMGUI.Controls;


namespace UnityEditor.TreeViewExamples
{
    public class SimpleTreeViewWindow : EditorWindow
    {
        // SerializeField ����ȷ������ͼ״̬д�봰��
        // �����ļ�������ζ��ֻҪ����δ�رգ���ʹ�������� Unity��Ҳ�ᱣ��
        // ״̬�����ʡ�Ը����ԣ���Ȼ�����л�/�����л�״̬��
        [SerializeField] TreeViewState m_TreeViewState;
        //TreeView �������л������Ӧ��ͨ�������ݶ�������ؽ���
        SimpleTreeView m_TreeView;
        SearchField m_SearchField;

        [MenuItem("TreeView Examples/Simple Tree Window")]
        static void ShowWindow()
        {
            // ��ȡ���д򿪵Ĵ��ڣ����û�У����½�һ�����ڣ�
            var window = GetWindow<SimpleTreeViewWindow>();
            window.titleContent = new GUIContent("My Window");
            window.Show();
        }
        void OnEnable()
        {
            //����Ƿ��Ѵ������л���ͼ״̬���ڳ������¼��غ�
            // ��Ȼ���ڵ�״̬��
            if (m_TreeViewState == null)
                m_TreeViewState = new TreeViewState();

            m_TreeView = new SimpleTreeView(m_TreeViewState);
            m_SearchField = new SearchField();
            m_SearchField.downOrUpArrowKeyPressed += m_TreeView.SetFocusAndEnsureSelectedItem;
        }
        void OnGUI()
        {
            DoToolbar();
            DoTreeView();
            //m_TreeView.OnGUI(new Rect(0, 0, position.width, position.height));
        }
        void DoToolbar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.Space(100);
            GUILayout.FlexibleSpace();
            m_TreeView.searchString = m_SearchField.OnToolbarGUI(m_TreeView.searchString);
            GUILayout.EndHorizontal();
        }
        void DoTreeView()
        {
            Rect rect = GUILayoutUtility.GetRect(0, 100000, 0, 100000);
            m_TreeView.OnGUI(rect);
        }
    }
}

using System;
using System.Reflection;

using UnityEditor;
using UnityEngine;

using VirtualEscapes.ColorCoder;

namespace VirtualEscapes.ColorScript
{
    [CustomEditor(typeof(MonoScript), false)]
    [CanEditMultipleObjects]
    public class ColorScriptInspector : Editor
    {
        private Editor defaultEditor;//Unity's built-in editor

        public override bool UseDefaultMargins() { return false; }

        public override bool RequiresConstantRepaint() { return mColorScript == null ? false : mColorScript.requiresConstantRepaint; }

        private ColorScript mColorScript;

        private MonoScript monoScript { get { return target as MonoScript; } }

        private ColorScriptDataContainer mColorScriptDataContainer;

        private GUIContent mContentOldVersionLabel, mContentOpenWindow;

        public ColorScriptDataContainer dataContainer
        {
            get
            {
                if (mColorScriptDataContainer == null)
                {
                    mColorScriptDataContainer = EditorHelper.CreateDataAsset<ColorScriptDataContainer>(ColorScript.APP_PATH, ColorScript.DATA_FILENAME);
                }
                return mColorScriptDataContainer;
            }
        }

        void OnEnable()
        {
            ColorCoder.Themes.Manager.Initialise();

            ColorCoder.ColorCoder lColorCoder = new ColorCoder.ColorCoder(new LexerCSharp());

            if (mColorScript == null) mColorScript = new ColorScript(lColorCoder, false, dataContainer.inspectorData);

            //FIXME: surely we can set this in the constructor and make it readonly
            mColorScript.monoScript = monoScript;

            //When this inspector is created, also create the built-in inspector
            defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.MonoScriptInspector, UnityEditor"));

            mContentOldVersionLabel = new GUIContent("Color Script can display in the Unity Inspector in Unity 2019.x and greater.");
            mContentOpenWindow = new GUIContent("Open " + ColorScript.APP_NAME + " Window", "Open Color Script in a new window");
        }

        void OnDisable()
        {
            mColorScript.Destroy();

            //destroy the default editor to avoid memory leakage
            if (defaultEditor != null)
            {
                MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (disableMethod != null)
                {
                    disableMethod.Invoke(defaultEditor, null);
                }
                DestroyImmediate(defaultEditor);
            }
        }

        public override void OnInspectorGUI()
        {
            if (monoScript == null) return; //catches if we've deleted the script file that this inspector was associated with
            if (!mColorScript.monoScriptIsValid)
            {
                defaultEditor.OnInspectorGUI();
                return;
            }

            bool lbUseCustomScriptInspectorCache = dataContainer.useCustomScriptInspector;

            GUIStyle toolBarStyle = EditorStyles.toolbarButton;
            GUIContent[] laOptions = new GUIContent[] { new GUIContent(ColorScript.APP_NAME), new GUIContent("Unity Inspector") };
            dataContainer.useCustomScriptInspector = drawColorScriptTopBar(laOptions, toolBarStyle);

            bool lbNeedsRefresh = lbUseCustomScriptInspectorCache != dataContainer.useCustomScriptInspector;

            if (dataContainer.useCustomScriptInspector)
            {
                mColorScript.monoScript = monoScript;

                bool hasTokenized;
                if (!mColorScript.Update(out hasTokenized)) return;

                lbNeedsRefresh |= hasTokenized;

                if (!VisualElementHelper.FindInspectorElementsForMonoScriptEditor())
                {
                    //something has gone wrong so we should draw button with option to open in EditorWindow
                    GUILayout.Space(EditorGUIUtility.singleLineHeight);
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(mContentOldVersionLabel);
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();

                    GUILayout.Space(EditorGUIUtility.singleLineHeight);
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button(mContentOpenWindow))
                    {
                        ColorScriptWindow.Init();
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();

                    return;

                }

                Rect lControlRect = VisualElementHelper.GetMonoScriptInspectorControlRect(EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing); //the entire rect for this editor
                //Debug.Log("Drawing ColorScript Inspector");
                mColorScript.GUIDraw(ref lControlRect, lbNeedsRefresh, dataContainer.inspectorData, true);
            }
            else
            {
                EditorGUILayout.Space();
                defaultEditor.OnInspectorGUI();
            }
        }

        private bool drawColorScriptTopBar(GUIContent[] contents, GUIStyle style)
        {
            int liUseCustomScriptInspector = dataContainer.useCustomScriptInspector ? 0 : 1;
            liUseCustomScriptInspector = GUILayout.Toolbar(liUseCustomScriptInspector, contents, style);
            return liUseCustomScriptInspector == 0 ? true : false;
        }
    }
}

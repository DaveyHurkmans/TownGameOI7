#if UNITY_2019_1_OR_NEWER

using System.Linq;

using UnityEngine.UIElements;

#else

using UnityEngine.Experimental.UIElements;

#endif

using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace VirtualEscapes.ColorScript
{
    public static class VisualElementHelper
    {
        /// <summary>
        /// Get the available height in this editor view (underneath all the headers)
        /// </summary>
        /// <returns></returns>
        public static float MonoScriptInspectorViewHeight// => EditorWindowHeight - EditorStartPositionY;
        {
            get
            {
#if UNITY_2019_1_OR_NEWER
                float paddingVertical = sMonoScriptInspector.style.paddingBottom.value.value + sMonoScriptInspector.style.paddingTop.value.value;
#else
                float paddingVertical = sMonoScriptInspector.style.paddingBottom.value + sMonoScriptInspector.style.paddingTop.value;
#endif
                //Debug.Log("paddingVertical = " + paddingVertical);
                float height = sInspectorFooter.worldBound.y - sMonoScriptInspector.worldBound.y - paddingVertical - 4;
                return height;
            }
        }

        private static VisualElement sMonoScriptInspector, sInspectorFooter;

        public static bool hasMonoScriptVisualElements { get { return sMonoScriptInspector != null && sInspectorFooter != null; } }

        public static Rect GetMonoScriptInspectorControlRect(float yOffset)
        {
            float lfAvailableHeight = MonoScriptInspectorViewHeight - yOffset;
            return EditorGUILayout.GetControlRect(false, lfAvailableHeight, GUIStyle.none);
        }

        /// <summary>
        /// Finds the VisualElements necessary to determine the available height for the custom editor inspector
        /// </summary>
        /// <returns>True if the necessary elements have been found. If False, it's likely the inspector window is either not open or is not showing a monoscript</returns>
        public static bool FindInspectorElementsForMonoScriptEditor()
        {
            if (hasMonoScriptVisualElements) return true;

            Type lInspectorWindowType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            Type lPropertyEditorType = lInspectorWindowType.BaseType;

            EditorWindow[] allWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();

            foreach (EditorWindow window in allWindows)
            {
                Type lType = window.GetType();
                if (lType.Name == "InspectorWindow")
                {
                    object lResult = null;

#if UNITY_2019_1_OR_NEWER
                    lResult = window.rootVisualElement; //very easy
#endif

                    if (lResult == null)
                    {
                        if (CSAssemblyHelper.GetTypeContainsInstanceField(lPropertyEditorType, "m_RootVisualElement"))
                        {
                            lResult = CSAssemblyHelper.GetInstanceField(lPropertyEditorType, window, "m_RootVisualElement");
                        }
                        else if (CSAssemblyHelper.GetTypeContainsInstanceField(lPropertyEditorType, "m_RootElementPerEditorMode"))
                        {
                            lResult = CSAssemblyHelper.GetInstanceField(lPropertyEditorType, window, "m_RootElementPerEditorMode");
                        }
                    }

                    if (lResult != null)
                    {
                        VisualElement lRoot = lResult as VisualElement;

                        if (lRoot == null)
                        {
                            Dictionary<Type, VisualElement> visualElementDictionary = lResult as Dictionary<Type, VisualElement>;
                            foreach (KeyValuePair<Type, VisualElement> kvp in visualElementDictionary)
                            {
                                lRoot = kvp.Value;
                            }

                        }

                        if (lRoot != null)
                        {
                            sMonoScriptInspector = findVisualElement(lRoot, x => { return x.name.EndsWith(" (Mono Script)Inspector"); });
                            //Debug.Log($"lMonoScriptInspector = {lMonoScriptInspector.name}, {lMonoScriptInspector.layout}");

#if UNITY_2019_1_OR_NEWER
                            sInspectorFooter = findVisualElement(lRoot, x => { return x.GetClasses().Contains("unity-inspector-footer-info"); });
#else
                            sInspectorFooter = findVisualElement(lRoot, x => { return x.ClassListContains("unity-inspector-footer-info"); });
#endif
                            //Debug.Log($"lFooterElement = {lFooterElement.name}, {lFooterElement.layout}");

                            return hasMonoScriptVisualElements;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        private delegate bool VisualElementEqualityDelegate(VisualElement x);

        private static VisualElement findVisualElement(VisualElement parent, VisualElementEqualityDelegate equalityDelegate)
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            VisualElement foundChild = null;

            foreach (VisualElement child in parent.Children())
            {
                // If the child is not of the request child type child
                if (!equalityDelegate(child))
                {
                    // recursively drill down the tree
                    foundChild = findVisualElement(child, equalityDelegate);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else
                {
                    // child element found.
                    foundChild = child;
                    break;
                }
            }

            return foundChild;
        }
    }
}
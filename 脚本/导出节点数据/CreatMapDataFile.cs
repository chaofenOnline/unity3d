using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreatMapDataFile : EditorWindow {

    static string writePath = "C:\\Users\\Administrator\\Desktop\\hiddenObjects1.0.9_unity\\scene_res_json\\";
    static GameObject selectObj;


    [MenuItem("星大陆/导出节点信息")]
    static void XDLWindow()
    {
        EditorWindow.GetWindow<CreatMapDataFile>();
    }

    private void OnGUI() {
        GUILayout.Label("设置数据生成路径");
        writePath = GUILayout.TextField(writePath);
        // GUILayout.Label("请选择目标节点");

        // if (GUILayout.Button("生成C#渔场数据")) {
        //     if (selectObj != null) {
        //         CreatMapUitl.CreatCSharpMapFile(selectObj, writePath);
        //     }
        // }
        // if (GUILayout.Button("生成C/C++渔场数据")) {
        //     if (selectObj != null) {
        //         // CreatMapUitl.CreatCSharpMapFile(selectObj, writePath);
        //     }
        // }

        // if (GUILayout.Button("生成Java渔场数据")) {
        //     if (selectObj != null) {
        //         // CreatMapUitl.CreatCSharpMapFile(selectObj, writePath);
        //     }
        // }
        if (GUILayout.Button("生成JS数据")) {
            if (selectObj != null)
            {
                CreatMapUitl.CreatJSMapFile(selectObj, writePath);
            }
        }

        if (GUILayout.Button("生成JSON数据")) {
            if (selectObj != null)
            {
                CreatMapUitl.CreatJSONMapFile(selectObj, writePath);
            }
        }
        // if (GUILayout.Button("生成TS渔场数据"))
        // {
        //     if (selectObj != null)
        //     {
        //         CreatMapUitl.CreatTSMapFile(selectObj, writePath);
        //     }
        // }

        // if (GUILayout.Button("生成二进制数据文件")) {
        //     if (selectObj != null) {
        //         // CreatMapUitl.CreatCSharpMapFile(selectObj, writePath);
        //     }
        // }

        if (Selection.activeGameObject != null) {
                GUILayout.Label(Selection.activeGameObject.name);
                selectObj = Selection.activeGameObject;
            // if (Selection.activeGameObject.GetComponent<SWS.WaypointManager>()) {
            //     GUILayout.Label(Selection.activeGameObject.name);
            //     selectObj = Selection.activeGameObject;
            // }
        }
    }

    private void OnSelectionChange()
    {
        Repaint();
    }



}


using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;



public class CreatMapUitl {

    // public static void CreatCSharpMapFile(GameObject selectObj, string writePath)
    // {
    //     string fileName = selectObj.name;
    //     string className = fileName;
    //     StreamWriter sw = new StreamWriter(Application.dataPath + writePath + className + ".cs");

    //     sw.WriteLine("using UnityEngine;\nusing System.Collections;\n");
    //     sw.WriteLine("public class " + className + " {");
    //     sw.WriteLine("\tstatic public road_data[] roads = new road_data[]{");

    //     foreach (Transform t in selectObj.transform) {
    //         sw.WriteLine("\t\tnew road_data {");
    //         sw.WriteLine("\t\t\tdata = new Vector3[] {");

    //         PathManager road_data = t.GetComponent<PathManager>();
    //         Vector3[] road_points = WaypointManager.GetCurved(road_data.GetPathPoints());
    //         for (int i = 0; i < road_points.Length; i++) {
    //             sw.WriteLine("\t\t\t\tnew Vector3(" + road_points[i].x + "f, " + road_points[i].y + "f, " + road_points[i].z + "f),");
    //         }
    //         sw.WriteLine("\t\t\t},");
    //         sw.WriteLine("\t\t},\n");
    //     }

    //     sw.WriteLine("\t};");
    //     sw.WriteLine("}");

        
    //     sw.Flush();
    //     sw.Close();
    //     AssetDatabase.Refresh();        //这里是一个点
    // }

    public static void CreatJSMapFile(GameObject selectObj, string writePath)
    {
        string fileName = selectObj.name;
        string className = fileName;        
        StreamWriter sw = new StreamWriter(Application.dataPath + writePath + className + ".js");

        sw.WriteLine("var " + className + " = {");
        sw.WriteLine("\troads: [");

        // foreach (Transform t in selectObj.transform)
        // {
        //     sw.WriteLine("\t\t[");
            

        //     PathManager road_data = t.GetComponent<PathManager>();
        //     Vector3[] road_points = WaypointManager.GetCurved(road_data.GetPathPoints());
        //     for (int i = 0; i < road_points.Length; i++)
        //     {
        //         sw.WriteLine("\t\t\t{ x: " + road_points[i].x + ", y: " + road_points[i].y + ", z: " + road_points[i].z + " },");
        //     }
            
        //     sw.WriteLine("\t\t],\n");
        // }

        sw.WriteLine("\t],");
        sw.WriteLine("};\nmodule.exports = " + className + ";");


        sw.Flush();
        sw.Close();
        AssetDatabase.Refresh();        //这里是一个点
    }

    // json格式
     public static void CreatJSONMapFile(GameObject selectObj, string writePath)
    {
        // 当前场景名
        string sceneName = SceneManager.GetActiveScene().name;
        
        Transform trans = selectObj.transform;
        
  
        Transform child = null;
        Transform child2 = null;
    
        Debug.Log(sceneName);

        // StreamWriter sw = new StreamWriter(Application.dataPath + writePath + sceneName + ".json");
        StreamWriter sw = new StreamWriter( writePath + sceneName + ".json");
        sw.Write("{");
        sw.Write("\"name\":"+"\""+sceneName+"\",");
        sw.Write("\"children\":"+"[");
        

        for (int i = 0; i < trans.childCount; i++)
        {
            sw.Write("{");
            child = trans.GetChild(i);
            // Debug.Log(child.name);
            sw.Write("\"name\":"+"\""+child.name+"\",");
            if(child.childCount > 0){
                sw.Write("\"children\":"+"[");
                
                for (int i2 = 0; i2 < child.childCount; i2++)
                {
                    child2 = child.GetChild(i2);
                    // Debug.Log(child2.name);
                    sw.Write("{");
                    
                    sw.Write("\"name\":"+"\""+child2.name+"\",");
                    sw.Write("\"pos\":"+"{\"x\":"+child2.position.x+",\"y\":"+child2.position.y+"}");
                    if(i2 == child.childCount -1){
                        sw.Write("}");
                    }else{
                        sw.Write("},");
                    }
                }

                sw.Write("]");
                
            }
            if(i == trans.childCount -1){
                sw.Write("}");
            }else{
                sw.Write("},");
            }
        }
        sw.Write("]");
        sw.Write("}");
        sw.Flush();
        sw.Close();
        AssetDatabase.Refresh(); 
        
    }

    // public static void CreatTSMapFile(GameObject selectObj, string writePath)
    // {
    //     string fileName = selectObj.name;
    //     string className = fileName;
    //     StreamWriter sw = new StreamWriter(Application.dataPath + writePath + className + ".ts");

    //     sw.WriteLine("export class " + className + " {");
    //     sw.WriteLine("\n public static roads = [");

    //     // foreach (Transform t in selectObj.transform)
    //     // {
    //     //     sw.WriteLine("\t\t[");


    //     //     PathManager road_data = t.GetComponent<PathManager>();
    //     //     Vector3[] road_points = WaypointManager.GetCurved(road_data.GetPathPoints());
    //     //     for (int i = 0; i < road_points.Length; i++)
    //     //     {
    //     //         sw.WriteLine("\t\t\t{ x: " + road_points[i].x + ", y: " + road_points[i].y + ", z: " + road_points[i].z + " },");
    //     //     }

    //     //     sw.WriteLine("\t\t],\n");
    //     // }

    //     sw.WriteLine("\t];");
    //     sw.WriteLine("};\n");


    //     sw.Flush();
    //     sw.Close();
    //     AssetDatabase.Refresh();        //这里是一个点
    // }
}

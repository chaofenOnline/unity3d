using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// 切割
/// </summary>
public static class SpriteAtlasSplitter
{
    [MenuItem("Assets/图集切割")]
    static void DeepProcessToSprite()
    {
        // get selected directory path
        string rootPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string outRootPath = rootPath + "_图集切割结果";
        Object[] textures = {};
        // 单文件
        if(File.Exists(rootPath)){
            Debug.Log("单文件切割");
            Texture2D inTex = Selection.activeObject as Texture2D;//获取旋转的对象
            string allPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(inTex));//获取路径名称
            Debug.Log(allPath);
            string path = allPath + "/" + inTex.name + ".PNG";//图片路径名称            // texture importer
            TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口
            // string dirPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(inTex)).Substring(allPath.Length);  
            SpriteAtlasSplitter.split(texImp,inTex,outRootPath,"");
            return;
        }
        // 目录
        else{
            textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        }
        Debug.Log("目录切割");
        // output root directory path
        // get all textures
        // Object[] textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        if (!rootPath.StartsWith("Assets/") || textures.Length < 1)
        {
            Debug.LogError("非有效输入");
            return;
        }

        foreach (Texture2D inTex in textures)
        {

            // image's directory path under rootPath
            string dirPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(inTex)).Substring(rootPath.Length);
            // get extension
            //string ext = Path.GetExtension(tex);
            // input image path
            string inImgPath = rootPath + dirPath + "/" + inTex.name + ".PNG";

            // texture importer
            TextureImporter texImp = AssetImporter.GetAtPath(inImgPath) as TextureImporter;
            // enable read and write
            texImp.isReadable = true;
            // set alpha
            texImp.alphaSource = TextureImporterAlphaSource.FromInput;
            // reimport asset
            AssetDatabase.ImportAsset(inImgPath);

            SpriteAtlasSplitter.split(texImp,inTex,outRootPath,dirPath);
            // SpriteAtlasSplitter.sayHi();
        }

        // 刷新资源窗口界面
        AssetDatabase.Refresh();
        Debug.Log("All done. Output path:" + outRootPath);
    }
    static void sayHi(){
        Debug.Log("sayHi");
    }

    static void split(TextureImporter texImp,Texture2D inTex,string outRootPath,string dirPath){ 
        foreach (SpriteMetaData metaData in texImp.spritesheet)
            {
                Texture2D outTex = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);
                // set image content
                for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)
                {
                    for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                    {
                        outTex.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, inTex.GetPixel(x, y));
                    }
                }

                //转换纹理到EncodeToPNG兼容格式
                if (outTex.format != TextureFormat.ARGB32 && outTex.format != TextureFormat.RGB24)
                {
                    Texture2D newTexture = new Texture2D(outTex.width, outTex.height);
                    newTexture.SetPixels(outTex.GetPixels(0), 0);
                    outTex = newTexture;
                }
                var pngData = outTex.EncodeToPNG();

                // output path
                string outPath = outRootPath + dirPath + "/" + inTex.name;
                // check and create output directory
                if (!Directory.Exists(outPath))
                {
                    Directory.CreateDirectory(outPath);
                }

                // write image
                File.WriteAllBytes(outPath + "/" + metaData.name + ".PNG", pngData);
            }
            Debug.Log(inTex.name + "切割完成");


    }
}

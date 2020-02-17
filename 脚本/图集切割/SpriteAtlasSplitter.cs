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
    [MenuItem("Assets/开始切割")]
    static void ProcessToSprite()
    {
        Texture2D image = Selection.activeObject as Texture2D;//获取旋转的对象
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));//获取路径名称
        string path = rootPath + "/" + image.name + ".PNG";//图片路径名称


 		// 当前场景名
        string sceneName = SceneManager.GetActiveScene().name;
        // 存储路径
        string savePath = "C:\\Users\\Administrator\\Desktop\\hiddenObjects1.0.9_unity\\scene_res\\" + sceneName;
        
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            Debug.Log("文件夹不存在,创建");
        }

        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;//获取图片入口


        // AssetDatabase.CreateFolder(rootPath, image.name);//创建文件夹


        foreach (SpriteMetaData metaData in texImp.spritesheet)//遍历小图集
        {
            Texture2D myimage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);

            //abc_0:(x:2.00, y:400.00, width:103.00, height:112.00)
            for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)//Y轴像素
            {
                for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                    myimage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, image.GetPixel(x, y));
            }


            //转换纹理到EncodeToPNG兼容格式
            if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)
            {
                Texture2D newTexture = new Texture2D(myimage.width, myimage.height);
                newTexture.SetPixels(myimage.GetPixels(0), 0);
                myimage = newTexture;
            }
            var pngData = myimage.EncodeToPNG();


            //AssetDatabase.CreateAsset(myimage, rootPath + "/" + image.name + "/" + metaData.name + ".PNG");
            File.WriteAllBytes(savePath + "/" + metaData.name + ".PNG", pngData);
            // File.WriteAllBytes(rootPath + "/" + image.name + "/" + metaData.name + ".PNG", pngData);
            // 刷新资源窗口界面
            AssetDatabase.Refresh();
            Debug.Log(image.name+"切割完成");
            
        }
    }
}

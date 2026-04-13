using System.IO;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CheckFolder : MonoBehaviour
{
    public string folderPath;
    public float scanInterval = 2f;

    private HashSet<string> loadedFiles = new HashSet<string>();
    public static Spawner Instance;

    void Start()
    {
#if UNITY_EDITOR
        folderPath = Application.dataPath + "/AquascapeAssets";
#else
        folderPath = Application.dataPath + "/../AquascapeAssets";
#endif

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        StartCoroutine(ScanFolderRoutine());
    }

    IEnumerator ScanFolderRoutine()
    {
        while (true)
        {
            ScanFolder();
            yield return new WaitForSeconds(scanInterval);
        }
    }

    void ScanFolder()
    {
        string[] files = Directory.GetFiles(folderPath, "*.png");

        foreach (string file in files)
        {
            if (loadedFiles.Contains(file)) continue;

            loadedFiles.Add(file);
            StartCoroutine(ProcessFile(file));
        }
    }

    IEnumerator ProcessFile(string path)
    {
        byte[] fileData = File.ReadAllBytes(path);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);

        Spawner.Instance.SpawnFromFile(Path.GetFileName(path), tex);

        yield return null;
    }
}
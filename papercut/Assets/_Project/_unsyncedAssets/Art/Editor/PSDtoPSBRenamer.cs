using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PSDtoPSBRenamerWatcher
{
    private const string ArtFolder = "Assets/_Project/Art";
    private const int MaxAttempts = 5;
    private const int DelayMilliseconds = 100;
    private static FileSystemWatcher watcher;

    static PSDtoPSBRenamerWatcher()
    {
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), ArtFolder);
        watcher = new FileSystemWatcher(fullPath, "*.psd");
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime;
        watcher.Created += OnFileEvent;
        watcher.Changed += OnFileEvent;
        watcher.EnableRaisingEvents = true;
    }

    private static void OnFileEvent(object sender, FileSystemEventArgs e)
    {
        ProcessFile(e.FullPath);
    }

    private static void ProcessFile(string psdFullPath)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            for (int attempt = 0; attempt < MaxAttempts; attempt++)
            {
                if (IsFileReady(psdFullPath))
                {
                    string psbFullPath = Path.ChangeExtension(psdFullPath, ".psb");
                    if (File.Exists(psbFullPath))
                    {
                        File.Replace(psdFullPath, psbFullPath, null);
                        Debug.Log($"Replaced {Path.GetFileName(psdFullPath)} with updated {Path.GetFileName(psbFullPath)}");
                    }
                    else
                    {
                        File.Move(psdFullPath, psbFullPath);
                        Debug.Log($"Renamed {Path.GetFileName(psdFullPath)} to {Path.GetFileName(psbFullPath)}");
                    }
                    string metaPath = psdFullPath + ".meta";
                    if (File.Exists(metaPath))
                        File.Delete(metaPath);
                    break;
                }
                Thread.Sleep(DelayMilliseconds);
            }
        });
    }

    private static bool IsFileReady(string filename)
    {
        try
        {
            using FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None);
            return stream.Length > 0;
        }
        catch
        {
            return false;
        }
    }
}

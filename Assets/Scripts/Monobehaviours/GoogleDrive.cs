using System;
using System.Text;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public  class GoogleDrive
{

    /// <summary>
    /// Return List with all files from GoogleDrive
    /// </summary>
    /// <returns></returns>
    public static void FileList(Action<FileList> onDone)
    {
        GoogleDriveFiles.List().Send().OnDone += onDone;
    }

    /// <summary>
    /// Save on GoogleDrive jsonString info
    /// </summary>
    /// <param name="data">string</param>
    /// <param name="name">name of file</param>
    /// <returns></returns>
    public static void Upload(string data, string name)
    {
        var file = new File { Name = name, Content = Encoding.ASCII.GetBytes(data) };
        GoogleDriveFiles.Create(file).Send();
    }

    /// <summary>
    /// Find file by ID and download it
    /// </summary>
    /// <param name="fileID"></param>
    /// <returns></returns>
    public static void DownloadFile(string id, Action<File> onDone)
    {
    GoogleDriveFiles.Download(id).Send().OnDone += onDone;
    }
}

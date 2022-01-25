using Unity.Entities;
using UnityEngine.InputSystem;
using UnityEngine;
using static GoogleDrive;
using UnityGoogleDrive.Data;
using System.Text;
using UnityGoogleDrive;

public class GoogleDriveSystem : SystemBase
{
    private InputAction _loadButton;

    private InputAction _saveButton;

    protected override void OnStartRunning()
    {
        _loadButton = new InputAction("Load", binding: ("<Keyboard>/L"));
        _loadButton.Enable();

        _saveButton = new InputAction("Save", binding: ("<Keyboard>/U"));
        _saveButton.Enable();

        FileList(FindFileOrCreate);
    }

    protected override void OnStopRunning()
    {
        _saveButton.Disable();
        _loadButton.Disable();
    }

    protected override void OnUpdate()
    {
        if(_saveButton.phase == InputActionPhase.Started)
        {
            FileList(ChangeContent);
        }

        if (_loadButton.phase == InputActionPhase.Started)
        {
            FileList(FindFileOrCreate);
        }
    }
    
    private void CreateFile()
    {
        Entities.ForEach(
           (Entity entity, in HealthComponent healthComponent) =>
           {
               int health = healthComponent.Health;
               var jsonString = JsonUtility.ToJson(healthComponent);
               Upload(jsonString, "PlayerInfo.json");
           }).WithoutBurst().Run();
    }

    private void FindFileOrCreate(FileList fileList)
    {
        var files = fileList.Files;
        foreach (var file in files)
        {
            if (file.Name.Equals("PlayerInfo.json"))
            {
                DownloadFile(file.Id, DeserializeAndSave);
                return;
            }
        }
        CreateFile();
    }

    private void DeserializeAndSave(File file)
    {
        string json = Encoding.ASCII.GetString(file.Content);
        HealthComponent health = JsonUtility.FromJson<HealthComponent>(json);
        Entities.ForEach(
            (ref HealthComponent healthComponent) =>
            {
                healthComponent = health;
            }).Run();
    }

    private void ChangeContent(FileList fileList)
    {
        Entities.ForEach(
                (Entity entity, in HealthComponent healthComponent) =>
                {
                    int health = healthComponent.Health;
                    var jsonString = JsonUtility.ToJson(healthComponent);
                    foreach(var file in fileList.Files)
                    {
                        if(file.Name.Equals("PlayerInfo.json"))
                        {
                            var newFile = new File { Name = "PlayerInfo.json", Content = Encoding.ASCII.GetBytes(jsonString) };
                            GoogleDriveFiles.Update(file.Id, newFile).Send();
                        }
                    }
                }).WithoutBurst().Run();
    }
}

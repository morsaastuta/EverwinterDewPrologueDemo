using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    string root = ".";
    string globalPath = "sav";

    string slotFile = "slot_";
    string settingsFile = "settings_";

    string slotPath;
    string settingsPath;

    public FileDataHandler(int slot)
    {
        slotPath = Path.Combine(root, globalPath, slotFile += slot);
        settingsPath = Path.Combine(root, globalPath, settingsFile += slot);
    }

    public void SaveSlot(SlotData slotData)
    {
        try
        {
            // Create directory to save files
            if (!Directory.Exists(Path.GetDirectoryName(slotPath))) Directory.CreateDirectory(Path.GetDirectoryName(slotPath));

            // Set writer
            StreamWriter writer = new StreamWriter(new FileStream(slotPath, FileMode.Create));

            // Write file
            writer.Write(JsonUtility.ToJson(slotData, true));
            writer.Flush();
            writer.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public SlotData LoadSlot()
    {
        SlotData slotData = null;
        try
        {
            // Set reader
            StreamReader reader = new StreamReader(new FileStream(slotPath, FileMode.Open));

            // Read file
            slotData = JsonUtility.FromJson<SlotData>(reader.ReadToEnd());
            reader.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return slotData;
    }

    public void DeleteSlot()
    {
        if (File.Exists(slotPath))
        {
            File.Delete(slotPath);
        }
    }

    // Just for checking
    public FileDataHandler() {}

    public List<int> Check()
    {
        List<int> nonExistingSlots = new();
        if (Directory.Exists(Path.Combine(root, globalPath)))
        {
            for (int i = 0; i < 4; i++)
            {
                if (!File.Exists(Path.Combine(root, globalPath, slotFile + (i + 1))))
                {
                    nonExistingSlots.Add(i);
                }
            }
        }
        return nonExistingSlots;
    }
}
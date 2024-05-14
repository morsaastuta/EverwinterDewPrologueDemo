using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    string root = ".";
    string globalPath = "sav";

    string slotFile = "slot_";
    string menuFile = "menu";

    string slotPath;
    string menuPath;

    public FileDataHandler(int slot)
    {
        slotPath = Path.Combine(root, globalPath, slotFile += slot);
        menuPath = Path.Combine(root, globalPath, menuFile);
    }

    public void SaveSlot(SlotData slotData)
    {
        try
        {
            // Create directory to save files
            if (!Directory.Exists(Path.GetDirectoryName(slotPath))) Directory.CreateDirectory(Path.GetDirectoryName(slotPath));

            // Write file
            byte[] bytes = SerializationUtility.SerializeValue(slotData, DataFormat.JSON);
            File.WriteAllBytes(slotPath, bytes);
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
            // Read file
            byte[] bytes = File.ReadAllBytes(slotPath);
            slotData = SerializationUtility.DeserializeValue<SlotData>(bytes, DataFormat.JSON);
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
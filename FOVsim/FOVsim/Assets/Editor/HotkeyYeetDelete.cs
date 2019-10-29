using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using System;

[InitializeOnLoadAttribute]
public class DisableEditorShortcutsOnPlay
{
    private const string emptyProfile = "An Empty Profile";

    // register an event handler when the class is initialized
    static DisableEditorShortcutsOnPlay()
    {
        EditorApplication.playModeStateChanged += DetectPlayModeState;
    }

    private static void CreateEmptyProfile()
    {
        try
        {
            ShortcutManager.instance.CreateProfile(emptyProfile);
        }
        catch (Exception)
        {
            // Already existed, so... we good
        }

        ShortcutManager.instance.activeProfileId = emptyProfile;
        foreach (var pid in ShortcutManager.instance.GetAvailableShortcutIds())
            ShortcutManager.instance.RebindShortcut(pid, ShortcutBinding.empty);
        ShortcutManager.instance.activeProfileId = ShortcutManager.defaultProfileId;
    }

    private static void DeleteEmptyProfile()
    {
        ShortcutManager.instance.DeleteProfile(emptyProfile);
    }

    private static void DetectPlayModeState(PlayModeStateChange state)
    {
        Debug.Log(state);
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            CreateEmptyProfile();
            ShortcutManager.instance.activeProfileId = emptyProfile;
        }
        else
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            ShortcutManager.instance.activeProfileId = ShortcutManager.defaultProfileId;
            DeleteEmptyProfile();
        }

    }

}
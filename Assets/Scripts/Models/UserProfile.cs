using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserProfile
{
    public string userName;
    public int level;
    public int currentXP;
    public int totalXP;
    public string avatarState;
    public List<string> unlockedAchievements;
    public string createdAt;

    public UserProfile()
    {
        userName = "Héroe";
        level = 1;
        currentXP = 0;
        totalXP = 0;
        avatarState = "egg";
        unlockedAchievements = new List<string>();
        createdAt = DateTime.Now.ToString("o");
    }

    // Añadir XP y verificar subida de nivel
    public bool AddXP(int amount)
    {
        currentXP += amount;
        totalXP += amount;

        int xpNeeded = GetXPForNextLevel();
        
        if (currentXP >= xpNeeded)
        {
            LevelUp();
            return true;
        }
        
        return false;
    }

    private void LevelUp()
    {
        level++;
        currentXP = currentXP - GetXPForNextLevel();
        
        // Actualizar estado del avatar según nivel
        UpdateAvatarState();
    }

    // Fórmula de XP necesario para siguiente nivel
    public int GetXPForNextLevel()
    {
        return 50 + (level - 1) * 25; // 50, 75, 100, 125...
    }

    // Porcentaje de progreso al siguiente nivel
    public float GetLevelProgress()
    {
        return (float)currentXP / GetXPForNextLevel();
    }

    private void UpdateAvatarState()
    {
        if (level >= 20) avatarState = "master";
        else if (level >= 15) avatarState = "hero";
        else if (level >= 10) avatarState = "warrior";
        else if (level >= 5) avatarState = "apprentice";
        else avatarState = "beginner";
    }

    public void UnlockAchievement(string achievementId)
    {
        if (!unlockedAchievements.Contains(achievementId))
        {
            unlockedAchievements.Add(achievementId);
        }
    }

    public bool HasAchievement(string achievementId)
    {
        return unlockedAchievements.Contains(achievementId);
    }
}

[System.Serializable]
public class Achievement
{
    public string id;
    public string title;
    public string description;
    public string iconName;
    public int xpReward;
    public bool isUnlocked;

    public Achievement(string achievementId, string achievementTitle, string desc, string icon, int xp)
    {
        id = achievementId;
        title = achievementTitle;
        description = desc;
        iconName = icon;
        xpReward = xp;
        isUnlocked = false;
    }
}
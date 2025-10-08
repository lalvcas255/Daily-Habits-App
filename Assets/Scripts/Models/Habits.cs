using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Habit
{
    public string id;
    public string name;
    public string description;
    public string iconName;
    public HabitCategory category;
    public int xpReward;
    public string createdAt;
    public List<string> completedDates;
    public bool isActive;

    public Habit()
    {
        id = Guid.NewGuid().ToString();
        completedDates = new List<string>();
        createdAt = DateTime.Now.ToString("o");
        isActive = true;
        xpReward = 10;
    }

    public Habit(string habitName, string habitDescription, HabitCategory habitCategory, string icon = "⭐")
    {
        id = Guid.NewGuid().ToString();
        name = habitName;
        description = habitDescription;
        category = habitCategory;
        iconName = icon;
        xpReward = 10;
        createdAt = DateTime.Now.ToString("o");
        completedDates = new List<string>();
        isActive = true;
    }

    // Comprobar si está completado hoy
    public bool IsCompletedToday()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        return completedDates.Contains(today);
    }

    // Marcar como completado
    public void MarkAsCompleted()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        if (!completedDates.Contains(today))
        {
            completedDates.Add(today);
        }
    }

    // Desmarcar
    public void UnmarkToday()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        completedDates.Remove(today);
    }

    // Calcular racha actual
    public int GetCurrentStreak()
    {
        if (completedDates.Count == 0) return 0;

        List<DateTime> dates = completedDates
            .Select(d => DateTime.Parse(d))
            .OrderByDescending(d => d)
            .ToList();

        int streak = 0;
        DateTime checkDate = DateTime.Now.Date;

        foreach (DateTime date in dates)
        {
            if (date.Date == checkDate.Date)
            {
                streak++;
                checkDate = checkDate.AddDays(-1);
            }
            else if ((checkDate - date.Date).Days > 1)
            {
                break;
            }
        }

        return streak;
    }

    public int GetTotalCompletions()
    {
        return completedDates.Count;
    }
}

public enum HabitCategory
{
    Health,
    Productivity,
    Social,
    Learning,
    Fitness,
    Mindfulness,
    Other
}
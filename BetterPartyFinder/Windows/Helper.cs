﻿using System.Numerics;
using Dalamud.Interface;
using ImGuiNET;

namespace BetterPartyFinder.Windows;

public static class Helper
{
    internal static bool IconButton(FontAwesomeIcon icon, string? id = null, string? tooltip = null, int width = 0)
    {
        var label = icon.ToIconString();
        if (id != null)
            label += $"##{id}";

        Plugin.Interface.UiBuilder.IconFontFixedWidthHandle.Push();
        var size = new Vector2(0, 0);
        if (width > 0)
        {
            var style = ImGui.GetStyle();
            size.X = width - 2 * style.CellPadding.X;
        }
        var ret = ImGui.Button(label, size);
        Plugin.Interface.UiBuilder.IconFontFixedWidthHandle.Pop();

        if (tooltip != null && ImGui.IsItemHovered())
            ImGui.SetTooltip(tooltip);

        return ret;
    }

    internal enum CheckState
    {
        Unchecked,
        Checked,
        Indeterminate
    }

    internal static void ThreeStateCheckbox(string label, ref CheckState state)
    {
        // Display the label depending on the current state
        string checkboxLabel = state switch
        {
            CheckState.Unchecked => "[ ]",
            CheckState.Checked => "[X]",
            CheckState.Indeterminate => "[-]",
            _ => "[ ]"
        };

        if (ImGui.Button(checkboxLabel))
        {
            // Cycle through the three states
            state = (CheckState)(((int)state + 1) % 3);
        }

        ImGui.SameLine();
        ImGui.Text(label);  // Display the text next to the checkbox
    }
}
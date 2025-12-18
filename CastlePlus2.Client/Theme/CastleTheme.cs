using MudBlazor;

namespace CastlePlus2.Client.Theme
{
    public static class CastleTheme
    {
        public static MudTheme Neon = new()
        {
            PaletteDark = new PaletteDark
            {
                Background = "#0b0f19",
                Surface = "#161b22",
                AppbarBackground = "rgba(11, 15, 25, 0.8)",
                DrawerBackground = "#0b0f19",

                Primary = "#00d2ff",    // Neon Cyan
                Secondary = "#d946ef",  // Neon Pink
                Tertiary = "#8b5cf6",   // Neon Purple
                Success = "#22c55e",
                Error = "#ef4444",
                Warning = "#eab308",

                TextPrimary = "#f1f5f9",
                TextSecondary = "#94a3b8",
                TableLines = "rgba(255,255,255,0.05)"
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "12px",
                DrawerWidthLeft = "280px"
            }
        };
    }
}

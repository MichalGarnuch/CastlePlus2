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
                Surface = "#121826",
                AppbarBackground = "rgba(22, 27, 34, 0.68)",
                DrawerBackground = "rgba(22, 27, 34, 0.68)",

                Primary = "#00d2ff",
                Secondary = "#d946ef",
                Tertiary = "#8b5cf6",
                Success = "#22c55e",
                Error = "#ef4444",
                Warning = "#eab308",

                TextPrimary = "#f1f5f9",
                TextSecondary = "#94a3b8",
                TableLines = "rgba(255,255,255,0.05)"
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "18px",
                DrawerWidthLeft = "280px"
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = new[] { "Montserrat", "Roboto", "Helvetica", "Arial", "sans-serif" }
                }
            }
        };
    }
}

using MudBlazor;

namespace CastlePlus2.Client.Theme
{
    public static class CastleTheme
    {
        public static MudTheme Dark = new()
        {
            PaletteDark = new PaletteDark
            {
                Background = "#0E1117",
                Surface = "#161B22",
                AppbarBackground = "#0E1117",
                DrawerBackground = "#0E1117",
                Primary = "#4FC3F7",
                Secondary = "#81C784",
                TextPrimary = "#E6EDF3",
                TextSecondary = "#9BA4AE",
                TableLines = "#21262D"
            },
            LayoutProperties = new LayoutProperties
            {
                DrawerWidthLeft = "300px"
            }
        };
    }
}

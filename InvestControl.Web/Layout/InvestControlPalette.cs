using MudBlazor;
using MudBlazor.Utilities;

namespace InvestControl.Web.Layout;

public sealed class InvestControlPalette : PaletteDark
{
    private InvestControlPalette()
    {
        Primary = "#ff6600";        // Laranja Itaú
        Secondary = "#004481";      // Azul escuro
        Background = "#f5f5f5";
        AppbarBackground = "#004481";
        TextPrimary = "#000000";
        DrawerBackground = "#ffffff";
        Success = "#00995d";        // Verde Itaú
        Error = "#d60000";          // Vermelho alerta Itaú
    }

    public static InvestControlPalette CreatePallete => new();
}
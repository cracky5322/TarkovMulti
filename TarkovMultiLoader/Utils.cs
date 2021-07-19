
using Comfort.Common;
using EFT;

class Utils
{
    public static GameWorld gameWorld { get => Singleton<GameWorld>.Instance; }
    public static bool inGame { get => Singleton<GameWorld>.Instantiated; }
    public static Player localPlayer { get => gameWorld.RegisteredPlayers.Find(p => p.IsYourPlayer); }

}

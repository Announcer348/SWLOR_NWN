using SWLOR.Game.Server.Event.Conversation.KeyItem;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    public class any_keyitems_1
#pragma warning restore IDE1006 // Naming Styles
    {
        public static int Main()
        {
            return KeyItemCheck.Check(1, 2) ? 1 : 0;
        }
    }
}
using Teal_Way_RPG.BattleData;
using static Teal_Way_RPG.Utils;

namespace Teal_Way_RPG.GeneralData
{
    public static class GameOver
    {
        public static void Process()
        {
            Process(true);
        }

        public static void Process(bool waitForInput)
        {
            Clear();
            CW("You were publicly humiliated and executed.");
            Sleep(1);
            CW("You died as you lived: insipid and ignorant.");
            Sleep(1);
            CW("C:\\Windows is also will be deleted next reboot.");
            Sleep(1);
            if (waitForInput)
            {
                PKC("Press any key to start a new life...");
            }

            SaveLoad.DeleteSaveFile();
            NewGame.ResetCurrentProgress();
            Battle.RequestExitToMainMenu();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    public static class UserConfig
    {
        public static int ClickedCount = 0;
        public static int RevealedCards = 0;

        public static bool CheckForWin()
        {
            // aktualizuje licznik dobrze trafionych par
            RevealedCards += 2;
            // jesli ilosc trafionych == ilosci dostepnych kart
            if (RevealedCards == Card.CardList.Count)
                return true;
            else
                return false;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Putinator
{
    class Match
    {
        public static karaktär spelare1;
        public static karaktär spelare2;
        public static string textForts = " ";
        //public static int borderXMax;
         
        //public static int borderYMax = 900;
        //public static int borderYMin = 0;

        public void placeraKaraktärer(karaktär[] karaktärlista)
        {
            for (int i = 0; i < 4; i++)
            {
                karaktärlista[i].posX = 200 + 400 * i;
            }
        }
        
        
        //Tillåter spelaren att välja karaktär och förstorar karaktären musen är på 
        public static void väljKaraktär (karaktär[] karaktärLista, Rectangle musRektangel, MouseState mus, Vector2 karaktärstorlek,int gameState)
        {
            if (gameState == 1 || gameState == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (musRektangel.Intersects(karaktärLista[i].karaktärRektangelKropp))
                    {
                        Vector2 större = new Vector2(400);
                        karaktärLista[i].storlek = större;

                        if (mus.LeftButton == ButtonState.Pressed)
                        {

                            textForts = "press enter to continue";
                            if (gameState == 1)
                            {
                                spelare1 = karaktärLista[i];
                            }
                            else if (gameState == 2)
                            {
                                spelare2 = karaktärLista[i];
                            }
                        }
                    }

                    else
                    {
                        karaktärLista[i].storlek = karaktärstorlek;
                    }
                }

            }
        }       
    }
}

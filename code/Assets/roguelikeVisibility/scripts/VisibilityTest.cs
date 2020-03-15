using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Eses.RoguelikeVisibility
{

    // Copyright Sami S.

    // use of any kind without a written permission 
    // from the author is not allowed.

    // DO NOT:
    // Fork, clone, copy or use in any shape or form.

    public class VisibilityTest : MonoBehaviour
    {
        public Text text;

        [Header("Raycasting")]
        public int circleDist = 2;
        public float rayDist = 2;

        [Header("Symbols")]
        public char cellDark = ',';
        public char cellVisible = '.';
        public char cellWall = '#';
        public char cellPlayer = '@';

        // local
        VisibilityChecker visChecker;
        Vector2Int actorPos;


        // level data
        public int[,] level =
        {
            {0,0,0,0,0,0,0},
            {0,1,1,0,1,1,0},
            {0,1,0,0,0,1,0},
            {0,0,0,0,0,1,0},
            {0,1,0,0,0,0,0},
            {0,1,1,0,0,0,0},
            {0,0,0,0,0,0,0}
        };

        public int[,] level2 =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0},
            {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,0},
            {0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
            {0,0,0,0,0,0,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };


        void Start()
        {
            visChecker = new VisibilityChecker(level2, circleDist, rayDist);
            visChecker.Init();
        }


        void Update()
        {
            // move actor with keys
            if (Input.anyKeyDown)
            {
                int posx = (int)Input.GetAxisRaw("Horizontal");
                int posy = (int)Input.GetAxisRaw("Vertical");
                actorPos += new Vector2Int(posx, -posy);
            }

            // Update map
            visChecker.UpdateVisibility(actorPos);

            // Draw map data
            RenderAsciiMap(level2, visChecker.VisibilityData, actorPos);
        }


        void RenderAsciiMap(int[,] mapData, bool[,] visibilityData, Vector2Int actorPos)
        {
            // Clear string
            text.text = "";

            for (int y = 0; y < visibilityData.GetLength(0); y++)
            {
                for (int x = 0; x < visibilityData.GetLength(1); x++)
                {
                    if (x == actorPos.x && y == actorPos.y)
                    {
                        text.text += cellPlayer;
                    }
                    else if (visibilityData[y, x])
                    {
                        if (mapData[y, x] == 0)
                        {
                            text.text += cellVisible;
                        }
                        else if (mapData[y, x] == 1)
                        {
                            text.text += cellWall;
                        }
                    }
                    else
                    {
                        text.text += cellDark;
                    }
                }

                text.text += "\n";
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eses.RoguelikeVisibility
{

    // Copyright Sami S.

    // use of any kind without a written permission 
    // from the author is not allowed.

    // DO NOT:
    // Fork, clone, copy or use in any shape or form.

    public class VisibilityChecker
    {

        // visibility area
        int circleDist = 2;
        float rayDist = 2;

        // Local mapdata
        int[,] mapData;

        // Updated visibility area
        bool[,] mapVisibility;

        // Helpers
        Bounds[,] bounds;
        Rect[,] rects;
        List<Vector2Int> tileCircle;
        List<Vector2Int> rayTiles;

        // visibility
        public bool[,] VisibilityData
        {
            get
            {
                return mapVisibility;
            }
        }



        #region Main

        // ctor
        public VisibilityChecker(int[,] mapData, int circleDist, float rayDist)
        {
            this.mapData = mapData;
            this.circleDist = circleDist;
            this.rayDist = rayDist;
        }

        public void Init()
        {
            CreateCellBounds(this.mapData);
        }

        public void UpdateVisibility(Vector2Int actorPosition)
        {
            this.mapVisibility = new bool[mapData.GetLength(0), mapData.GetLength(1)];
            tileCircle = GetTileCircle(this.mapData, actorPosition, circleDist);
            SetVisibleTilesBounds(this.mapData, this.mapVisibility, tileCircle, actorPosition, rayDist);
        }

        #endregion




        #region Helpers

        // Get a cirle (not interior) of tiles for visibility ray ends
        List<Vector2Int> GetTileCircle(int[,] mapData, Vector2Int actor, float distance)
        {
            List<Vector2Int> list = new List<Vector2Int>();

            for (int angle = 0; angle < 360; angle += 10)
            {
                // Cos and sin to map point somewhere on ring
                var px = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
                var py = distance * Mathf.Sin(angle * Mathf.Deg2Rad);

                // Point on circle, rounded to tile
                var pos = new Vector2Int(Mathf.RoundToInt(px), Mathf.RoundToInt(py));

                // Add player world offset to origin centered circle
                pos += actor;

                // step in 10 degree steps, so points
                // might be many times in same tile
                if (!list.Contains(pos))
                {
                    list.Add(pos);
                }
            }

            return list;
        }

        #endregion




        #region BoundsVersion

        // Create Bounds list that matches each tile
        // Bounds contains intersectRay, which will be used for raytest
        void CreateCellBounds(int[,] data)
        {
            bounds = new Bounds[data.GetLength(0), data.GetLength(1)];

            for (int y = 0; y < data.GetLength(0); y++)
            {
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    bounds[y, x] = new Bounds(new Vector2(x, y), new Vector3(1f, 1f));
                }
            }
        }

        // Get tiles along path from player to each circle edge tile
        static List<Vector2Int> GetRayTilesBounds(Bounds[,] data, Vector2Int player, Vector2Int end, float distance)
        {
            List<Vector2Int> hits = new List<Vector2Int>();

            // int vectors as float vectors
            Vector3 playerPos = new Vector3(player.x, player.y);
            Vector3 endPos = new Vector3(end.x, end.y);

            // dir to circle edge
            Vector3 dir = (endPos - playerPos).normalized;
            var ray = new Ray(playerPos, dir);

            for (int y = 0; y < data.GetLength(0); y++)
            {
                for (int x = 0; x < data.GetLength(1); x++)
                {
                    if (data[y, x].IntersectRay(ray))
                    {
                        var tile = new Vector2Int(x, y);

                        if (Vector2.Distance(player, tile) <= distance)
                        {
                            hits.Add(tile);
                        }
                    }
                }
            }

            // Sort, closest to player is first
            hits.Sort(delegate (Vector2Int a, Vector2Int b) { return Vector2.Distance(player, a).CompareTo(Vector2.Distance(player, b)); });

            return hits;
        }

        // March each ray and set tile visible, until wall comes across
        void SetVisibleTilesBounds(int[,] mapData, bool[,] visData, List<Vector2Int> tileCircle, Vector2Int actor, float rayDist)
        {
            foreach (var circleTile in tileCircle)
            {
                rayTiles = GetRayTilesBounds(bounds, actor, circleTile, rayDist);

                foreach (var t in rayTiles)
                {
                    visData[t.y, t.x] = true;

                    // if wall stop setting visible
                    if (mapData[t.y, t.x] == 1)
                    {
                        // show wall we hit also
                        visData[t.y, t.x] = true;
                        break;
                    }
                }
            }
        }

        #endregion

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LayoutConst
{
    // update values when we agree on a standard, use to simplify calculations
    public const float TileH = 8f; // height of tiles for stage layout
    public const float TileInnerW = 12f; // width of middle (traversable) tile for stage layout
    public const float TileOuterW = 2f; // width of outer (wall) tiles for stage layout
}

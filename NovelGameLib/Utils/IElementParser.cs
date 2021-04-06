using System;
using System.Collections.Generic;
using AngleSharp.Dom;

namespace NovelGameLib.Utils
{
    static class IElementParser
    {
        public static int? ParseInt(IElement element)
        {
            if (!int.TryParse(element.TextContent, out int result))
            {
                return null;
            }

            return result;
        }

        public static bool ParseBool(IElement flag)
        {
            if (flag.TextContent == "f") return false;
            else return true;
        }
    }
}

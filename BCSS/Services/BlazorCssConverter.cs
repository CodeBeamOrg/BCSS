using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSS.Services
{
    public static class BlazorCssConverter
    {
        public static string Convert (string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return string.Empty;
            }

            string[] processedString = className.Split('-');
            string? key = processedString.FirstOrDefault();
            string? value = processedString.LastOrDefault();
            //string? value = string.Join(null, processedString.Skip(1));
            
            if (key == "aspect")
            {
                switch (value)
                {
                    case "video":
                        return "aspect-ratio:16/9";
                    case "square":
                        return "aspect-ratio:1/1";
                    case "auto":
                        return "aspect-ratio:auto";
                    default:
                        return $"aspect-ratio:{value}";
                }
            }

            if (key == "box")
            {
                switch (value)
                {
                    case "border":
                        return "box-sizing:border-box";
                    case "content":
                        return "box-sizing:content-box";
                    default:
                        return $"box-sizing:{value}";
                }
            }

            if (key == "clear")
            {
                switch (value)
                {
                    case "left":
                        return "clear:left";
                    case "right":
                        return "clear:right";
                    case "both":
                        return "clear:both";
                    case "none":
                        return "clear:none";
                    default:
                        return $"clear:{value}";
                }
            }

            if (key == "d")
            {
                switch (value)
                {
                    case "flex":
                        return "display:flex";
                    case "block":
                        return "display:block";
                    default:
                        return $"display:{value}";
                }
            }

            if (key == "float")
            {
                switch (value)
                {
                    case "left":
                        return "float:left";
                    case "right":
                        return "float:right";
                    case "none":
                        return "float:none";
                    default:
                        return $"float:{value}";
                }
            }

            if (key == "h")
            {
                if (value?.Contains("rem") == true)
                {
                    return $"height:{value}";
                }
                if (value?.Contains('.') == true)
                {
                    return $"height:{value}rem";
                }
                if (value?.Contains('%') == true)
                {
                    return $"height:{value}";
                }
                switch (value)
                {
                    case "min":
                        return "height:min-content";
                    case "max":
                        return "height:max-content";
                    case "fit":
                        return "height:fit-content";
                    default:
                        return $"height:{value}px";
                }
            }

            if (key == "hmin")
            {
                if (value?.Contains("rem") == true)
                {
                    return $"min-height:{value}";
                }
                if (value?.Contains('.') == true)
                {
                    return $"min-height:{value}rem";
                }
                if (value?.Contains('%') == true)
                {
                    return $"min-height:{value}";
                }
                switch (value)
                {
                    case "min":
                        return "min-height:min-content";
                    case "max":
                        return "min-height:max-content";
                    case "fit":
                        return "min-height:fit-content";
                    default:
                        return $"min-height:{value}px";
                }
            }

            if (key == "hmax")
            {
                if (value?.Contains("rem") == true)
                {
                    return $"max-height:{value}";
                }
                if (value?.Contains('.') == true)
                {
                    return $"max-height:{value}rem";
                }
                if (value?.Contains('%') == true)
                {
                    return $"max-height:{value}";
                }
                switch (value)
                {
                    case "min":
                        return "max-height:min-content";
                    case "max":
                        return "max-height:max-content";
                    case "fit":
                        return "max-height:fit-content";
                    default:
                        return $"max-height:{value}px";
                }
            }

            if (key == "pos")
            {
                switch (value)
                {
                    case "static":
                        return "position:static";
                    case "fixed":
                        return "position:fixed";
                    case "absolute":
                        return "position:absolute";
                    case "relative":
                        return "position:relative";
                    case "sticky":
                        return "position:sticky";
                    default:
                        return $"position:{value}";
                }
            }

            if (key == "w")
            {
                if (value?.Contains("rem") == true)
                {
                    return $"width:{value}";
                }
                if (value?.Contains('.') == true)
                {
                    return $"width:{value}rem";
                }
                if (value?.Contains('%') == true)
                {
                    return $"width:{value}";
                }
                switch (value)
                {
                    case "min":
                        return "width:min-content";
                    case "max":
                        return "width:max-content";
                    case "fit":
                        return "width:fit-content";
                    default:
                        return $"width:{value}px";
                }
            }

            if (key == "wmin")
            {
                if (value?.Contains("rem") == true)
                {
                    return $"min-width:{value}";
                }
                if (value?.Contains('.') == true)
                {
                    return $"min-width:{value}rem";
                }
                if (value?.Contains('%') == true)
                {
                    return $"min-width:{value}";
                }
                switch (value)
                {
                    case "min":
                        return "min-width:min-content";
                    case "max":
                        return "min-width:max-content";
                    case "fit":
                        return "min-width:fit-content";
                    default:
                        return $"min-width:{value}px";
                }
            }

            if (key == "wmax")
            {
                if (value?.Contains("rem") == true)
                {
                    return $"max-width:{value}";
                }
                if (value?.Contains('.') == true)
                {
                    return $"max-width:{value}rem";
                }
                if (value?.Contains('%') == true)
                {
                    return $"max-width:{value}";
                }
                switch (value)
                {
                    case "min":
                        return "max-width:min-content";
                    case "max":
                        return "max-width:max-content";
                    case "fit":
                        return "max-width:fit-content";
                    default:
                        return $"max-width:{value}px";
                }
            }

            if (key == "z")
            {
                if (value == "auto")
                {
                    return "z-index:auto";
                }
                return $"z-index:{value}";
            }

            return string.Empty;
        }
    }
}

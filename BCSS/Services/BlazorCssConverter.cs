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
                switch (value)
                {
                    case "min":
                        return "height:min-content";
                    case "max":
                        return "height:max-content";
                    case "fit":
                        return "height:fit-content";
                }
                return DimensionResult(value, "height");
            }

            if (key == "hmin")
            {
                switch (value)
                {
                    case "min":
                        return "min-height:min-content";
                    case "max":
                        return "min-height:max-content";
                    case "fit":
                        return "min-height:fit-content";
                }
                return DimensionResult(value, "min-height");
            }

            if (key == "hmax")
            {
                switch (value)
                {
                    case "min":
                        return "max-height:min-content";
                    case "max":
                        return "max-height:max-content";
                    case "fit":
                        return "max-height:fit-content";
                }
                return DimensionResult(value, "max-height");
            }

            if (key == "m")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin");
            }

            if (key == "mt")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-top");
            }

            if (key == "mb")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-bottom");
            }

            if (key == "ml")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-left");
            }

            if (key == "mr")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-right");
            }

            if (key == "mx")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-left") + " " + DimensionResult(value, "margin-right");
            }

            if (key == "my")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-top") + " " + DimensionResult(value, "margin-bottom");
            }

            if (key == "ms")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-inline-start");
            }

            if (key == "me")
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-inline-end");
            }

            if (key == "p")
            {
                return DimensionResult(value, "padding");
            }

            if (key == "pt")
            {
                return DimensionResult(value, "padding-top");
            }

            if (key == "pb")
            {
                return DimensionResult(value, "padding-bottom");
            }

            if (key == "pl")
            {
                return DimensionResult(value, "padding-left");
            }

            if (key == "pr")
            {
                return DimensionResult(value, "padding-right");
            }

            if (key == "px")
            {
                return DimensionResult(value, "padding-left") + " " + DimensionResult(value, "padding-right");
            }

            if (key == "py")
            {
                return DimensionResult(value, "padding-top") + " " + DimensionResult(value, "padding-bottom");
            }

            if (key == "ps")
            {
                return DimensionResult(value, "padding-inline-start");
            }

            if (key == "pe")
            {
                return DimensionResult(value, "padding-inline-end");
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
                switch (value)
                {
                    case "min":
                        return "width:min-content";
                    case "max":
                        return "width:max-content";
                    case "fit":
                        return "width:fit-content";
                }
                return DimensionResult(value, "width");
            }

            if (key == "wmin")
            {
                switch (value)
                {
                    case "min":
                        return "min-width:min-content";
                    case "max":
                        return "min-width:max-content";
                    case "fit":
                        return "min-width:fit-content";
                }
                return DimensionResult(value, "min-width");
            }

            if (key == "wmax")
            {
                switch (value)
                {
                    case "min":
                        return "max-width:min-content";
                    case "max":
                        return "max-width:max-content";
                    case "fit":
                        return "max-width:fit-content";
                }
                return DimensionResult(value, "max-width");
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

        public static string DimensionResult(string? value, string cssName)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value?.Contains("rem") == true)
            {
                return $"{cssName}:{value}";
            }
            if (value?.Contains('.') == true)
            {
                return $"{cssName}:{value}rem";
            }
            if (value?.Contains('%') == true)
            {
                return $"{cssName}:{value}";
            }
            return $"{cssName}:{value}px";
        }

    }
}

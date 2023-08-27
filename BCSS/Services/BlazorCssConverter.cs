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
            if (key?.Contains(':') == true)
            {
                key = key.Split(':').LastOrDefault();
            }
            int keyLength = key?.Length ?? 0;
            string? value = processedString.Length < 2 ? string.Empty : className.Substring(processedString.FirstOrDefault().Length + 1);
            //string? value = string.Join(null, processedString.Skip(1));

            if (string.Equals(key, "aspect", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "border", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (value)
                {
                    case "solid":
                        return "border-style:solid";
                    case "dashed":
                        return "border-style:dashed";
                    case "dotted":
                        return "border-style:dotted";
                    case "double":
                        return "border-style:double";
                    case "hidden":
                        return "border-style:hidden";
                    case "none":
                        return "border-style:none";
                }
                return DimensionResult(value, "border-width");
            }

            if (string.Equals(key, "borderb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-bottom-width");
            }

            if (string.Equals(key, "bordert", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-top-width");
            }

            if (string.Equals(key, "borderl", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-left-width");
            }

            if (string.Equals(key, "borderr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-right-width");
            }

            if (string.Equals(key, "box", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "clear", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "d", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "float", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "h", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "hmin", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "hmax", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "m", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin");
            }

            if (string.Equals(key, "mt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-top");
            }

            if (string.Equals(key, "mb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-bottom");
            }

            if (string.Equals(key, "ml", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-left");
            }

            if (string.Equals(key, "mr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-right");
            }

            if (string.Equals(key, "mx", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-left") + " " + DimensionResult(value, "margin-right");
            }

            if (string.Equals(key, "my", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-top") + " " + DimensionResult(value, "margin-bottom");
            }

            if (string.Equals(key, "ms", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-inline-start");
            }

            if (string.Equals(key, "me", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value?.Replace('n', '-'), "margin-inline-end");
            }

            if (string.Equals(key, "opacity", StringComparison.InvariantCultureIgnoreCase))
            {
                double dividedNum;
                if (double.TryParse(value, out dividedNum))
                {
                    return $"opacity:{(dividedNum / 100d).ToString().Replace(',', '.')}";
                }
                return $"opacity:{value}";
            }

            if (string.Equals(key, "overflowx", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"overflow-x:{value}";
            }

            if (string.Equals(key, "overflowy", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"overflow-y:{value}";
            }

            if (string.Equals(key, "p", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding");
            }

            if (string.Equals(key, "pt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-top");
            }

            if (string.Equals(key, "pb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-bottom");
            }

            if (string.Equals(key, "pl", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-left");
            }

            if (string.Equals(key, "pr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-right");
            }

            if (string.Equals(key, "px", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-left") + " " + DimensionResult(value, "padding-right");
            }

            if (string.Equals(key, "py", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-top") + " " + DimensionResult(value, "padding-bottom");
            }

            if (string.Equals(key, "ps", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-inline-start");
            }

            if (string.Equals(key, "pe", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "padding-inline-end");
            }

            if (string.Equals(key, "pos", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "r", StringComparison.InvariantCultureIgnoreCase) || string.Equals(key, "rounded", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-radius");
            }

            if (string.Equals(key, "rlt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-top-left-radius");
            }

            if (string.Equals(key, "rlb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-bottom-left-radius");
            }

            if (string.Equals(key, "rrt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-top-right-radius");
            }

            if (string.Equals(key, "rrb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-bottom-right-radius");
            }

            if (string.Equals(key, "rt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-top-left-radius") + " " + DimensionResult(value, "border-top-right-radius");
            }

            if (string.Equals(key, "rb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-bottom-left-radius") + " " + DimensionResult(value, "border-bottom-right-radius");
            }

            if (string.Equals(key, "rl", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-top-left-radius") + " " + DimensionResult(value, "border-bottom-left-radius");
            }

            if (string.Equals(key, "rr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-top-right-radius") + " " + DimensionResult(value, "border-bottom-right-radius");
            }

            if (string.Equals(key, "rs", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-start-start-radius") + " " + DimensionResult(value, "border-end-start-radius");
            }

            if (string.Equals(key, "re", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(value, "border-end-end-radius") + " " + DimensionResult(value, "border-start-end-radius");
            }

            if (string.Equals(key, "w", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "wmin", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "wmax", StringComparison.InvariantCultureIgnoreCase))
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

            if (string.Equals(key, "z", StringComparison.InvariantCultureIgnoreCase))
            {
                if (value == "auto")
                {
                    return "z-index:auto";
                }
                return $"z-index:{value}";
            }

            return $"{key?.ToLower()}:{value.ToLower()}";
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

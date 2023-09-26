using System.Data.SqlTypes;
using System.Drawing;

namespace BCSS.Services
{
    public static class BlazorCssConverter
    {
        public static List<string> GetSuffixes(string className)
        {
            var suffixes = new List<string>();
            if (string.IsNullOrWhiteSpace(className))
            {
                return suffixes;
            }

            string key = string.Empty;
            if (className.Contains("--"))
            {
                key = className.Split("--").First();
            }
            else
            {
                key = className.Split('-').First();
            }

            if (key.Contains(':') == false)
            {
                return suffixes;
            }

            string[] partials = key.Replace('/', '-').Split(':');
            for (int i = 0; i < partials.Length - 1; i++)
            {
                suffixes.Add(partials[i]);
            }
            return suffixes;
        }

        public static string Convert(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return string.Empty;
            }

            className = className.ToLower();

            string? key = null;
            string? value = null;
            string[] splittedString = className.Split("--", 2);
            string[] processedString = className.Split('-');

            if (splittedString.Length == 2 && (className.Contains('[') == false || (className.Contains('[') && className.IndexOf("--") < className.IndexOf("["))))
            {
                key = splittedString[0].Split(':').Last();
                value = splittedString[1];
            }
            else
            {
                key = processedString.First().Split(':').Last().Replace('+', '-');
                value = processedString.Length < 2 ? string.Empty : className.Substring(processedString.First().Length + 1);
            }

            string? fullKey = GetFullKeyName(key.Replace("+", null));
            string? processedValue = value;

            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            string subString = GetCustomValue(value);
            processedValue = processedValue.Replace("[" + subString + "]", CustomValueResult(subString));

            //if (processedValue.Contains('[') && processedValue.Contains(']'))
            //{
            //    int startPos = processedValue.IndexOf('[');
            //    int endPos = processedValue.IndexOf("]");
            //    string subString = processedValue.Substring(startPos + 1, endPos - startPos - 1);
            //    processedValue = processedValue.Replace("[" + subString + "]", CustomValueResult(subString));
            //}

            if (string.Equals(fullKey, "aspect-ratio", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (value)
                {
                    case "video":
                        return "aspect-ratio:16/9";
                    case "square":
                        return "aspect-ratio:1/1";
                    default:
                        return $"aspect-ratio:{processedValue}";
                }
            }

            if (string.Equals(fullKey, "background", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (value)
                {
                    case "fixed":
                        return "background-attachment:fixed";
                    case "local":
                        return "background-attachment:local";
                    case "scroll":
                        return "background-attachment:scroll";
                    case "clip-box":
                        return "background-clip:border-box";
                    case "clip-padding":
                        return "background-clip:padding-box";
                    case "clip-content":
                        return "background-clip:content-box";
                    case "clip-text":
                        return "background-clip:text";
                    case "origin-border":
                        return "background-origin:border-box";
                    case "origin-padding":
                        return "background-origin:padding-box";
                    case "origin-content":
                        return "background-origin:content-box";
                    case "bottom":
                        return "background-position:bottom";
                    case "center":
                        return "background-position:center";
                    case "top":
                        return "background-position:top";
                    case "left":
                        return "background-position:left";
                    case "right":
                        return "background-position:right";
                    case "left-bottom":
                        return "background-position:left bottom";
                    case "left-top":
                        return "background-position:left top";
                    case "right-bottom":
                        return "background-position:right bottom";
                    case "right-top":
                        return "background-position:right top";
                    case "repeat":
                        return "background-repeat:repeat";
                    case "no-repeat":
                        return "background-repeat:no-repeat";
                    case "repeat-x":
                        return "background-repeat:repeat-x";
                    case "repeat-y":
                        return "background-repeat:repeat-y";
                    case "repeat-round":
                        return "background-repeat:round";
                    case "repeat-space":
                        return "background-repeat:space";
                    case "auto":
                        return "background-size:auto";
                    case "cover":
                        return "background-size:cover";
                    case "contain":
                        return "background-size:contain";
                    case "none":
                        return "background-image:none";
                }
                return $"background:{processedValue}";
            }

            if (string.Equals(fullKey, "border", StringComparison.InvariantCultureIgnoreCase))
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
                //return DimensionResult(processedValue, "border-width");
                return SpacedResult(value, "border");
            }

            if (string.Equals(fullKey, "border-bottom", StringComparison.InvariantCultureIgnoreCase))
            {
                //return DimensionResult(value, "border-bottom-width");
                return SpacedResult(value, "border-bottom");
            }

            if (string.Equals(fullKey, "border-top", StringComparison.InvariantCultureIgnoreCase))
            {
                return SpacedResult(value, "border-top");
            }

            if (string.Equals(fullKey, "border-left", StringComparison.InvariantCultureIgnoreCase))
            {
                return SpacedResult(value, "border-left");
            }

            if (string.Equals(fullKey, "border-right", StringComparison.InvariantCultureIgnoreCase))
            {
                return SpacedResult(value, "border-right");
            }

            if (string.Equals(fullKey, "box", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (value)
                {
                    case "border":
                        return "box-sizing:border-box";
                    case "content":
                        return "box-sizing:content-box";
                    default:
                        return $"box-sizing:{processedValue}";
                }
            }

            if (string.Equals(fullKey, "height", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "min":
                        return "height:min-content";
                    case "max":
                        return "height:max-content";
                    case "fit":
                        return "height:fit-content";
                }
                return DimensionResult(processedValue, "height");
            }

            if (string.Equals(fullKey, "min-height", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "min":
                        return "min-height:min-content";
                    case "max":
                        return "min-height:max-content";
                    case "fit":
                        return "min-height:fit-content";
                }
                return DimensionResult(processedValue, "min-height");
            }

            if (string.Equals(fullKey, "max-height", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "min":
                        return "max-height:min-content";
                    case "max":
                        return "max-height:max-content";
                    case "fit":
                        return "max-height:fit-content";
                }
                return DimensionResult(processedValue, "max-height");
            }

            if (string.Equals(key, "m", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin");
            }

            if (string.Equals(key, "mt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-top");
            }

            if (string.Equals(key, "mb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-bottom");
            }

            if (string.Equals(key, "ml", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-left");
            }

            if (string.Equals(key, "mr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-right");
            }

            if (string.Equals(key, "mx", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-left") + " " + DimensionResult(processedValue, "margin-right");
            }

            if (string.Equals(key, "my", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-top") + " " + DimensionResult(processedValue, "margin-bottom");
            }

            if (string.Equals(key, "ms", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-inline-start");
            }

            if (string.Equals(key, "me", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-inline-end");
            }

            if (string.Equals(key, "object", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "contain":
                        return "object-fit:contain";
                    case "cover":
                        return "object-fit:cover";
                    case "fill":
                        return "object-fit:fill";
                    case "none":
                        return "object-fit:none";
                    case "scale-down":
                        return "object-fit:scale-down";
                    case "bottom":
                        return "object-position:bottom";
                    case "center":
                        return "object-position:center";
                    case "top":
                        return "object-position:top";
                    case "left":
                        return "object-position:left";
                    case "right":
                        return "object-position:right";
                    case "left-bottom":
                        return "object-position:left bottom";
                    case "left-top":
                        return "object-position:left top";
                    case "right-bottom":
                        return "object-position:right bottom";
                    case "right-top":
                        return "object-position:right top";
                }
            }

            if (string.Equals(fullKey, "opacity", StringComparison.InvariantCultureIgnoreCase))
            {
                double dividedNum;
                if (double.TryParse(processedValue, out dividedNum))
                {
                    return $"opacity:{(dividedNum / 100d).ToString().Replace(',', '.')}";
                }
                return $"opacity:{processedValue}";
            }

            if (string.Equals(fullKey, "overflow-x", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"overflow-x:{processedValue}";
            }

            if (string.Equals(fullKey, "overflow-y", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"overflow-y:{processedValue}";
            }

            if (string.Equals(key, "p", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding");
            }

            if (string.Equals(key, "pt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-top");
            }

            if (string.Equals(key, "pb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-bottom");
            }

            if (string.Equals(key, "pl", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-left");
            }

            if (string.Equals(key, "pr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-right");
            }

            if (string.Equals(key, "px", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-left") + " " + DimensionResult(processedValue, "padding-right");
            }

            if (string.Equals(key, "py", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-top") + " " + DimensionResult(processedValue, "padding-bottom");
            }

            if (string.Equals(key, "ps", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-inline-start");
            }

            if (string.Equals(key, "pe", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-inline-end");
            }

            if (string.Equals(key, "r", StringComparison.InvariantCultureIgnoreCase) || string.Equals(key, "rounded", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-radius");
            }

            if (string.Equals(key, "rlt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-top-left-radius");
            }

            if (string.Equals(key, "rlb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-bottom-left-radius");
            }

            if (string.Equals(key, "rrt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-top-right-radius");
            }

            if (string.Equals(key, "rrb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-bottom-right-radius");
            }

            if (string.Equals(key, "rt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-top-left-radius") + " " + DimensionResult(processedValue, "border-top-right-radius");
            }

            if (string.Equals(key, "rb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-bottom-left-radius") + " " + DimensionResult(processedValue, "border-bottom-right-radius");
            }

            if (string.Equals(key, "rl", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-top-left-radius") + " " + DimensionResult(processedValue, "border-bottom-left-radius");
            }

            if (string.Equals(key, "rr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-top-right-radius") + " " + DimensionResult(processedValue, "border-bottom-right-radius");
            }

            if (string.Equals(key, "rs", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-start-start-radius") + " " + DimensionResult(processedValue, "border-end-start-radius");
            }

            if (string.Equals(key, "re", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "border-end-end-radius") + " " + DimensionResult(processedValue, "border-start-end-radius");
            }

            if (string.Equals(key, "resize", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "x":
                        return "resize:horizontal";
                    case "y":
                        return "resize:vertical";
                    case "":
                    case null:
                        return "resize:both";
                }
                return $"resize:{processedValue}";
            }

            if (string.Equals(fullKey, "width", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "min":
                        return "width:min-content";
                    case "max":
                        return "width:max-content";
                    case "fit":
                        return "width:fit-content";
                }
                return DimensionResult(processedValue, "width");
            }

            if (string.Equals(fullKey, "min-width", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "min":
                        return "min-width:min-content";
                    case "max":
                        return "min-width:max-content";
                    case "fit":
                        return "min-width:fit-content";
                }
                return DimensionResult(processedValue, "min-width");
            }

            if (string.Equals(fullKey, "max-width", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "min":
                        return "max-width:min-content";
                    case "max":
                        return "max-width:max-content";
                    case "fit":
                        return "max-width:fit-content";
                }
                return DimensionResult(processedValue, "max-width");
            }

            return $"{fullKey}:{processedValue}";
        }

        public static string GetFullKeyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            string val = name.Replace("-", null);
            switch (val)
            {
                case "ar":
                case "aspect":
                    return "aspect-ratio";
                case "b":
                    return "border";
                case "bb":
                case "borderb":
                    return "border-bottom";
                case "bt":
                case "bordert":
                    return "border-top";
                case "bl":
                case "borderl":
                    return "border-left";
                case "br":
                case "borderr":
                    return "border-right";
                case "bg":
                    return "background";
                case "box":
                case "boxsizing":
                    return "box-sizing";
                case "c":
                    return "cursor";
                case "d":
                    return "display";
                case "f":
                    return "font";
                case "h":
                    return "height";
                case "hmin":
                    return "min-height";
                case "hmax":
                    return "max-height";
                case "o":
                    return "opacity";
                case "of":
                case "flow":
                    return "overflow";
                case "ofx":
                case "overflowx":
                case "flowx":
                    return "overflow-x";
                case "ofy":
                case "overflowy":
                case "flowy":
                    return "overflow-y";
                case "pos":
                    return "position";
                case "scroll":
                    return "scroll-behavior";
                case "select":
                    return "user-select";
                case "touch":
                    return "touch-action";
                case "v":
                case "vis":
                    return "visibility";
                case "w":
                    return "width";
                case "wmin":
                case "w-min":
                    return "min-width";
                case "wmax":
                case "w-max":
                    return "max-width";
                case "ws":
                    return "white-space";
                case "z":
                case "zindex":
                    return "z-index";
                default:
                    return name;
            }
        }

        public static string DimensionResult(string? value, string cssName)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value?.Contains("rem") == true || value?.Contains('%') == true || value?.Contains("vw") == true || value?.Contains("vh") == true || value?.Contains("em") == true)
            {
                return $"{cssName}:{value}";
            }
            if (value?.Contains('.') == true)
            {
                return $"{cssName}:{value}0rem";
            }
            if (value?.Contains(',') == true)
            {
                return $"{cssName}:{value.Replace(',', '.')}0em";
            }
            return $"{cssName}:{value}px";
        }

        public static string SpacedResult(string? value, string? name = null)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string subString = GetCustomValue(value);
            return $"{(string.IsNullOrEmpty(name) ? null : name + ":")}{value.Replace("["+subString+"]", CustomValueResult(subString)).Replace('-', '*')}";
        }

        public static string CustomValueResult(string? val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return string.Empty;
            }
            if (val.Contains('#'))
            {
                Color color = new();
                var ps = val.Split(',').First();
                try
                {
                    color = ColorTranslator.FromHtml(ps);
                }
                catch (Exception)
                {

                }
                string result = color.R + "," + color.G + "," + color.B + "," + color.A;
                return $"rgba({result})";
            }
            if (val.Contains("--"))
            {
                return $"var({val})";
            }
            if (val.Contains(','))
            {
                return $"rgba({val})";
            }
            return val;
        }

        public static string GetCustomValue(string? val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return string.Empty;
            }
            if (val.Contains('[') && val.Contains(']'))
            {
                int startPos = val.IndexOf('[');
                int endPos = val.IndexOf("]");
                string subString = val.Substring(startPos + 1, endPos - startPos - 1);
                return subString;
            }
            return val;
        }

    }
}

using System.Drawing;

namespace BCSS.Services
{
    public static class BlazorCssConverter
    {
        public static List<string> GetPrefixes(string className)
        {
            var prefixes = new List<string>();
            if (string.IsNullOrWhiteSpace(className))
            {
                return prefixes;
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
                return prefixes;
            }

            string[] partials = key.Replace('/', '-').Split(':');
            for (int i = 0; i < partials.Length - 1; i++)
            {
                prefixes.Add(partials[i]);
            }
            return prefixes;
        }

        public static string Convert(string className, BlazorCssProvider? provider = null)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return string.Empty;
            }

            bool _shouldNotLowercase = false;
            if (className.Contains('[') && className.Contains('/'))
            {
                _shouldNotLowercase = true;
            }

            if (_shouldNotLowercase == false)
            {
                className = className.ToLower();
            }

            if (className.Contains('-') == false)
            {
                return GetOneWordResult(className);
            }

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

            if (_shouldNotLowercase == true)
            {
                key = key.ToLower();
            }
            
            string? fullKey = GetFullKeyName(key.Replace("+", null));
            string? processedValue = value;

            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            string subString = GetCustomValue(value);
            processedValue = processedValue.Replace("[" + subString + "]", CustomValueResult(subString));

            if (string.Equals(fullKey, "animation", StringComparison.InvariantCultureIgnoreCase))
            {
                return SpacedResult(value, "animation");
            }

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

            if (string.Equals(fullKey, "backdrop-filter", StringComparison.InvariantCultureIgnoreCase))
            {
                string[] splitVal = processedValue.Split('-');
                if (2 <= splitVal.Length)
                {
                    return $"backdrop-filter:{splitVal[0]}({splitVal[1]})";
                }
                return $"backdrop-filter:{processedValue})";
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
                return SpacedResult(value, "border");
            }

            if (string.Equals(fullKey, "border-bottom", StringComparison.InvariantCultureIgnoreCase))
            {
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

            if (string.Equals(fullKey, "bottom", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue.Replace('n', '-'), fullKey);
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

            if (string.Equals(fullKey, "filter", StringComparison.InvariantCultureIgnoreCase))
            {
                string[] splitVal = processedValue.Split('-');
                if (2 <= splitVal.Length)
                {
                    return $"filter:{splitVal[0]}({splitVal[1]})";
                }
                return $"filter:{processedValue})";
            }

            if (string.Equals(fullKey, "flex", StringComparison.InvariantCultureIgnoreCase))
            {
                switch (processedValue)
                {
                    case "center":
                        return "place-content:center place-items:center";
                    case "start":
                        return "place-content:start place-items:start";
                    case "end":
                        return "place-content:end place-items:end";
                    case "top-left":
                        return "place-content:start place-items:start";
                    case "top-center":
                        return "place-content:center place-items:start";
                    case "top-right":
                        return "place-content:end place-items:start";
                    case "center-left":
                        return "place-content:start place-items:center";
                    case "center-right":
                        return "place-content:end place-items:center";
                    case "bottom-right":
                        return "place-content:end place-items:end";
                    case "bottom-center":
                        return "place-content:center place-items:end";
                    case "bottom-left":
                        return "place-content:start place-items:end";
                    case "col":
                        return "flex-direction:column";
                    case "col-reverse":
                        return "flex-direction:column-reverse";
                    case "row":
                        return "flex-direction:row";
                    case "row-reverse":
                        return "flex-direction:row-reverse";
                    case "wrap":
                        return "flex-wrap:wrap";
                    case "wrap-reverse":
                        return "flex-wrap:wrap-reverse";
                }
                return SpacedResult(processedValue, fullKey);
            }

            if (string.Equals(fullKey, "gap", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, fullKey, provider);
            }

            if (string.Equals(fullKey, "height", StringComparison.InvariantCultureIgnoreCase))
            {
                return GetHeightWidthKeywordResult(processedValue, fullKey);
            }

            if (string.Equals(fullKey, "left", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue.Replace('n', '-'), fullKey);
            }

            if (string.Equals(fullKey, "min-height", StringComparison.InvariantCultureIgnoreCase))
            {
                return GetHeightWidthKeywordResult(processedValue, fullKey);
            }

            if (string.Equals(fullKey, "max-height", StringComparison.InvariantCultureIgnoreCase))
            {
                return GetHeightWidthKeywordResult(processedValue, fullKey);
            }

            if (string.Equals(key, "ma", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin", provider);
            }

            if (string.Equals(key, "mt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-top", provider);
            }

            if (string.Equals(key, "mb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-bottom", provider);
            }

            if (string.Equals(key, "ml", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-left", provider);
            }

            if (string.Equals(key, "mr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-right", provider);
            }

            if (string.Equals(key, "mx", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-left", provider) + " " + DimensionResult(processedValue, "margin-right", provider);
            }

            if (string.Equals(key, "my", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-top", provider) + " " + DimensionResult(processedValue, "margin-bottom", provider);
            }

            if (string.Equals(key, "ms", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-inline-start", provider);
            }

            if (string.Equals(key, "me", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue?.Replace('n', '-'), "margin-inline-end", provider);
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

            if (string.Equals(fullKey, "origin", StringComparison.InvariantCultureIgnoreCase))
            {
                return SpacedResult(value, "transform-origin");
            }

            if (string.Equals(fullKey, "overflow-x", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"overflow-x:{processedValue}";
            }

            if (string.Equals(fullKey, "overflow-y", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"overflow-y:{processedValue}";
            }

            if (string.Equals(key, "pa", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding", provider);
            }

            if (string.Equals(key, "pt", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-top", provider);
            }

            if (string.Equals(key, "pb", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-bottom", provider);
            }

            if (string.Equals(key, "pl", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-left", provider);
            }

            if (string.Equals(key, "pr", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-right", provider);
            }

            if (string.Equals(key, "px", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-left", provider) + " " + DimensionResult(processedValue, "padding-right", provider);
            }

            if (string.Equals(key, "py", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-top", provider) + " " + DimensionResult(processedValue, "padding-bottom", provider);
            }

            if (string.Equals(key, "ps", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-inline-start", provider);
            }

            if (string.Equals(key, "pe", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue, "padding-inline-end", provider);
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

            if (string.Equals(fullKey, "right", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue.Replace('n', '-'), fullKey);
            }

            if (string.Equals(fullKey, "rotate", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:rotate({processedValue.Replace('n', '-')}deg)";
            }

            if (string.Equals(fullKey, "rotatex", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:rotateX({processedValue.Replace('n', '-')}deg)";
            }

            if (string.Equals(fullKey, "rotatey", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:rotateY({processedValue.Replace('n', '-')}deg)";
            }

            if (string.Equals(fullKey, "rotatez", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:rotateZ({processedValue.Replace('n', '-')}deg)";
            }

            if (string.Equals(fullKey, "scale", StringComparison.InvariantCultureIgnoreCase))
            {
                if (double.TryParse(processedValue, out double result))
                {
                    return $"transform:scale({(result / 100d).ToString().Replace(',', '.')})";
                }
                else
                {
                    return $"transform:scale({processedValue})";
                }
            }

            if (string.Equals(fullKey, "scalex", StringComparison.InvariantCultureIgnoreCase))
            {
                if (double.TryParse(processedValue, out double result))
                {
                    return $"transform:scaleX({(result / 100d).ToString().Replace(',', '.')})";
                }
                else
                {
                    return $"transform:scaleX({processedValue})";
                }
            }

            if (string.Equals(fullKey, "scaley", StringComparison.InvariantCultureIgnoreCase))
            {
                if (double.TryParse(processedValue, out double result))
                {
                    return $"transform:scaleY({(result / 100d).ToString().Replace(',', '.')})";
                }
                else
                {
                    return $"transform:scaleY({processedValue})";
                }
            }

            if (string.Equals(fullKey, "shadow", StringComparison.InvariantCultureIgnoreCase))
            {
                if (processedValue.Contains("inset"))
                {
                    string[] splitVal = processedValue.Split("-");
                    if (int.TryParse(splitVal.Last(), out int splitResult))
                    {
                        if (splitResult == 0)
                        {
                            return "box-shadow:none";
                        }
                        return $"box-shadow:inset*0px*-{1 + (splitResult / 2)}px*{2 + (splitResult / 2) + (splitResult / 10)}px*{splitResult / 3}px*rgba(0,0,0,0.15),inset*0px*{1 + (splitResult / 2)}px*{2 + (splitResult / 2) + (splitResult / 10)}px*{splitResult / 3}px*rgba(0,0,0,0.15)";
                    }
                }
                if (int.TryParse(processedValue, out int result))
                {
                    if (result == 0)
                    {
                        return "box-shadow:none";
                    }
                    return $"box-shadow:0px*{1 + (result / 2)}px*{2 + (result / 2) + (result / 10)}px*{result / 3}px*rgba(0,0,0,0.15)";
                }
                else
                {
                    return $"box-shadow:{processedValue}";
                }
                
            }

            if (string.Equals(fullKey, "shadow-inset", StringComparison.InvariantCultureIgnoreCase))
            {
                if (int.TryParse(processedValue, out int result))
                {
                    if (result == 0)
                    {
                        return "box-shadow:none";
                    }
                    return $"box-shadow:inset*0px*-{1 + (result / 2)}px*{2 + (result / 2) + (result / 10)}px*{result / 3}px*rgba(0,0,0,0.15),inset*0px*{1 + (result / 2)}px*{2 + (result / 2) + (result / 10)}px*{result / 3}px*rgba(0,0,0,0.15)";
                }
                else
                {
                    return $"box-shadow:{processedValue}";
                }

            }

            if (string.Equals(fullKey, "skew", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:skew({processedValue}deg,{processedValue}deg)";
            }

            if (string.Equals(fullKey, "skewx", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:skewX({processedValue}deg)";
            }

            if (string.Equals(fullKey, "skewy", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:skewY({processedValue}deg)";
            }

            if (string.Equals(fullKey, "top", StringComparison.InvariantCultureIgnoreCase))
            {
                return DimensionResult(processedValue.Replace('n', '-'), fullKey);
            }

            if (string.Equals(fullKey, "translate", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:translate({processedValue}px,{processedValue}px)";
            }

            if (string.Equals(fullKey, "translatex", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:translateX({processedValue}px)";
            }

            if (string.Equals(fullKey, "translatey", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:translateY({processedValue}px)";
            }

            if (string.Equals(fullKey, "translatez", StringComparison.InvariantCultureIgnoreCase))
            {
                return $"transform:translateZ({processedValue}px)";
            }

            if (string.Equals(fullKey, "transition", StringComparison.InvariantCultureIgnoreCase))
            {
                return SpacedResult(value, "transition");
            }

            if (string.Equals(fullKey, "width", StringComparison.InvariantCultureIgnoreCase))
            {
                return GetHeightWidthKeywordResult(processedValue, fullKey);
            }

            if (string.Equals(fullKey, "min-width", StringComparison.InvariantCultureIgnoreCase))
            {
                return GetHeightWidthKeywordResult(processedValue, fullKey);
            }

            if (string.Equals(fullKey, "max-width", StringComparison.InvariantCultureIgnoreCase))
            {
                return GetHeightWidthKeywordResult(processedValue, fullKey);
            }

            List<string> filterValues = new() { "blur", "brightness", "contrast", "grayscale", "invert", "saturate", "sepia" };
            if (filterValues.Contains(fullKey))
            {
                return  $"filter:{fullKey}({DimensionResult(processedValue)})";
            }
            if (fullKey.StartsWith("bd") && filterValues.Contains(fullKey.Substring(2)))
            {
                return $"backdrop-filter:{fullKey.Substring(2)}({DimensionResult(processedValue)})";
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
                case "bdfilter":
                    return "backdrop-filter";
                case "bgc":
                case "bgcolor":
                    return "background-color";
                case "bgi":
                case "bgimage":
                    return "background-image";
                case "box":
                case "boxsizing":
                    return "box-sizing";
                case "c":
                    return "color";
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
                case "p":
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

        public static string DimensionResult(string? value, string? cssName = null, BlazorCssProvider? provider = null)
        {
            if (value == null)
            {
                return string.Empty;
            }
            bool isNameNull = string.IsNullOrEmpty(cssName);

            if (value?.Contains("rem") == true || value?.Contains('%') == true || value?.Contains("vw") == true || value?.Contains("vh") == true || value?.Contains("em") == true || value?.Contains("deg") == true)
            {
                return $"{(isNameNull ? null : cssName + ":")}{value}";
            }
            if (value?.Contains('.') == true)
            {
                return $"{(isNameNull ? null : cssName + ":")}{value}0rem";
            }
            if (value?.Contains(',') == true)
            {
                return $"{(isNameNull ? null : cssName + ":")}{value.Replace(',', '.')}0em";
            }
            if (provider != null && int.TryParse(value, out int result))
            {
                return $"{(isNameNull ? null : cssName + ":")}{result * provider?.Spacing}px";
            }
            return $"{(isNameNull ? null : cssName + ":")}{value}px";
        }

        public static string SpacedResult(string? value, string? name = null)
        {
            if (value == null)
            {
                return string.Empty;
            }

            List<string> partialValues = new();

            if (value.Contains("ease-in-out"))
            {
                partialValues.Add("ease+in+out");
                value = value.Replace("ease-in-out", "");
            }
            if (value.Contains("ease-in"))
            {
                partialValues.Add("ease+in");
                value = value.Replace("ease-in", "");
            }
            if (value.Contains("ease-out"))
            {
                partialValues.Add("ease+out");
                value = value.Replace("ease-out", "");
            }
            if (value.Contains("background-color"))
            {
                partialValues.Add("background+color");
                value = value.Replace("background-color", "");
            }
            if (value.Contains("border-color"))
            {
                partialValues.Add("border+color");
                value = value.Replace("border-color", "");
            }
            if (value.Contains("box-shadow"))
            {
                partialValues.Add("box+shadow");
                value = value.Replace("box-shadow", "");
            }
            if (value.Contains("backdrop-filter"))
            {
                partialValues.Add("backdrop+filter");
                value = value.Replace("backdrop-filter", "");
            }

            string[] splittedValue = value.Split('-');
            

            foreach (var val in splittedValue)
            {
                if (string.IsNullOrEmpty(val))
                {
                    continue;
                }
                if (name == null)
                {
                    partialValues.Add(val);
                    continue;
                }
                if (name == "transition" || name == "animation")
                {
                    if (double.TryParse(val, out double result))
                    {
                        partialValues.Add(result.ToString().Replace(',', '.') + "s");
                        continue;
                    }
                }

                if (name.Contains("border"))
                {
                    if (double.TryParse(val, out double result))
                    {
                        partialValues.Add(result.ToString().Replace(',', '.') + "px");
                        continue;
                    }
                }

                partialValues.Add(val);
            }
            string unifiedValue = string.Join("-", partialValues);
            string subString = GetCustomValue(unifiedValue);
            return $"{(string.IsNullOrEmpty(name) ? null : name + ":")}{unifiedValue.Replace("["+subString+"]", CustomValueResult(subString)).Replace('-', '*')}";
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
            if (val.StartsWith('/'))
            {
                return $"url({val.Substring(1)})";
            }
            return val;
        }

        public static string GetHeightWidthKeywordResult(string value, string cssProp)
        {
            switch (value)
            {
                case "full":
                    return $"{cssProp}:100%";
                case "screen":
                    return $"{cssProp}:100{(cssProp.Contains("width") ? "vw" : "vh")}";
                case "min":
                    return $"{cssProp}:min-content";
                case "max":
                    return $"{cssProp}:max-content";
                case "fit":
                    return $"{cssProp}:fit-content";
            }
            return DimensionResult(value, cssProp);
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

        public static string PostProcess(string? val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return string.Empty; 
            }

            return val.Replace('+', '-').Replace('*', ' ');
        }

        public static string GetOneWordResult(string? className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return string.Empty;
            }

            if (className.Contains(':'))
            {
                className = className.Split(':').Last();
            }

            switch (className)
            {
                case "absolute":
                    return "position:absolute";
                case "fill":
                    return "height:100% width:100%";
                case "fixed":
                    return "position:fixed";
                case "hidden":
                    return "display:none";
                case "invisible":
                    return "visibility:hidden";
                case "relative":
                    return "position:relative";
                case "static":
                    return "position:static";
                case "sticky":
                    return "position:sticky";
                case "visible":
                    return "visibility:visible";
            }

            return className;
        }

    }
}

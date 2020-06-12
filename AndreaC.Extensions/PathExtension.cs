using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AndreaC.Extensions
{
    public static partial class PathExtension
    {
        public static string GetAvailableFileName(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("Path cannot be null");
            if (File.Exists(path) == false) return path;

            string filename = Path.GetFileName(path);
            var match = Regex.Match(filename, @"\((?<i>[\d]+)\)");
            if (match.Success &&
                match.Groups != null &&
                match.Groups.Count > 0 &&
                match.Groups["i"] != null &&
                int.TryParse(match.Groups["i"].Value, out int i))
            {
                filename = filename.Remove(match.Groups["i"].Index, match.Groups["i"].Length);
                filename = filename.Insert(match.Groups["i"].Index, (i + 1).ToString());
            }
            else filename = $"{Path.GetFileNameWithoutExtension(path)} (1){Path.GetExtension(path)}";
            return GetAvailableFileName(Path.Combine(Path.GetDirectoryName(path), filename));
        }
    }
}

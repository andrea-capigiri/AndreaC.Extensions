using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AndreaC.Extensions
{
    public partial class PathExtension
    {
        private static string GetAvailableFileName(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("Path cannot be null");
            if (File.Exists(path) == false) return path;

            string filename = Path.GetFileName(path);
            var match = Regex.Match(filename, @"\((?<i>[\d]+)\)");
            if (match.Success &&
                match.Groups.TryGetValue("i", out Group iGrp) &&
                iGrp != null &&
                int.TryParse(iGrp.Value, out int i))
            {
                filename = filename.Remove(iGrp.Index, iGrp.Length);
                filename = filename.Insert(iGrp.Index, (i + 1).ToString());
            }
            else filename = $"{Path.GetFileNameWithoutExtension(path)} (1){Path.GetExtension(path)}";
            return GetAvailableFileName(Path.Combine(Path.GetDirectoryName(path), filename));
        }
    }
}

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

public static class HelperFunction
{
    public static string StringToSlug(string name)
    {
        // Chuyển đổi chuỗi thành dạng slug (ví dụ: "Xin Chào Việt Nam" -> "xin-chao-viet-nam")
        if (string.IsNullOrWhiteSpace(name)) return string.Empty;

        // 1. Chuyển thành chữ thường
        string slug = name.ToLowerInvariant().Trim();

        // 2. Thay "đ" bằng "d" (chữ đ không bị tách dấu khi Normalize)
        slug = slug.Replace("đ", "d");

        // 3. Tách dấu ra khỏi ký tự gốc (ví dụ: "ế" -> "e" + dấu mũ + dấu sắc), rồi bỏ các dấu
        string normalized = slug.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }
        slug = sb.ToString().Normalize(NormalizationForm.FormC);

        // 4. Thay các ký tự không phải chữ/số bằng dấu "-"
        slug = Regex.Replace(slug, @"[^a-z0-9]+", "-");

        // 5. Bỏ dấu "-" ở đầu và cuối chuỗi
        slug = slug.Trim('-');

        return slug;
    }
}

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public static class StringHelpers
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Hàm tạo chuỗi ngẫu nhiên tùy chỉnh
        /// </summary>
        /// <param name="length">Độ dài chuỗi muốn lấy</param>
        /// <param name="useNumbers">Có bao gồm số hay không</param>
        /// <param name="useLetters">Có bao gồm chữ cái hay không</param>
        public static string GenerateRandomString(int length, bool useNumbers = true, bool useLetters = true)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            string charPool = "";
            if (useLetters) charPool += letters;
            if (useNumbers) charPool += numbers;

            // Trường hợp người dùng không chọn cả 2 (fail-safe)
            if (string.IsNullOrEmpty(charPool)) return string.Empty;

            var result = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                result.Append(charPool[_random.Next(charPool.Length)]);
            }

            return result.ToString();
        }

        public static string ToUrlFriendly(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Chuyển sang chữ thường
            string str = input.ToLowerInvariant();

            // Chuẩn hóa Unicode FormD (tách dấu khỏi ký tự)
            str = str.Normalize(NormalizationForm.FormD);

            // Loại bỏ dấu tiếng Việt
            var sb = new StringBuilder();
            foreach (var ch in str)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }
            str = sb.ToString().Normalize(NormalizationForm.FormC);

            // Thay ký tự đặc biệt bằng dấu gạch ngang
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");   // bỏ ký tự lạ
            str = Regex.Replace(str, @"\s+", "-");           // thay khoảng trắng
            str = Regex.Replace(str, @"-+", "-");            // bỏ trùng dấu '-'

            return str.Trim('-');
        }

        /// <summary>
        /// Viết hoa chữ cái đầu tiên của chuỗi, phần còn lại viết thường.
        /// </summary>
        /// <param name="input">Chuỗi cần xử lý</param>
        /// <returns></returns>
        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        /// <summary>
        /// Rút gọn chuỗi đến độ dài tối đa chỉ định, nhưng cố gắng cắt tại vị trí của một từ để tránh cắt nửa từ.
        /// </summary>
        /// <param name="input">Chuỗi cần xử lý</param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string ShortenAtWord(string input, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length <= maxLength)
                return input;

            var shortened = input.Substring(0, maxLength);

            int lastSpace = shortened.LastIndexOf(' ');
            if (lastSpace > 0)
                shortened = shortened.Substring(0, lastSpace);

            return shortened + "...";
        }

        /// <summary>
        /// Định dạng một giá trị số thập phân (có thể null) thành chuỗi tiền tệ theo culture chỉ định.
        /// </summary>
        /// <param name="amount">
        /// Giá trị tiền tệ cần định dạng. Nếu <c>null</c> sẽ trả về giá trị tiền tệ bằng 0.
        /// </param>
        /// <param name="cultureCode">
        /// Mã culture (ví dụ: <c>"vi-VN"</c>) dùng để định dạng tiền tệ.
        /// </param>
        /// <returns>
        /// Chuỗi tiền tệ được định dạng theo culture tương ứng.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Ném ra khi <paramref name="cultureCode"/> bị null, rỗng hoặc không hợp lệ.
        /// </exception>
        public static string FormatCurrency(decimal? amount, string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(cultureCode))
                throw new ArgumentException("Culture code must not be null or empty.", nameof(cultureCode));

            var culture = new CultureInfo(cultureCode);

            var value = amount ?? 0m;

            return value.ToString("C0", culture);
        }

        /// <summary>
        /// Loại bỏ tất cả các thẻ HTML khỏi chuỗi đầu vào, trả về một chuỗi chỉ chứa văn bản thuần túy.
        /// </summary>
        /// <param name="input">Đầu vào</param>
        /// <returns>Một chuỗi không chứa bất kì thẻ HTML nào</returns>
        public static string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Viết hoa chữ cái đầu tiên của mỗi từ trong chuỗi, phần còn lại viết thường, và loại bỏ khoảng trắng thừa.
        /// </summary>
        /// <param name="input">Chuỗi đầu vào</param>
        /// <returns>Chuỗi sau khi đã chuẩn hóa viết hoa chữ đầu mỗi từ</returns>
        public static string NormalizeFullName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = Regex.Replace(input.Trim(), @"\s+", " ");

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        /// <summary>
        /// Kiểm tra xem chuỗi đầu vào có phải là một số điện thoại hợp lệ tại Việt Nam hay không. 
        /// Số điện thoại hợp lệ phải bắt đầu bằng "0" hoặc "+84", theo sau là một trong các chữ số 3, 5, 7, 8, 
        /// hoặc 9, và tiếp theo là 8 chữ số nữa.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidVietnamPhone(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(input, @"^(0|\+84)[3|5|7|8|9]\d{8}$");
        }

        /// <summary>
        /// Lấy chữ cái đầu tiên của mỗi từ trong chuỗi đầu vào, viết hoa và nối chúng lại thành một chuỗi mới.
        /// </summary>
        /// <param name="input">Họ và tên người dùng</param>
        /// <returns>Chuỗi viết tắt tên của người dùng (Nguyễn Văn A -> NVA)</returns>
        public static string GetInitials(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var words = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Concat(words.Select(w => char.ToUpper(w[0])));
        }

        /// <summary>
        /// So sánh hai chuỗi sau khi đã loại bỏ dấu tiếng Việt và chuyển về dạng URL-friendly,
        /// </summary>
        /// <param name="source">Nguồn so sánh</param>
        /// <param name="keyword">Từ khóa</param>
        /// <returns>Kết quả trùng khớp hay không?</returns>
        public static bool ContainsIgnoreAccent(string source, string keyword)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(keyword))
                return false;

            var s1 = ToUrlFriendly(source);
            var s2 = ToUrlFriendly(keyword);

            return s1.Contains(s2);
        }

        /// <summary>
        /// Hàm này sẽ loại bỏ tất cả các ký tự không phải là chữ số từ chuỗi đầu vào, giúp chuẩn hóa số CMND/CCCD về dạng chỉ chứa số.
        /// </summary>
        /// <param name="input">Căn cước công dân cần xử lý</param>
        /// <returns>Số căn cước sau khi chuẩn hóa</returns>
        public static string NormalizeIdentityNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return Regex.Replace(input, @"\D", "");
        }
    }
}

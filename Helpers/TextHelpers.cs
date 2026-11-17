namespace WebVanChuyenLogicstic.Helpers
{
    public static class TextHelpers
    {
        /// <summary>
        /// Tách chuỗi thành 2 phần: phần đầu (giữ nguyên) và N từ cuối (để tô màu).
        /// Luôn giữ lại ít nhất 1 từ ở phần đầu, tránh trường hợp tô cam toàn bộ câu.
        /// </summary>
        public static (string Start, string End) SplitLastWords(this string text, int lastWordsCount)
        {
            if (string.IsNullOrWhiteSpace(text)) return (text, "");
            /* Câu rỗng hoặc chỉ toàn khoảng trắng: không có gì để tách,
               trả về nguyên văn cho "Start", "End" để trống */

            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            /* Cắt chuỗi thành mảng các từ, dựa theo dấu cách.
               StringSplitOptions.RemoveEmptyEntries: nếu trong chuỗi có 2 dấu
               cách liền nhau (gõ nhầm), không tạo ra phần tử rỗng xen giữa —
               tránh lỗi đếm sai số từ */

            if (words.Length <= 1) return (text, "");
            /* Nếu câu chỉ có 1 từ (hoặc 0 từ) — không tách được, vì tách ra
               sẽ khiến "Start" rỗng, cả câu tô cam hết, mất điểm nhấn
               tương phản. Trường hợp này trả về nguyên văn, không tô màu gì */

            var take = Math.Min(lastWordsCount, words.Length - 1);
            /* "take" là số từ THỰC SỰ sẽ được tô cam.
               Dùng Math.Min để không bao giờ lấy nhiều hơn (words.Length - 1) —
               tức LUÔN chừa lại ít nhất 1 từ ở phần đầu.
               Ví dụ: câu chỉ có 3 từ, lastWordsCount truyền vào là 4
               → Math.Min(4, 3-1) = Math.Min(4, 2) = 2 — chỉ tô 2 từ cuối,
               không báo lỗi, không tràn mảng */

            var startWords = words[..^take];
            /* Cú pháp "[..^take]" là Range + Index từ cuối của C# (có từ C# 8):
               "^take" nghĩa là "đếm từ CUỐI mảng vào, lùi lại đúng [take] phần tử".
               "[..^take]" nghĩa là "lấy từ đầu mảng (0) cho tới vị trí đó
               (không bao gồm)" — tức lấy hết các từ TRỪ [take] từ cuối cùng */

            var endWords = words[^take..];
            /* Ngược lại: "[^take..]" nghĩa là "lấy từ vị trí [take từ cuối]
               cho tới hết mảng" — tức chỉ lấy đúng [take] từ cuối cùng */

            return (string.Join(' ', startWords), string.Join(' ', endWords));
            /* Join lại 2 mảng con thành 2 chuỗi, nối bằng dấu cách — trả về
               dạng tuple (Start, End) để nơi gọi hàm destructure ra 2 biến
               cùng lúc, ví dụ: var (a, b) = "...".SplitLastWords(4); */
        }
    }
}

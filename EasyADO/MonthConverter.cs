namespace EasyADO
{
    public static class MonthConverter
    {
        public static string ToText(int monthIndex)
        {
            return monthIndex switch
            {
                1 => "January",
                2 => "Feburary",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => "Invalid Month",
            };
        }
        public static string ToKhmerText(int monthIndex)
        {
            return monthIndex switch
            {
                1 => "មករា",
                2 => "កម្ភៈ",
                3 => "មីនា",
                4 => "មេសា",
                5 => "ឧសភា",
                6 => "មិថុនា",
                7 => "កក្កដា",
                8 => "សីហា",
                9 => "កញ្ញា",
                10 => "តុលា",
                11 => "វិច្ឆិកា",
                12 => "ធ្នូ",
                _ => "ខែមិនត្រឹមត្រូវ",
            };
        }
    }
}

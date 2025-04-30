using System;
namespace MarsXApps.Models
{
    public class SelectModel
    {
        public SelectModel()
        {
            Text = string.Empty;
        }

        public int Value { get; set; }
        public string Text { get; set; }

    }

    public class SelectOptionModel
    {
        public SelectOptionModel()
        {
            Option1 = string.Empty;
            Option2 = string.Empty;
            Option3 = string.Empty;
            Option4 = string.Empty;
            Option5 = string.Empty;

            IsActive = true;
        }

        public int Value { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public bool? IsActive { get; set; }

    }
}


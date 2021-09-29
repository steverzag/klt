using System;

namespace FloraYFaunaAPI.ViewModel
{
    public class CarouselViewModel : BaseDBModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
}

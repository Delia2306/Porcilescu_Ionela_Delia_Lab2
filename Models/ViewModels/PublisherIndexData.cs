using Porcilescu_Ionela_Delia_Lab2.Models;

namespace Porcilescu_Ionela_Delia_Lab2.Models.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}

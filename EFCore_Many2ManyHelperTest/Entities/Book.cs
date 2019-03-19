using System.Collections.Generic;

namespace EFCore_Many2ManyHelperTest.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}

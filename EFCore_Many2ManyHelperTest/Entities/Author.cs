using System.Collections.Generic;

namespace EFCore_Many2ManyHelperTest.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookAuthor> AuthorBooks { get; set; }
    }
}

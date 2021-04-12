using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookServices
    {
        int Add(AddBookDto dto);
        void Update(int id, UpdateBookDto dto);

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategroyServices
    {
        int Add(AddBookCategroyDto dto);
    }
}

using HaladoProg2Beadandó.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaladoProg2Beadandó.Controllers
{
 
    [ApiController]
    public abstract class DataContextController : ControllerBase
    {
        protected readonly DataContext _context;
        public DataContextController(DataContext context)
        {
            _context = context;
        }


    }
}

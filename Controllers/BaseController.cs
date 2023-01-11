using Microsoft.AspNetCore.Mvc;
using PaylocityDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Controllers
{
    [Produces("application/json")]
    public class BaseController : Controller
    {
		protected readonly AppDbContext dbContext;

		public BaseController(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
	}
}

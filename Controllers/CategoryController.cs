using BokkingOnline.Data;
using BokkingOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace BokkingOnline.Controllers
{
    public class CategoryController : Controller
    {
        

		private readonly GenericRepository<Category> _repository;

		public CategoryController(GenericRepository<Category> repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> Index()
        {
            var categoryList = await _repository.GetAllAsync();
            return View(categoryList);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }
		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				await _repository.CreateAsync(obj);
				_repository.Save();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

		//GET
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var categoryFromDb = await _repository.GetByIdAsync(id);
			//var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
			}
			if (ModelState.IsValid)
			{
				await _repository.UpdateAsync(obj);
				_repository.Save();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int? id)
		{
			await _repository.DeleteAsync(id);
			_repository.Save();
			return RedirectToAction("Index");

		}
	}
}

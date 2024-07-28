using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        //var objCategoryList = _dbContext.Categories.ToList();
        IEnumerable<Category> objCategoryList = _dbContext.Categories;
        return View(objCategoryList);
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    // GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) return NotFound();

        var categoryFromDb = _dbContext.Categories.FirstOrDefault(x => x.ID == id);
        if (categoryFromDb == null) return NotFound();

        return View(categoryFromDb);
    }


    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (!ModelState.IsValid) return View(obj);

        _dbContext.Categories.Update(obj);
        _dbContext.SaveChanges();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0) return NotFound();

        var categoryFromDb = _dbContext.Categories.Find(id);

        if (categoryFromDb == null) return NotFound();

        return View(categoryFromDb);
    }

    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _dbContext.Categories.Find(id);
        if (obj == null) return NotFound();

        _dbContext.Categories.Remove(obj);
        _dbContext.SaveChanges();
        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index");

    }
}

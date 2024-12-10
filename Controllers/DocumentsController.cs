using Document_Repository.Models;
using Document_Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace Document_Repository.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext Context;

        public DocumentsController(ApplicationDbContext context) 
        {
            this.Context = context;
        }
        public IActionResult Index()
        {
            var documents = Context.Documents.ToList();
            return View(documents);
        }

        public IActionResult Create()
        {
            var documents = Context.Documents.ToList();
            return View(documents);
        }

        [HttpPost]
        public IActionResult Create(string documentName, string documentCode, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", file.FileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Save document details to the database
                var document = new Document
                {
                    DocumentName = documentName,
                    DocumentCode = documentCode,
                    FilePath = filePath,
                    FileSize = Math.Round((decimal)(file.Length / 1024.0 / 1024.0), 2), // Size in MB
                    FileExtension = Path.GetExtension(file.FileName),
                    UploadedBy = User.Identity?.Name ?? "Anonymous",
                    UploadedDateTime = DateTime.Now
                };

                Context.Documents.Add(document);
                Context.SaveChanges();

                return RedirectToAction("Create"); // Refresh the form and list
            }

            var documents = Context.Documents.ToList();
            ModelState.AddModelError(string.Empty, "File upload failed.");
            return View(documents);
        }

        public IActionResult View(int id)
        {
            var document = Context.Documents.FirstOrDefault(d => d.DocumentId == id);
            if (document == null) return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(document.FilePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(document.FilePath));
        }

        [HttpPost]
        public IActionResult Edit(int id, string documentName, string documentCode, IFormFile file)
        {
            var document = Context.Documents.FirstOrDefault(d => d.DocumentId == id);
            if (document == null) return NotFound();

            document.DocumentName = documentName;
            document.DocumentCode = documentCode;

            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", file.FileName);

                // Save new file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Update file details
                document.FilePath = filePath;
                document.FileSize = Math.Round((decimal)(file.Length / 1024.0 / 1024.0), 2);
                document.FileExtension = Path.GetExtension(file.FileName);
            }

            Context.SaveChanges();
            return RedirectToAction("Create");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var document = Context.Documents.FirstOrDefault(d => d.DocumentId == id);
            if (document == null) return NotFound();

            if (System.IO.File.Exists(document.FilePath))
            {
                System.IO.File.Delete(document.FilePath);
            }

            Context.Documents.Remove(document);
            Context.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}
    
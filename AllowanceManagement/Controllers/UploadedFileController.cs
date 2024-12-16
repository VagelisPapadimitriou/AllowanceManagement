using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AllowanceManagement.Controllers
{
    public class UploadedFileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UploadedFileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<UploadedFile> uploadedFilesList = _unitOfWork.UploadedFile.GetAll().ToList();
            return View(uploadedFilesList);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["error"] = "Παρακαλώ επιλέξτε ένα αρχείο.";
                return RedirectToAction("Index");
            }

            // Check if a file with the same name already exists in the database
            var existingFile = _unitOfWork.UploadedFile.Get(uf => uf.FileName == file.FileName);
            if (existingFile != null)
            {
                TempData["error"] = "Ένα αρχείο με αυτό το όνομα υπάρχει ήδη.";
                return RedirectToAction("Index");
            }

            // Read file content into a memory stream
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var uploadedFile = new UploadedFile
                {
                    FileName = file.FileName,
                    UploadDate = DateTime.Now,
                    FileContent = memoryStream.ToArray() // Store file content in byte array
                };

                _unitOfWork.UploadedFile.Add(uploadedFile);
                _unitOfWork.Save();
            }

            TempData["success"] = "Το αρχείο ανέβηκε επιτυχώς.";
            return RedirectToAction("Index");
        }

        public IActionResult Download(int id)
        {
            var uploadedFile = _unitOfWork.UploadedFile.Get(uf => uf.FileId == id);
            if (uploadedFile == null)
            {
                return NotFound();
            }

            var fileType = "application/octet-stream";

            return File(uploadedFile.FileContent, fileType, uploadedFile.FileName);
        }

        public IActionResult Open(int id)
        {
            var uploadedFile = _unitOfWork.UploadedFile.Get(uf => uf.FileId == id);
            if (uploadedFile == null)
            {
                return NotFound();
            }

            // Determine the MIME type based on the file extension
            string mimeType = GetMimeType(uploadedFile.FileName);

            // URL encode the filename to ensure it is safe for use in HTTP headers
            var encodedFileName = Uri.EscapeDataString(uploadedFile.FileName);

            // Set Content-Disposition header to inline to open the file in the browser
            Response.Headers.Add("Content-Disposition", $"inline; filename=\"{encodedFileName}\"");

            return File(uploadedFile.FileContent, mimeType);

        }

        private string GetMimeType(string fileName)
        {
            var mimeTypes = new Dictionary<string, string>
    {
        {".pdf", "application/pdf"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".png", "image/png"},
        {".gif", "image/gif"},
        {".html", "text/html"},
        {".txt", "text/plain"},
        {".doc", "application/msword"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        // Add more mappings as needed
    };

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return mimeTypes.ContainsKey(extension) ? mimeTypes[extension] : "application/octet-stream";
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var uploadedFileFromDb = _unitOfWork.UploadedFile.Get(uf => uf.FileId == id);
            if (uploadedFileFromDb == null)
            {
                return NotFound();
            }

            return View(uploadedFileFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var uploadedFileFromDb = _unitOfWork.UploadedFile.Get(uf => uf.FileId == id);
            if (uploadedFileFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.UploadedFile.Remove(uploadedFileFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Επιτυχής Διαγραφή Αρχείου";
            return RedirectToAction("Index");
        }


    }
}

#nullable disable
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BananaReader_audiobooks_ms.Model;
using BananaReader_audiobooks_ms.Persistence;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google;
using Microsoft.Extensions.Options;
using BananaReader_audiobooks_ms;
using System.IO;
using System.Text;
using BananaReader_audiobooks_ms.CloudStorage;
using System;

namespace BananaReader.AudioBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioBooksController : ControllerBase
    {
        private readonly AudioBooksContext _context;
        // The Google Cloud Storage client.
        private readonly ICloudStorage _cloudStorage;

        public AudioBooksController(AudioBooksContext context, ICloudStorage cloudStorage)
        {
            _context = context;
            _cloudStorage = cloudStorage;
        }       

        // GET: api/AudioBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AudioBook>>> GetAudioBooks()
        {
            return await _context.AudioBooks.ToListAsync();
        }

        // GET: api/AudioBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AudioBook>> GetAudioBook(int id)
        {
            var audioBook = await _context.AudioBooks.FindAsync(id);

            if (audioBook == null)
            {
                return NotFound();
            }

            return audioBook;
        }

        // PUT: api/AudioBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAudioBook(int id, AudioBook audioBook)
        {
            if (id != audioBook.id)
            {
                return BadRequest();
            }

            _context.Entry(audioBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AudioBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AudioBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AudioBook>> PostAudioBook(IFormFile archive, string Title)
        {
            try
            {

                AudioBook audioBook = new AudioBook();
                string fileNameForStorage = FormFileName(Title, archive.FileName);
                audioBook.path = await _cloudStorage.UploadFileAsync(archive, fileNameForStorage);
                _context.AudioBooks.Add(audioBook);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAudioBook", new { id = audioBook.id }, audioBook);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }

        // DELETE: api/AudioBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudioBook(int id)
        {
            var audioBook = await _context.AudioBooks.FindAsync(id);
            if (audioBook == null)
            {
                return NotFound();
            }

            _context.AudioBooks.Remove(audioBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AudioBookExists(int id)
        {
            return _context.AudioBooks.Any(e => e.id == id);
        }
    }
}

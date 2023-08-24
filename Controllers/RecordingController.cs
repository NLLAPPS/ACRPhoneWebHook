using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ACRPhone.Webhook.Authentication;
using ACRPhone.Webhook.Models;
using ACRPhone.Webhook.Repositories;
using ACRPhone.Webhook.ViewModels;


namespace ACRPhone.Webhook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordingController : ControllerBase
    {
        private readonly ILogger<RecordingController> _logger;

        private readonly IRecordingRepository _recordingRepository;

        public RecordingController(ILogger<RecordingController> logger, IRecordingRepository recordingRepository)
        {
            _logger = logger;

            _recordingRepository = recordingRepository;
        }

        [Authorize(AuthenticationSchemes  = CustomAuthOptions.DefaultScheme)]
        [HttpPost("all")]
        public IEnumerable<Recording> GetAll()
        {
            return _recordingRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetRecording")]
        public ActionResult<Recording> Get(long id)
        {
            var recording = _recordingRepository.GetById(id);
            if (recording == null)
            {
                return NotFound();
            }

            return recording;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Recording model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            _recordingRepository.Add(model);
            _recordingRepository.Save();

            return CreatedAtRoute("GetRecording", new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Recording model)
        {
            if (model == null || model.Id != id)
            {
                return BadRequest();
            }

            var recording = _recordingRepository.GetById(id);
            if (recording == null)
            {
                return NotFound();
            }

            recording.Note = model.Note;

            _recordingRepository.Update(recording);
            _recordingRepository.Save();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var recording = _recordingRepository.GetById(id);
            if (recording == null)
            {
                return NotFound();
            }

            _recordingRepository.Delete(recording);
            _recordingRepository.Save();

            return new NoContentResult();
        }

     
        [HttpPost("upload")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)]
        public async Task<IActionResult> Upload([FromForm] UploadRecordViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Secret) && model.Secret != Models.User.Secret)
            {
                return StatusCode(401, "User with specified secret " + model.Secret + " doesn't exist");
            }

            try
            {
                var length = 0L;
                var safeFileName = "";

                if (model.File != null)
                {
                    var file = model.File;
                    length = file.Length;
                    safeFileName = SafeFileName(file.FileName);
                    var filePath = Path.Combine(GetUploadPath(), safeFileName);

                    if (length == 0)
                    {
                        return BadRequest("Expected at least 1 byte, but got 0");
                    }

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                }

                var recording = new Recording
                {
                    Source = model.Source,
                    FileName = safeFileName,
                    Note = model.Note,
                    Date = model.Date,
                    FileSize = length,
                    Duration = model.Duration,
                };

                _recordingRepository.Add(recording);
                _recordingRepository.Save();

                return Ok();
            }
            catch (Exception e)
            {
                var message = "Error while uploading recording";

                _logger.LogError(e, message);

                return StatusCode(500, message);
            }
        }

        private static string GetUploadPath()
        {
            return "wwwroot/Uploads";
        }

        private static string SafeFileName(string fileName)
        {

            /* if (fileName.Contains(@"\"))
                 fileName = fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1);

             return fileName;*/

            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');

        }
    }
}
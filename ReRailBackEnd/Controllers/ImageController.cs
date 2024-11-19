using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ReRailBackEnd.Contexts;
using ReRailBackEnd.DTO;
using ReRailBackEnd.Entities;
using ReRailBackEnd.Hubs;
using ReRailBackEnd.Services;
using System.Reflection;

namespace ReRailBackEnd.Controllers
{
    [ApiController]
    [Route("/Image")]
    public class ImageController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHubContext<ClassificationHub> _hubContext;
        private readonly ILocationRectifierService _locationRectifierService;
        public ImageController(AppDbContext appDbContext,
            IHubContext<ClassificationHub> hubContext,
            ILocationRectifierService locationRectifierService)
        {
            _appDbContext = appDbContext;
            _hubContext = hubContext;
            _locationRectifierService = locationRectifierService;
        }


        [HttpPost("UploadImage")]
        public ActionResult UploadImage(UploadedImageDTO model)
        {
            if (model.Image == null || string.IsNullOrEmpty(model.Text))
            {
                return BadRequest("Both image and text are required.");
            }
            byte[] imageData;
            using (var memoryStream = new MemoryStream())
            {
                model.Image.CopyTo(memoryStream);
                imageData = memoryStream.ToArray();
            }
            TrackSnapShot trackSnapShot = new()
            {
                Image = imageData,
                Location = model.Text,//_locationRectifierService.RectifyPoint(model.Text), We trust out source data currently, more precise rectification code will be added later
                CreationDate = DateTime.Now,
            };
            try
            {
                _appDbContext.Images.Add(trackSnapShot);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex) {
                return BadRequest("Could not store image");
            }
            try
            {
                //_hubContext.Clients.All.SendAsync("ProcessImage", trackSnapShot.Id).Wait();
                _hubContext.Clients.Group("Resolver").SendAsync("ProcessImage", trackSnapShot.Id).Wait();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return Ok();
        }
        [HttpGet("GetImage")]
        public ActionResult GetImage(int? Id)
        {
            if(Id == null) { return BadRequest("No id was provided"); }
            var imageRecord = _appDbContext.Images.FirstOrDefault(e=> e.Id == Id);
            if (imageRecord == null)
            {
                return NotFound("Image not found.");
            }
            // Return image as binary data with appropriate content type
            return File(imageRecord.Image, "image/jpeg");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ChatApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Action to display all chat messages
        public IActionResult Index()
        {
            List<ChatMessage> messages = _context.ChatMessage.ToList();
            return View(messages);
        }

        // Action to store a new chat message
        [HttpPost]
        [Authorize]
        public IActionResult SendMessage(string userName, string Message)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(Message))
            {
                // Handle invalid input (e.g., username or message content is empty)
                // You can return a BadRequest or display an error message
                return BadRequest("Invalid input. Both username and message content are required.");
            }

            var message = new ChatMessage
            {
                UserName = userName,
                Message = Message,
                Timestamp = DateTime.Now
            };

            _context.ChatMessage.Add(message);
            _context.SaveChanges();

            // Redirect back to the chat page
            return RedirectToAction("Index");
        }

    }
}


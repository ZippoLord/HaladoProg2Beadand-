using AutoMapper;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models.DTOs.Transaction;
using HaladoProg2Beadandó.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HaladoProg2Beadandó.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : DataContextController
    {
        private readonly TransactionLogService _logService; 
        private readonly IMapper mapper;
        public TransactionsController(DataContext context, IMapper mapper, TransactionLogService logService) : base(context)
        {
            _logService = logService;
            this.mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(int userId)
        {
            var transactions = _logService.AllLogs();
            if (transactions == null || !transactions.Any(x => x.UserId == userId))
                return NotFound("Nincs ilyen ID-jű felhasználó");
            var result = transactions
           .Where(x => x.UserId == userId)
           .OrderByDescending(d => d.Date)
           .ToList();

            var mapped = mapper.Map<List<TransactionDetailsDTO>>(result);
            return Ok(mapped);
        }


        [HttpGet("details/{transactionId}")]

        public async Task<IActionResult> GetTransactionById(int transactionId)
        {
            var transactions = _logService.AllLogs().FirstOrDefault(x => x.TransactionId == transactionId);
            if (transactions == null)
                return NotFound("Nincs ilyen ID-jű tranzakció");

            var mapped = mapper.Map<TransactionListDTO>(transactions);
            return Ok(mapped);
        }

    }
}
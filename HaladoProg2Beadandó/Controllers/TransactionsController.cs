using AutoMapper;
using HaladoProg2Beadandó.Data;
using HaladoProg2Beadandó.Models.DTOs;
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
            var result = transactions.Where(x => x.UserId == userId).OrderByDescending(d => d.Date).ToList();
            return Ok(result);
        }


        [HttpGet("details/{transactionId}")]

        public async Task<ActionResult<TransactionDetailsDTO>> GetTransactionById(int transactionId)
        {
            var transactions = _logService.AllLogs();
            if (transactions == null || !transactions.Any(x => x.TransactionId == transactionId))
                return NotFound("Nincs ilyen ID-jű tranzakció");
            var transaction = transactions.FirstOrDefault(x => x.TransactionId == transactionId);
            var result = new TransactionDetailsDTO
            {
                Price = transaction.Price,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Symbol = transaction.Symbol,
                CryptoCurrencyName = transaction.CryptoCurrencyName

            };
            return Ok(result);
        }

    }
}
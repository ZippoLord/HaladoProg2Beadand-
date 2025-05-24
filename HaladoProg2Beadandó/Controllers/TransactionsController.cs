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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactions;
        public TransactionsController(ITransactionService transactions)
        {
            _transactions = transactions;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(int userId)
        {
            try
            {
                var result = await _transactions.GetTransactionsByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba történt: {ex.Message}");
            }
        }


        [HttpGet("details/{transactionId}")]

        public async Task<IActionResult> GetTransactionById(int transactionId)
        {
            try
            {
                var result = await _transactions.GetTransactionsByTransactionId(transactionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Hiba történt: {ex.Message}");
            }
        }

    }
}
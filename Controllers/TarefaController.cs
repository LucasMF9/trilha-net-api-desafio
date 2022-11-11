using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("Obter por id")]
        public IActionResult ObterPorId(int id)
        {
            //Método obter por Id corrigido
            var tarefaBancoId = _context.Tarefas.Find(id);
            if (tarefaBancoId == null)
                return NotFound();

            return Ok(tarefaBancoId);
        }

        [HttpGet("Obter Todos")]
        public IActionResult ObterTodos()
        {       
            //Método obter todos corrigido
            var tarefaBancoTodos = _context.Tarefas;
            return Ok(tarefaBancoTodos);
        }

        [HttpGet("Obter Por Titulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            //Método obter por titulo corrigido
            var tarefa = _context.Tarefas.Where(x => x.Titulo == titulo);
            return Ok(tarefa);
        }

        [HttpGet("Obter Por Data")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("Obter Por Status")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            //Método obter por status funcionando(não alterado)
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPost("Criar Tarefa")]
        public IActionResult Criar(Tarefa tarefa)
        {
            //Método criar corrigido
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("Atualizar Tarefa")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            //Método atualizar corrigido
            var tarefaBancoAtualizar = _context.Tarefas.Find(id);

            if (tarefaBancoAtualizar == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBancoAtualizar.Titulo = tarefa.Titulo;
            tarefaBancoAtualizar.Descricao = tarefa.Descricao;
            tarefaBancoAtualizar.Data = tarefa.Data;
            tarefaBancoAtualizar.Status = tarefa.Status;

            _context.Update(tarefaBancoAtualizar);
            _context.SaveChanges();

            return Ok(tarefaBancoAtualizar);
        }

        [HttpDelete("Apagar Tarefa")]
        public IActionResult Deletar(int id)
        {
            //Método deletar corrigido
            var tarefaBancoDeletar = _context.Tarefas.Find(id);

            if (tarefaBancoDeletar == null)
                return NotFound();

            _context.Remove(tarefaBancoDeletar);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

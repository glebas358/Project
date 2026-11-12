using Project.Api.Repositories.Interfaces;
using Project.Api.Services.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Services
{
    public class ManagerNoteService : IManagerNoteService
    {
        private readonly IManagerNoteRepository _managerNoteRepository;

        public ManagerNoteService(IManagerNoteRepository managerNote)
        {
            _managerNoteRepository = managerNote;
        }

        public async Task AddAsync(string text, long employeeId)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("Текст заметки не может быть пустым.");

            var note = new ManagerNote
            {
                Text = text,
                EmployeeId = employeeId,
                CreatedAt = DateTime.UtcNow
            };

            await _managerNoteRepository.AddAsync(note);
        }

        public async Task<List<ManagerNote>> GetAllAsync()
        {
            return await _managerNoteRepository.GetAllAsync();
        }

        public async Task UpdateAsync(long id, string newText, long EmployeeId)
        {
            if (string.IsNullOrWhiteSpace(newText)) throw new Exception("Текст не может быть пустым.");

            var note = await _managerNoteRepository.GetByIdAsync(id);
            if (note == null) throw new Exception("Заметка не найдена.");

            
            if (note.EmployeeId != EmployeeId) throw new Exception("Вы можете редактировать только свои заметки.");

            note.Text = newText;
            await _managerNoteRepository.UpdateAsync(note);
        }

        public async Task DeleteAsync(long id, long EmployeeId)
        {
            var note = await _managerNoteRepository.GetByIdAsync(id);
            if (note == null) throw new Exception("Заметка не найдена.");

            if (note.EmployeeId != EmployeeId) throw new Exception("Вы можете удалять только свои заметки.");

            await _managerNoteRepository.DeleteAsync(note);
        }
    }
}

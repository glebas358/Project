using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Application.Interfaces.Repositories;
using Project.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Project.UI.ViewModels
{
    public partial class ManagerNotesViewModel : ObservableObject
    {
        private readonly IManagerNoteRepository _noteRepository;

        public ObservableCollection<ManagerNote> Notes { get; } = new();

        [ObservableProperty] private string _newNoteText = string.Empty;
        [ObservableProperty] private bool _isBusy;

        [ObservableProperty]
        private long? _editingNoteId;

        public ManagerNotesViewModel(IManagerNoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

       

        [RelayCommand]
        private async Task LoadNotesAsync()
        {
            IsBusy = true;
            try
            {
                var notes = await _noteRepository.GetAllAsync();
                Notes.Clear();
                foreach (var note in notes) Notes.Add(note);
            }
            catch (Exception ex) { await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК"); }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async Task AddNoteAsync()
        {
            if (string.IsNullOrWhiteSpace(NewNoteText)) return;

            try
            {
                await _noteRepository.AddAsync(NewNoteText);
                NewNoteText = string.Empty; 
                await LoadNotesAsync(); 
            }
            catch (Exception ex) { await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК"); }
        }

        [RelayCommand]
        private async Task UpdateNoteAsync(ManagerNote note)
        {
            if (note == null) return;

            
            string newText = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayPromptAsync(
                "Редактирование", "Измените текст заметки:", initialValue: note.Text, accept: "Сохранить", cancel: "Отмена");

            if (string.IsNullOrWhiteSpace(newText)) return;

            try
            {
                await _noteRepository.UpdateAsync(note.Id, newText);
                await LoadNotesAsync(); 
            }
            catch (Exception ex) { await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК"); }
        }

        
        [RelayCommand]
        private async Task DeleteNoteAsync(ManagerNote note)
        {
            if (note == null) return;

            bool confirm = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Удаление", "Вы уверены, что хотите удалить заметку?", "Да", "Нет");
            if (!confirm) return;

            try
            {
                await _noteRepository.DeleteAsync(note.Id);
                Notes.Remove(note); 
            }
            catch (Exception ex) { await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlertAsync("Ошибка", ex.Message, "ОК"); }
        }

    }
}
